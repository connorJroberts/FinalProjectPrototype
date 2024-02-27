using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : StateComponent
{
    private Quaternion wallRunAngle;
    private Vector3 wallRunDirection;
    private Vector3 verticalCrossVector;
    private float transitionTimer = 1.0f;

    public override void Enter(string msg = "")
    {
        Actor.CurrentJumpCount = Actor.JumpCount - 1;

        Vector3 facing = Actor.transform.forward;
        Vector3 resultant = (Actor.Collision.normal + facing);

        if (Vector3.Angle(resultant, Quaternion.AngleAxis(90, Vector3.up) * Actor.Collision.normal) <= 90)
        {
            wallRunAngle = Quaternion.AngleAxis(92, Vector3.up);
            verticalCrossVector = Vector3.up;
        }
        else
        {
            wallRunAngle = Quaternion.AngleAxis(-92, Vector3.up);
            verticalCrossVector = Vector3.down;
        }
        
        if (Vector3.Angle(facing, -Actor.Collision.normal) <= Actor.VerticalWallRunInitiationAngle) StateMachine.TransitionTo("VerticalWallRun");
        else if (Vector3.Angle(facing, -Actor.Collision.normal) >= 360f - Actor.VerticalWallRunInitiationAngle) StateMachine.TransitionTo("VerticalWallRun");

    }

    public override void FixedProcess()
    {
        //check if - or + with regard to wall normal + set wallRunDirection
        wallRunDirection = wallRunAngle * Actor.Collision.normal;

        Actor.Velocity = wallRunDirection.normalized * Actor.WallRunSpeed * Time.fixedDeltaTime + -Actor.Collision.normal * Time.fixedDeltaTime * 10;
        Actor.Velocity.y += Actor.VerticalWallRunFalloffRate * Actor.Gravity * Time.fixedDeltaTime * Time.fixedDeltaTime;
        Actor.Controller.Move(Actor.Velocity);

    }

    public override void Process()
    {
        HandleRotation();

        if (Actor.Controller.collisionFlags == CollisionFlags.None)
        {
            transitionTimer -= Time.fixedDeltaTime;
            if (transitionTimer < 0.0f)
            {
                StateMachine.TransitionTo("Air");
                Actor.Velocity = wallRunDirection.normalized * Actor.WallRunSpeed * Time.fixedDeltaTime;
            }
        }
        else transitionTimer = 1f;

        if (Input.GetButtonDown("Jump")) StateMachine.TransitionTo("WallJump");
        else if (Input.GetAxisRaw("Vertical") != 1) StateMachine.TransitionTo("Air");
        else if (Actor.Controller.isGrounded) StateMachine.TransitionTo("Idle");
    }

    private void HandleRotation()
    {
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(Actor.Velocity.x, 0, Actor.Velocity.z).normalized + 0.5f * Actor.Collision.normal, Vector3.up);

        float cameraAngle = Vector3.SignedAngle(Actor.Collision.normal, Actor.transform.forward, verticalCrossVector);
        if (cameraAngle <= 90 && cameraAngle >= 0) 
        {
            targetRotation = Quaternion.LookRotation(Actor.transform.forward, Vector3.up);
        }

        Actor.transform.rotation = Quaternion.Slerp(Actor.transform.rotation, targetRotation, 5 * Time.deltaTime);
    }


}
