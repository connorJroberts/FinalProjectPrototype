using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : StateComponent
{
    private Vector3 _initialPlayerVelocity;
    private Vector3 _initialPlayerRight;

    public override void Enter(string msg = "")
    {
        _initialPlayerRight = Player.transform.right;
        Vector3 groundComponent = Quaternion.AngleAxis(90, _initialPlayerRight) * Player.Collision.normal; //Using to store the vector projection magnitude for velocity maintenance
       Debug.DrawLine(Player.transform.position, Player.transform.position + groundComponent, Color.red, 5.0f);
        _initialPlayerVelocity = Vector3.Project(Player.Velocity, groundComponent);

        //Player.transform.position -= new Vector3(0, 1, 0);
        PlayerData.CameraRotation.gameObject.transform.localPosition = new Vector3(0,1,0);

    }

    public override void FixedProcess()
    {
        Vector3 groundComponent = Quaternion.AngleAxis(90, _initialPlayerRight) * Player.Collision.normal - Player.Collision.normal * 0.1f; //Using stored velocity magnitude with normalized ground 
        Player.Velocity = _initialPlayerVelocity.magnitude * groundComponent.normalized;                          //component to ensure velocity doesn't change on new angle surface
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
