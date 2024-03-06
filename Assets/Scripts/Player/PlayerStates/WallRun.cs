using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : State
{
    private Quaternion wallRunAngle;
    private Vector3 wallRunDirection;
    private Vector3 verticalCrossVector;
    private float transitionTimer = 1.0f;

    public override void Enter(string msg = "")
    {
        Player.CurrentJumpCount = PlayerData.JumpCount - 1;
        Player.CurrentDashCount = 1;

        Vector3 facing = Player.transform.forward;
        Vector3 resultant = (Player.Collision.normal + facing);

        if (Vector3.Angle(resultant, Quaternion.AngleAxis(90, Vector3.up) * Player.Collision.normal) <= 90)
        {
            wallRunAngle = Quaternion.AngleAxis(92, Vector3.up);
            verticalCrossVector = Vector3.up;
        }
        else
        {
            wallRunAngle = Quaternion.AngleAxis(-92, Vector3.up);
            verticalCrossVector = Vector3.down;
        }
        
        if (Vector3.Angle(facing, -Player.Collision.normal) <= PlayerData.VerticalWallRunInitiationAngle) StateMachine.TransitionTo(new VerticalWallRun());
        else if (Vector3.Angle(facing, -Player.Collision.normal) >= 360f - PlayerData.VerticalWallRunInitiationAngle) StateMachine.TransitionTo(new VerticalWallRun());

    }

    public override void FixedProcess()
    {
        //check if - or + with regard to wall normal + set wallRunDirection
        wallRunDirection = wallRunAngle * Player.Collision.normal;

        //Handle Fuel
        Player.CurrentFuel += PlayerData.FuelRegenerationAmount * PlayerData.FuelRegenerationRate * Time.fixedDeltaTime;
        Player.CurrentFuel = Mathf.Clamp(Player.CurrentFuel, 0, PlayerData.MaxFuel);

        Player.Velocity = wallRunDirection.normalized * Player.WallRunSpeed + -Player.Collision.normal * Time.fixedDeltaTime * 10;
        Player.Velocity.y += PlayerData.VerticalWallRunFalloffRate * PlayerData.Gravity * Time.fixedDeltaTime * Time.fixedDeltaTime;
        Player.Controller.Move(Player.Velocity);

    }

    public override void Process()
    {
        HandleRotation();

        if (Player.Controller.collisionFlags == CollisionFlags.None)
        {
            Player.Velocity = wallRunDirection.normalized * Player.WallRunSpeed;
            transitionTimer -= Time.fixedDeltaTime;
            if (transitionTimer < 0.0f)
            {
                StateMachine.TransitionTo(new Air());
            }
        }
        else transitionTimer = 1f;

        if (Input.GetButtonDown("Jump")) StateMachine.TransitionTo(new WallJump());
        else if (Input.GetButtonDown("Sprint")) StateMachine.TransitionTo(new Dash());
        else if (Input.GetAxisRaw("Vertical") != 1) StateMachine.TransitionTo(new Air());
        else if (Player.Controller.isGrounded) StateMachine.TransitionTo(new Idle());
    }

    private void HandleRotation()
    {
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(Player.Velocity.x, 0, Player.Velocity.z).normalized + 0.5f * Player.Collision.normal, Vector3.up);

        float cameraAngle = Vector3.SignedAngle(Player.Collision.normal, Player.transform.forward, verticalCrossVector);
        if (cameraAngle <= 90 && cameraAngle >= 0) 
        {
            targetRotation = Quaternion.LookRotation(Player.transform.forward, Vector3.up);
        }

        Player.transform.rotation = Quaternion.Slerp(Player.transform.rotation, targetRotation, 5 * Time.deltaTime);
    }


}