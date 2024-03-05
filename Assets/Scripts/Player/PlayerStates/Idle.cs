using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{

    public override void Enter(string msg = "")
    {

        //TODO: Global Broadcast Player.Idle
        Player.CurrentJumpCount = PlayerData.JumpCount;
        Player.CurrentFuel = PlayerData.MaxFuel;

    }

    override public void FixedProcess()
    {
        Player.Velocity = Vector3.Lerp(Player.Velocity, Vector3.zero, PlayerData.MomentumFalloffTime * Time.fixedDeltaTime * Time.fixedDeltaTime); // Lerps Actor velocity to 0 via Mopmentum Falloff

        //Handle Fuel
        Player.CurrentFuel += PlayerData.FuelRegenerationAmount * PlayerData.FuelRegenerationRate * Time.fixedDeltaTime;
        Player.CurrentFuel = Mathf.Clamp(Player.CurrentFuel, 0, PlayerData.MaxFuel);

    }

    public override void Process()
    {

        if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0) StateMachine.TransitionTo(new Walk());
        else if (Input.GetButtonDown("Jump")) StateMachine.TransitionTo(new Jump());
        else if (!Player.Controller.isGrounded) StateMachine.TransitionTo(new Air());
        else if (Input.GetButtonDown("Crouch")) StateMachine.TransitionTo(new Crouch());
    }
}
