using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateComponent : MonoBehaviour
{
    //Base Class

    [SerializeField, ReadOnly] protected StateMachine StateMachine;
    [SerializeField, ReadOnly] protected PlayerData PlayerData;
    [SerializeField, ReadOnly] protected PlayerController Player;

    public void ConfigureState(StateMachine stateMachine, PlayerData playerData, PlayerController player)
    {
        StateMachine = stateMachine;
        PlayerData = playerData;
        Player = player;
    }

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