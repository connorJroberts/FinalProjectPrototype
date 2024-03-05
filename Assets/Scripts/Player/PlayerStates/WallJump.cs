using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : State
{
    public override void Enter(string msg = "")
    {
        Player.Velocity = Player.Collision.normal * PlayerData.WallJumpHorizontalForce * Time.fixedDeltaTime + Player.transform.forward * PlayerData.WallRunSpeed * Time.fixedDeltaTime;
        Player.Velocity.y = Player.WallJumpVelocity * Time.fixedDeltaTime;
        Player.Controller.Move(Player.Velocity);
        StateMachine.TransitionTo(new Air());

    }
}
