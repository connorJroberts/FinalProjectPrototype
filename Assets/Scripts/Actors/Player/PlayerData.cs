using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : ActorData
{
    //Private Fields for Designer

    [SerializeField] private PlayerCameraRotation rotation;

    [Header("Move Speeds")]
    [SerializeField] private float _crouchSpeed = 1.0f;
    [SerializeField] private float _walkSpeed = 2f;
    [SerializeField] private float _runSpeed = 3f;
    [SerializeField] private float _sprintSpeed = 4f;
    [SerializeField] private float _wallRunSpeed = 3f;

    [Header("Wall Run")]
    [SerializeField] private float _horizontalWallRunFalloffRate = 0.3f;
    [SerializeField] private float _verticalWallClimbInitiationAngle = 15f;
    [SerializeField] private float _verticalWallClimbFalloffRate = 0.08f;
    [SerializeField] private float _verticalWallClimbForwardSpeedMultiplier = 0.5f;
    [SerializeField] private float _verticalWallClimbHeight = 6f;

    [Header("Dash")]
    [SerializeField] private float _dashDistance = 5f;


    [Header("Slide")]
    [SerializeField] private float _slideBuffer = 0.2f;

    [Header("Jump")]
    [SerializeField] private float _jumpHeight = 1.5f;
    [SerializeField] private float _wallJumpHeight = 1.0f;
    [SerializeField] private float _wallJumpHorizontalForce = 6f;
    [SerializeField] private int _jumpCount = 2;
    [SerializeField] private float _gravity = -10f;

    [Header("Jump Curve")]
    [SerializeField] private float _jumpCurveRate = 1.5f;
    [SerializeField] private JumpCurves _jumpCurves;

    [Header("Affordance")]
    [SerializeField] private float _coyoteTime = 0.1f;
    [SerializeField] private float _jumpBuffer = 0.1f;

    [Header("Momentum")]
    [SerializeField] private float _momentumFalloffTime = 0.3f;
    [SerializeField] private float _airResistance = 0.1f;
    [SerializeField, Range(0f, 1f)] private float _airControlFactor = 0.3f;

    [Header("Fuel")]
    [SerializeField] private float _maxFuel = 100f;
    [SerializeField] private float _fuelConsumptionRate = 0.1f;
    [SerializeField] private float _fuelRegenerationRate = 0.1f;
    [SerializeField] private float _dashFuelConsumptionAmount = 5f;
    [SerializeField] private float _sprintFuelConsumptionAmount = 5f;
    [SerializeField] private float _fuelRegenerationAmount = 3f;

    [Header("Options")] //TODO: Move to optionsdata in future
    [SerializeField] private float _mouseSensitivity = 0.1f;
    [SerializeField] private float _defaultCameraHeight = 2f;

    //Public References for Backend

    public PlayerCameraRotation CameraRotation => rotation;

    public float CrouchSpeed => _crouchSpeed;
    public float WalkSpeed => _walkSpeed;
    public float RunSpeed => _runSpeed;
    public float SprintSpeed => _sprintSpeed;
    public float WallRunSpeed => _wallRunSpeed;

    public float HorizontalWallRunFalloffRate => _horizontalWallRunFalloffRate;
    public float VerticalWallRunInitiationAngle => _verticalWallClimbInitiationAngle;
    public float VerticalWallRunFalloffRate => _verticalWallClimbFalloffRate;
    public float VerticalWallRunForwardSpeedMulitplier => _verticalWallClimbForwardSpeedMultiplier;
    public float VerticalWallRunHeight => _verticalWallClimbHeight;

    public float SlideBuffer => _slideBuffer;

    public float JumpHeight => _jumpHeight;
    public float WallJumpHeight => _wallJumpHeight;
    public float WallJumpHorizontalForce => _wallJumpHorizontalForce;
    public int JumpCount => _jumpCount;
    public float Gravity => _gravity;

    public float JumpCurveRate => _jumpCurveRate;
    public JumpCurves JumpCurves => _jumpCurves;

    public float CoyoteTime => _coyoteTime;
    public float JumpBuffer => _jumpBuffer;

    public float MomentumFalloffTime => _momentumFalloffTime;
    public float AirResistance => _airResistance;
    public float AirControlFactor => _airControlFactor;
 
    public float MaxFuel => _maxFuel;
    public float FuelConsumptionRate => _fuelConsumptionRate;
    public float FuelRegenerationRate => _fuelRegenerationRate;
    public float DashFuelConsumption => _dashFuelConsumptionAmount;
    public float SprintFuelConsumption => _sprintFuelConsumptionAmount;
    public float FuelRegenerationAmount => _fuelRegenerationAmount;
 
    //TODO: Move to Settings Data
    public float MouseSensitivity => _mouseSensitivity;
    public float DefaultCameraHeight => _defaultCameraHeight;

}
