using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using UnityEngine;

public class Air : StateComponent
{

    private bool _wallRunComplete = false;
    private float _jumpBuffer = 0;
    private float _slideBuffer = 0;

    public override void Enter(string msg = "")
    {
        _jumpBuffer = 0;

        if (msg == "WallRunComplete") _wallRunComplete = true;
        else _wallRunComplete = false;

    }

    public override void FixedProcess()
    {
        _jumpBuffer -= Time.fixedDeltaTime;
        _slideBuffer -= Time.fixedDeltaTime;

        Vector3 hMovement = Quaternion.LookRotation(PlayerData.transform.forward, Vector3.up) * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * PlayerData.WalkSpeed * Time.fixedDeltaTime;

        Player.Velocity.x = Mathf.Lerp(Player.Velocity.x, hMovement.x, PlayerData.AirControlFactor);
        Player.Velocity.z = Mathf.Lerp(Player.Velocity.z, hMovement.z, PlayerData.AirControlFactor);

        Player.Velocity.y += PlayerData.Gravity * Time.fixedDeltaTime * Time.fixedDeltaTime;
        Player.Controller.Move(Player.Velocity);

    }

    public override void Process()
    {
        if (Player.Controller.collisionFlags == CollisionFlags.Sides && Input.GetAxisRaw("Vertical") == 1 && !_wallRunComplete)
            StateMachine.TransitionTo("WallRun");
        else if (Input.GetButtonDown("Jump") && (Player.CurrentJumpCount > 0)) StateMachine.TransitionTo("Jump");
        else if (Input.GetButtonDown("Jump")) _jumpBuffer = PlayerData.JumpBuffer;
        else if (Input.GetButtonDown("Crouch")) _slideBuffer = PlayerData.SlideBuffer;
        else if (Player.Controller.isGrounded)
        {
            if (_jumpBuffer > 0) StateMachine.TransitionTo("Jump");
            else if (_slideBuffer > 0) StateMachine.TransitionTo("Slide");
            else StateMachine.TransitionTo("Idle");
        }
    }

}
