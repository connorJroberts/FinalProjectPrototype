using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : StateComponent
{
    public override void Enter(string msg = "")
    {
        Vector3 jumpDirection = new Vector3(Actor.Velocity.x, 0, Actor.Velocity.z).magnitude * Actor.transform.forward;


        Actor.CurrentJumpCount -= 1;
        Actor.Velocity = new Vector3(jumpDirection.x, Actor.JumpVelocity * Time.fixedDeltaTime, jumpDirection.z);
        Actor.Controller.Move(Actor.Velocity);
        StateMachine.TransitionTo("Air"); 

    }
}
