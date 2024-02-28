using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : StateComponent
{
    private Vector3 _initialPlayerVelocity;

    public override void Enter(string msg = "")
    {
        
        PlayerData.CameraRotation.gameObject.transform.localPosition = new Vector3(0, 1, 0);

    }

    private Vector3 hMove;

    public override void FixedProcess()
    {

        Player.Velocity = Vector3.Lerp(Player.Velocity, hMove * PlayerData.CrouchSpeed * Time.fixedDeltaTime, Time.fixedDeltaTime / PlayerData.MomentumFalloffTime) + new Vector3(0,-0.1f,0);
        Player.Controller.Move(Player.Velocity);

        //Handle Fuel
        Player.CurrentFuel += PlayerData.FuelRegenerationAmount * PlayerData.FuelRegenerationRate * Time.fixedDeltaTime;
        Player.CurrentFuel = Mathf.Clamp(Player.CurrentFuel, 0, PlayerData.MaxFuel);

    }

    public override void Process()
    {
        hMove = Quaternion.LookRotation(PlayerData.transform.forward, Vector3.up) * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        if (Input.GetButtonDown("Jump")) StateMachine.TransitionTo("Jump");
        else if (Input.GetButtonUp("Crouch") && Player.Velocity != Vector3.zero) StateMachine.TransitionTo("Walk");
        else if (Input.GetButtonUp("Crouch")) StateMachine.TransitionTo("Idle");
        else if (!Player.Controller.isGrounded) StateMachine.TransitionTo("Air");

    }

    public override void Exit()
    {
        PlayerData.CameraRotation.gameObject.transform.localPosition = new Vector3(0, PlayerData.DefaultCameraHeight, 0);
    }

}
