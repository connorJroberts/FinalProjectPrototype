using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using UnityEngine;

public class Air : StateComponent
{

    private float _jumpBuffer = 0;

    public override void Enter(string msg = "")
    {
        _jumpBuffer = 0;
    }

    public override void FixedProcess()
    {
        _jumpBuffer -= Time.fixedDeltaTime;

        Vector3 hMovement = Quaternion.LookRotation(Actor.transform.forward, Vector3.up) * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * Actor.WalkSpeed * Time.fixedDeltaTime;

        Actor.Velocity.x = Mathf.Lerp(Actor.Velocity.x, hMovement.x, Actor.AirControlFactor);
        Actor.Velocity.z = Mathf.Lerp(Actor.Velocity.z, hMovement.z, Actor.AirControlFactor);

        Actor.Velocity.y += Actor.Gravity * Time.fixedDeltaTime * Time.fixedDeltaTime;
        Actor.Controller.Move(Actor.Velocity);

    }

    public override void Process()
    {
        if (Actor.Controller.collisionFlags == CollisionFlags.Sides && Input.GetAxisRaw("Vertical") == 1) StateMachine.TransitionTo("WallRun");
        else if (Input.GetButtonDown("Jump") && (Actor.CurrentJumpCount > 0)) StateMachine.TransitionTo("Jump");
        else if (Input.GetButtonDown("Jump")) _jumpBuffer = Actor.JumpBuffer;
        else if (Actor.Controller.isGrounded)
        {
            if (_jumpBuffer > 0) StateMachine.TransitionTo("Jump");
            else StateMachine.TransitionTo("Idle");
        }
    }

}
