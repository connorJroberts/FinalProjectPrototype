using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalWallRun : StateComponent
{
    public override void Enter(string msg = "")
    {
        Player.Velocity = new Vector3(0f, Player.VerticalWallRunVelocity, 0f) * Time.fixedDeltaTime;
    }


    public override void FixedProcess()
    {
        Player.Velocity = PlayerData.transform.forward * PlayerData.RunSpeed * PlayerData.VerticalWallRunForwardSpeedMulitplier * Time.fixedDeltaTime + new Vector3(0, Player.Velocity.y, 0);
        Player.Velocity.y += PlayerData.VerticalWallRunFalloffRate * PlayerData.Gravity * Time.fixedDeltaTime * Time.fixedDeltaTime;

        Player.Controller.Move(Player.Velocity);
    }

    public override void Process()
    {
        if (Player.Velocity.y < 0f) StateMachine.TransitionTo("Air", "WallRunComplete");
    }
}
