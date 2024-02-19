using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateComponent : MonoBehaviour
{
    //Base Class

    [SerializeField, ReadOnly] public StateMachine StateMachine;
    [SerializeField, ReadOnly] public PlayerData Actor;

    virtual public void Enter(string msg = "")
    {
        //Start Equivalent for State
    }

    virtual public void Process()
    {
        //Update Equivalent for State
    }

    virtual public void FixedProcess()
    {
        //FixedUpdate Equivalent for State
    }

    virtual public void Exit()
    {
        //OnDestroy Equivalent for State
    }
}