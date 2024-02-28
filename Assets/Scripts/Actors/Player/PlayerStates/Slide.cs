using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : StateComponent
{
    private Vector3 _initialPlayerVelocity;

    public override void Enter(string msg = "")
    {
        Vector3 project = Vector3.Project(Player.Velocity, Player.Collision.normal);
        Vector3 groundComponent = Player.Velocity - project;
        _initialPlayerVelocity = groundComponent;

        PlayerData.CameraRotation.gameObject.transform.localPosition = new Vector3(0,1,0);
    }

    public override void FixedProcess()
    {
        Vector3 project = Vector3.Project(Player.Velocity, Player.Collision.normal);
        Vector3 groundComponent = (Player.Velocity - project).normalized * _initialPlayerVelocity.magnitude;

        Player.Controller.Move(Player.Velocity);
    }

    public override void Process()
    {
        if (!Input.GetButton("Crouch")) StateMachine.TransitionTo("Run");
        else if (Input.GetAxisRaw("Vertical") != 1) StateMachine.TransitionTo("Idle");
        else if (Player.Controller.collisionFlags == CollisionFlags.Sides) StateMachine.TransitionTo("Run");
        else if (Input.GetButtonDown("Jump")) StateMachine.TransitionTo("Jump", "SlideComplete");
        else if (!Player.Controller.isGrounded) StateMachine.TransitionTo("Air");
    }

    public override void Exit()
    {
        PlayerData.CameraRotation.gameObject.transform.localPosition = new Vector3(0, 2, 0);
    }

}
