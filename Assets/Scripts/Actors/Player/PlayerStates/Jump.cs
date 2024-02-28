using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class Jump : StateComponent
{
    private Vector3 _input;
    private Vector3 jumpDirection;

    public override void Enter(string msg = "")
    {
        if (msg != "SlideComplete")
        {
            _input = Quaternion.LookRotation(Player.transform.forward, Vector3.up) * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
            jumpDirection = new Vector3(Player.Velocity.x, 0, Player.Velocity.z).magnitude * _input;
        }
        else
        {
            jumpDirection = new Vector3(Player.Velocity.x, 0, Player.Velocity.z);
        }
        


        Player.CurrentJumpCount -= 1;
        Player.Velocity = new Vector3(jumpDirection.x, Player.JumpVelocity * Time.fixedDeltaTime, jumpDirection.z);
        Player.Controller.Move(Player.Velocity);
        StateMachine.TransitionTo("Air"); 

    }
}
