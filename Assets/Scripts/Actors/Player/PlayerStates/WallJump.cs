using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : StateComponent
{
    public override void Enter(string msg = "")
    {
        Actor.Velocity = Actor.Collision.normal * Actor.WallJumpHorizontalForce * Time.fixedDeltaTime + Actor.transform.forward * Actor.WallRunSpeed * Time.fixedDeltaTime; 
        Actor.Velocity.y = Actor.WallJumpVelocity * Time.fixedDeltaTime;
        Actor.Controller.Move(Actor.Velocity);
        StateMachine.TransitionTo("Air");

    }
}
