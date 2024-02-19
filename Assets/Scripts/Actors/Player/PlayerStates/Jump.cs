using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : StateComponent
{
    public override void Enter(string msg = "")
    {
        Actor.CurrentJumpCount -= 1;
        Actor.Velocity = new Vector3(Actor.Velocity.x, Actor.JumpVelocity * Time.fixedDeltaTime, Actor.Velocity.z);
        Actor.Controller.Move(Actor.Velocity);
        StateMachine.TransitionTo("Air"); 

    }
}
