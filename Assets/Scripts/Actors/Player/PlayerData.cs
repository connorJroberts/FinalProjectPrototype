using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : ActorData
{
    //Private Fields for Designer

    [SerializeField] private CharacterController controller;

    [SerializeField] private float _crouchSpeed = 1.0f;
    [SerializeField] private float _walkSpeed = 2f;
    [SerializeField] private float _runSpeed = 3f;
    [SerializeField] private float _sprintSpeed = 4f;
    [SerializeField] private float _wallRunSpeed = 3f;

    [SerializeField] private float _stateTransitionTime = 0.3f;

    [SerializeField] private float _slideDistance = 5f;
    [SerializeField] private float _slideTime = 0.5f;

    [SerializeField] private float _minJumpHeight = 0.5f;
    [SerializeField] private float _maxJumpHeight = 1.5f;
    [SerializeField] private float _wallJumpHeight = 1.0f;
    [SerializeField] private float _jumpTime = 0.5f;
    [SerializeField] private int _jumpCount = 2;

    [SerializeField] private float _jumpCurveRate = 1.5f;
    [SerializeField] private JumpCurves _jumpCurves;

    [SerializeField] private float _coyoteTime = 0.1f;
    [SerializeField] private float _jumpBuffer = 0.1f;

    [SerializeField] private float _momentumFalloffTime = 0.3f;
    [SerializeField] private float _airResistance = 0.1f;

    [SerializeField] private float _maxFuel = 100f;
    [SerializeField] private float _fuelConsumptionRate = 0.1f;
    [SerializeField] private float _jetpackFuelConsumptionAmount = 5f;
    [SerializeField] private float _sprintFuelConsumptionAmount = 5f;

    [SerializeField] private float _mouseSensitivity = 0.1f;

    //Public References for Backend

    public CharacterController Controller => controller;

    public float CrouchSpeed => _crouchSpeed;
    public float WalkSpeed => _walkSpeed;
    public float RunSpeed => _runSpeed;
    public float SprintSpeed => _sprintSpeed;
    public float WallRunSpeed => _wallRunSpeed;

    public float StateTransitionTime => _stateTransitionTime;

    public float SlideDistance => _slideDistance;
    public float SlideTime => _slideTime;

    public float MinJumpHeight => _minJumpHeight;
    public float MaxJumpHeight => _maxJumpHeight;
    public float WallJumpHeight => _wallJumpHeight;
    public float JumpTime => _jumpTime;
    public int JumpCount => _jumpCount;

    public float JumpCurveRate => _jumpCurveRate;
    public JumpCurves JumpCurves => _jumpCurves;

    public float CoyoteTime => _coyoteTime;
    public float JumpBufffer => _jumpBuffer;

    public float MomentumFalloffTime => _momentumFalloffTime;
    public float AirResistance => _airResistance;
 
    public float MaxFuel => _maxFuel;
    public float FuelConsumptionRate => _fuelConsumptionRate;
    public float JetpackFuelConsumption => _jetpackFuelConsumptionAmount;
    public float SprintFuelConsumption => _sprintFuelConsumptionAmount;
 
    public float MouseSensitivity => _mouseSensitivity;
}
