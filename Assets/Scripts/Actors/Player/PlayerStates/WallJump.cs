using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : StateComponent
{
    public override void Enter(string msg = "")
    {
        Player.Velocity = Player.Collision.normal * PlayerData.WallJumpHorizontalForce * Time.fixedDeltaTime + PlayerData.transform.forward * PlayerData.WallRunSpeed * Time.fixedDeltaTime;
        Player.Velocity.y = Player.WallJumpVelocity * Time.fixedDeltaTime;
        Player.Controller.Move(Player.Velocity);
        StateMachine.TransitionTo("Air");

    }
}
