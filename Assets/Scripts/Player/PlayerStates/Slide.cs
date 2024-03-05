using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : State
{
    private Vector3 _initialPlayerVelocity;

    public override void Enter(string msg = "")
    {
        Vector3 project = Vector3.Project(Player.Velocity, Player.Collision.normal);
        Vector3 groundComponent = Player.Velocity - project;
        _initialPlayerVelocity = groundComponent;

        Player.CameraRotation.gameObject.transform.localPosition = new Vector3(0,1,0);
    }

    public override void FixedProcess()
    {
        Vector3 project = Vector3.Project(Player.Velocity, Player.Collision.normal);
        Vector3 groundComponent = (Player.Velocity - project).normalized * _initialPlayerVelocity.magnitude;

        Player.Controller.Move(Player.Velocity);
    }

    public override void Process()
    {
        if (!Input.GetButton("Crouch")) StateMachine.TransitionTo(new Run());
        else if (Player.Controller.collisionFlags == CollisionFlags.Sides) StateMachine.TransitionTo(new Run());
        else if (Input.GetButtonDown("Jump")) StateMachine.TransitionTo(new Jump(), "SlideComplete");
        else if (!Player.Controller.isGrounded) StateMachine.TransitionTo(new Air());
    }

    public override void Exit()
    {
        Player.CameraRotation.gameObject.transform.localPosition = new Vector3(0, 2, 0);
    }

}
