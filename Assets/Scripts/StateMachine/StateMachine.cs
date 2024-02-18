using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] public PlayerData actorData;
    [SerializeField] private string _initialState;
    private StateComponent _currentState;

    private Dictionary<string, StateComponent> _states = new Dictionary<string, StateComponent>();

    void OnValidate()
    {
        StateComponent[] stateComponents = GetComponents<StateComponent>(); //Pass data into initial state
        foreach (StateComponent component in stateComponents)
        {
            Debug.Log(component.GetType().Name);
            _states.Add(component.GetType().Name, component);
        }

        SetupState(_initialState);
    }

    void Update()
    {
        _currentState.Process(); //Run the state equivalent of Update
    }

    private void FixedUpdate()
    {
        _currentState.FixedProcess(); //Run the staate equivalent of FixedUpdate
    }

    public void TransitionTo(string stateName = "", string exitMessage = "")
    {
        StateComponent state = _states[stateName];

        _currentState.Exit(); //Exit Current State

        if (state != null)
        {
            SetupState(stateName);
        }

        _currentState.Enter(exitMessage); //Enter the Next State

    }

    private void SetupState(string stateName) //Pass Data into State
    {
        _currentState = _states[stateName];
        _currentState.StateMachine = this;
        _currentState.Actor = actorData;
    }
}
