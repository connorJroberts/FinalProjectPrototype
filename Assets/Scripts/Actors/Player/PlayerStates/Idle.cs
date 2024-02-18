using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : StateComponent
{

    public override void Enter(string msg = "")
    {
        
        //TODO: Global Broadcast Player.Idle

    }

    override public void FixedProcess()
    {

        Actor.Velocity = Vector3.Lerp(Actor.Velocity, Vector3.zero, Actor.MomentumFalloffTime * Time.fixedDeltaTime); // Lerps Actor velocity to 0 via Mopmentum Falloff

    }

    public override void Process()
    {

        if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0) StateMachine.TransitionTo("Walk");
        else if (Input.GetButtonDown("Jump")) StateMachine.TransitionTo("Jump");
    }
}
