using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalWallRun : StateComponent
{
    public override void Enter(string msg = "")
    {
        Actor.Velocity = new Vector3(0f, Actor.VerticalWallRunVelocity, 0f) * Time.fixedDeltaTime;
    }


    public override void FixedProcess()
    {
        Actor.Velocity = Actor.transform.forward * Actor.RunSpeed * Actor.VerticalWallRunForwardSpeedMulitplier * Time.fixedDeltaTime + new Vector3(0, Actor.Velocity.y, 0);
        Actor.Velocity.y += Actor.VerticalWallRunFalloffRate * Actor.Gravity * Time.fixedDeltaTime * Time.fixedDeltaTime;



        Actor.Controller.Move(Actor.Velocity);
    }

    public override void Process()
    {
        if (Actor.Velocity.y < 0f) StateMachine.TransitionTo("Air", "WallRunComplete");
    }
}
