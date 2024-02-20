using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : StateComponent
{
    private Quaternion wallRunAngle;
    private Vector3 wallRunDirection;
    private float transitionTimer = 1.0f;

    public override void Enter(string msg = "")
    {
        Actor.CurrentJumpCount = Actor.JumpCount - 1;

        Vector3 facing = Actor.transform.forward;
        Vector3 resultant = (Actor.Collision.normal + facing);

        if (Vector3.Angle(resultant, Quaternion.AngleAxis(90, Vector3.up) * Actor.Collision.normal) <= 90) wallRunAngle = Quaternion.AngleAxis(92, Vector3.up);
        else wallRunAngle = Quaternion.AngleAxis(-92, Vector3.up);
    }

    public override void FixedProcess()
    {

        //check if - or + with regard to wall normal + set wallRunDirection
        wallRunDirection = wallRunAngle * Actor.Collision.normal;

        Actor.Velocity = wallRunDirection.normalized * Actor.WallRunSpeed * Time.fixedDeltaTime;
        Actor.Controller.Move(Actor.Velocity);

    }

    public override void Process()
    {
        if (Actor.Controller.collisionFlags == CollisionFlags.None)
        {
            transitionTimer -= Time.fixedDeltaTime;
            if (transitionTimer < 0.0f) StateMachine.TransitionTo("Air");
        }
        else transitionTimer = 1f;

        if (Input.GetButtonDown("Jump")) StateMachine.TransitionTo("WallJump");
        else if (Input.GetAxisRaw("Vertical") != 1) StateMachine.TransitionTo("Air");
    }

}
