using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : StateComponent
{
    private Vector3 hMove;

    public override void FixedProcess()
    {

        Actor.Velocity = Vector3.Lerp(Actor.Velocity, hMove * Actor.RunSpeed * Time.fixedDeltaTime, Time.fixedDeltaTime / Actor.MomentumFalloffTime);
        Actor.Controller.Move(Actor.Velocity);

        //Handle Fuel
        Actor.CurrentFuel += Actor.FuelRegenerationAmount * Actor.FuelRegenerationRate * Time.fixedDeltaTime;
        Actor.CurrentFuel = Mathf.Clamp(Actor.CurrentFuel, 0, Actor.MaxFuel);

    }

    public override void Process()
    {
        hMove = Quaternion.LookRotation(Actor.transform.forward, Vector3.up) * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        if (Actor.Velocity == Vector3.zero) StateMachine.TransitionTo("Idle");
        else if (Input.GetButtonDown("Jump")) StateMachine.TransitionTo("Jump");
        else if (Input.GetAxisRaw("Vertical") != 1) StateMachine.TransitionTo("Walk");
        else if (Input.GetButtonDown("Sprint") && Actor.CurrentFuel > 0) StateMachine.TransitionTo("Sprint");

    }
}
