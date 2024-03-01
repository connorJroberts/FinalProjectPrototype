using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;

public class Air : StateComponent
{

    private bool _wallRunComplete = false;
    private float _jumpBuffer = 0;
    private float _slideBuffer = 0;
    private Vector3 _initialVelocity;

    public override void Enter(string msg = "")
    {
        _initialVelocity = new Vector3(Player.Velocity.x, 0, Player.Velocity.z) ;
        _jumpBuffer = 0;
        Player.WallRunSpeed = _initialVelocity.magnitude;

        if (msg == "WallRunComplete") _wallRunComplete = true;
        else _wallRunComplete = false;

    }

    public override void FixedProcess()
    {
        _jumpBuffer -= Time.fixedDeltaTime;
        _slideBuffer -= Time.fixedDeltaTime;

        if (Player.Controller.collisionFlags == CollisionFlags.Above) Player.Velocity.y = Mathf.Abs(Player.Velocity.y) * -1; //Head Bonk Reflects Y Velocity

        Vector3 hInput = Quaternion.LookRotation(PlayerData.transform.forward, Vector3.up) * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        if (new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).z == -1f)
        {
            Player.Velocity = Vector3.Lerp(Player.Velocity, _initialVelocity.magnitude * hInput.normalized + new Vector3(0, Player.Velocity.y, 0), PlayerData.BackwardsAirControlFactor * (Time.fixedDeltaTime / PlayerData.MomentumFalloffTime));
        }
        else if (new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) != Vector3.zero)
        {
            Player.Velocity = Vector3.Lerp(Player.Velocity, _initialVelocity.magnitude * hInput.normalized + new Vector3(0, Player.Velocity.y, 0), PlayerData.AirControlFactor * (Time.fixedDeltaTime / PlayerData.MomentumFalloffTime));
        }

        Player.Velocity.y += PlayerData.Gravity * Time.fixedDeltaTime * Time.fixedDeltaTime * (Player.Velocity.y <= 0 ? PlayerData.GravityDropMultiplier : 1);
        Player.Controller.Move(Player.Velocity);

    }

    public override void Process()
    {
        if (Player.Controller.collisionFlags == CollisionFlags.Sides && Input.GetAxisRaw("Vertical") == 1 && !_wallRunComplete)
            StateMachine.TransitionTo("WallRun");
        else if (Input.GetButtonDown("Jump") && (Player.CurrentJumpCount > 0)) StateMachine.TransitionTo("Jump");
        else if (Input.GetButtonDown("Jump")) _jumpBuffer = PlayerData.JumpBuffer;
        else if (Input.GetButtonDown("Crouch")) _slideBuffer = PlayerData.SlideBuffer;
        else if (Input.GetButtonDown("Sprint")) StateMachine.TransitionTo("Dash");
        else if (Player.Controller.isGrounded)
        {
            Player.CurrentFuel = PlayerData.MaxFuel;
            if (_jumpBuffer > 0) StateMachine.TransitionTo("Jump");
            else if (Input.GetButton("Crouch")) StateMachine.TransitionTo("Slide");
            else StateMachine.TransitionTo("Idle");
        }
    }
}
