using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : State
{
    private Vector3 hMove;

    public override void FixedProcess()
    {

        Player.Velocity = Vector3.Lerp(Player.Velocity, hMove * PlayerData.WalkSpeed * Time.fixedDeltaTime, Time.fixedDeltaTime / PlayerData.MomentumFalloffTime);
        Player.Controller.Move(Player.Velocity);

        //Handle Fuel
        Player.CurrentFuel += PlayerData.FuelRegenerationAmount * PlayerData.FuelRegenerationRate * Time.fixedDeltaTime;
        Player.CurrentFuel = Mathf.Clamp(Player.CurrentFuel, 0, PlayerData.MaxFuel);

    }

    public override void Process()
    {
        hMove = Quaternion.LookRotation(Player.transform.forward, Vector3.up) * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        if (Player.Velocity == Vector3.zero) StateMachine.TransitionTo(new Idle());
        else if (Input.GetButtonDown("Jump")) StateMachine.TransitionTo(new Jump());
        else if (Input.GetAxisRaw("Vertical") == 1) StateMachine.TransitionTo(new Run());
        else if (Input.GetButtonDown("Crouch")) StateMachine.TransitionTo(new Crouch());
        else if (!Player.Controller.isGrounded)
        {
            StateMachine.TransitionTo(new Air());
            Player.Velocity.y = 0;
        }

    }

}
