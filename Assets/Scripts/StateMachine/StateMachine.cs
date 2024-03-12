using GameEvents;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine.Events;
using UnityEngine;

public class StateMachine : NetworkBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private PlayerController player;
    [SerializeField] private string _initialState;
    [SerializeField] private E_PlayerStatesEventAsset _stateEvent;
    private State _currentState;

    public UnityEvent<E_PlayerStates> OnStateChange;

    public override void OnNetworkSpawn()
    {
        if(!IsOwner) Destroy(this);
    }

    void Awake()
    {
        SetupState((State)Activator.CreateInstance(Type.GetType(_initialState))); //Create an instance of the initial state
    }

    void Update()
    {
        _currentState.Process(); //Run the state equivalent of Update
    }

    private void FixedUpdate()
    {
        _currentState.FixedProcess(); //Run the staate equivalent of FixedUpdate
    }

    public void TransitionTo(State state, string exitMessage = "")
    {
        _currentState.Exit(); //Exit Current State
        if (state != null)
        {
            SetupState(state);
        }
        _currentState.Enter(exitMessage); //Enter the Next State

        OnStateChange.Invoke(player.CurrentState);

    }

    private void SetupState(State state) //Pass Data into State
    {
        _currentState = state;
        _currentState.ConfigureState(this, playerData, player);
        player.CurrentState = Enum.Parse<E_PlayerStates>(_currentState.GetType().Name);
    }
}
