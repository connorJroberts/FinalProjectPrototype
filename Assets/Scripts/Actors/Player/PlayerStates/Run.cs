using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : StateComponent
{
    private Vector3 hMove;

    public override void FixedProcess()
    {

        Player.Velocity = Vector3.Lerp(Player.Velocity, hMove * PlayerData.RunSpeed * Time.fixedDeltaTime, Time.fixedDeltaTime / PlayerData.MomentumFalloffTime) + new Vector3(0, -0.1f, 0);
        Player.Controller.Move(Player.Velocity);

        //Handle Fuel
        Player.CurrentFuel += PlayerData.FuelRegenerationAmount * PlayerData.FuelRegenerationRate * Time.fixedDeltaTime;
        Player.CurrentFuel = Mathf.Clamp(Player.CurrentFuel, 0, PlayerData.MaxFuel);

    }

    public override void Process()
    {
        hMove = Quaternion.LookRotation(PlayerData.transform.forward, Vector3.up) * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        if (Player.Velocity == Vector3.zero) StateMachine.TransitionTo("Idle");
        else if (Input.GetButtonDown("Jump")) StateMachine.TransitionTo("Jump");
        else if (Input.GetAxisRaw("Vertical") != 1) StateMachine.TransitionTo("Walk");
        else if (Input.GetButton("Sprint") && Player.CurrentFuel > 0) StateMachine.TransitionTo("Sprint");
        else if (Input.GetButtonDown("Crouch")) StateMachine.TransitionTo("Crouch");
        else if (!Player.Controller.isGrounded) StateMachine.TransitionTo("Air");

    }
}
