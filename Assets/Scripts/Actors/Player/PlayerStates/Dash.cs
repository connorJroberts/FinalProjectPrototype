using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : StateComponent
{

    public override void Enter(string msg = "")
    {
        Player.Velocity = (Player.Velocity.magnitude + PlayerData.DashVelocity) * (Player.CameraRotation.transform.rotation * Vector3.forward) * Time.fixedDeltaTime;
        Player.Velocity.y += PlayerData.UpwardsDashVelocity * Time.fixedDeltaTime;
        Player.Controller.Move(Player.Velocity);
        StateMachine.TransitionTo("Air");
    }

}
