using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : StateComponent
{

    public override void Enter(string msg = "")
    {

        //TODO: Global Broadcast Player.Idle
        Actor.CurrentJumpCount = Actor.JumpCount;
        Actor.CurrentFuel = Actor.MaxFuel;

    }

    override public void FixedProcess()
    {

        Actor.Velocity = Vector3.Lerp(Actor.Velocity, Vector3.zero, Actor.MomentumFalloffTime * Time.fixedDeltaTime); // Lerps Actor velocity to 0 via Mopmentum Falloff

        //Handle Fuel
        Actor.CurrentFuel += Actor.FuelRegenerationAmount * Actor.FuelRegenerationRate * Time.fixedDeltaTime;
        Actor.CurrentFuel = Mathf.Clamp(Actor.CurrentFuel, 0, Actor.MaxFuel);

    }

    public override void Process()
    {

        if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0) StateMachine.TransitionTo("Walk");
        else if (Input.GetButtonDown("Jump")) StateMachine.TransitionTo("Jump");
        //else if (!Actor.Controller.isGrounded) StateMachine.TransitionTo("Air");
    }
}
