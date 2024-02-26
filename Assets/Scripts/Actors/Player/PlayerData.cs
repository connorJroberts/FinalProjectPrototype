using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : ActorData
{
    //Private Fields for Designer

    [SerializeField] private CharacterController controller;

    [Header("Move Speeds")]
    [SerializeField] private float _crouchSpeed = 1.0f;
    [SerializeField] private float _walkSpeed = 2f;
    [SerializeField] private float _runSpeed = 3f;
    [SerializeField] private float _sprintSpeed = 4f;
    [SerializeField] private float _wallRunSpeed = 3f;
    [SerializeField] private float _veritcalWallRunSpeed = 6f;

    [Header("Wall Run")]
    [SerializeField] private float _wallRunFalloffRate = 0.3f;
    [SerializeField] private float _verticalWallRunInitiationAngle = 15f;
    [SerializeField] private float _verticalWallRunFalloffRate = 0.08f;
    [SerializeField] private float _verticalWallRunForwardSpeedMultiplier = 0.5f;

    [Header("Slide")]
    [SerializeField] private float _slideDistance = 5f;
    [SerializeField] private float _slideTime = 0.5f;

    [Header("Jump")]
    [SerializeField] private float _jumpHeight = 1.5f;
    [SerializeField] private float _wallJumpHeight = 1.0f;
    [SerializeField] private float _wallJumpHorizontalForce = 6f;
    [SerializeField] private float _jumpTime = 0.5f;
    [SerializeField] private int _jumpCount = 2;

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
    [SerializeField] private float _jetpackFuelConsumptionAmount = 5f;
    [SerializeField] private float _sprintFuelConsumptionAmount = 5f;
    [SerializeField] private float _fuelRegenerationAmount = 3f;

    [Header("Options")] //TODO: Move to optionsdata in future
    [SerializeField] private float _mouseSensitivity = 0.1f;

    //Public References for Backend

    public CharacterController Controller => controller;

    public float CrouchSpeed => _crouchSpeed;
    public float WalkSpeed => _walkSpeed;
    public float RunSpeed => _runSpeed;
    public float SprintSpeed => _sprintSpeed;
    public float WallRunSpeed => _wallRunSpeed;
    public float VerticalWallRunSpeed => _veritcalWallRunSpeed;

    public float WallRunFalloffRate => _wallRunFalloffRate;
    public float VerticalWallRunInitiationAngle => _verticalWallRunInitiationAngle;
    public float VerticalWallRunFalloffRate => _verticalWallRunFalloffRate;
    public float VerticalWallRunForwardSpeedMulitplier => _verticalWallRunForwardSpeedMultiplier;

    public float SlideDistance => _slideDistance;
    public float SlideTime => _slideTime;

    public float JumpHeight => _jumpHeight;
    public float WallJumpHeight => _wallJumpHeight;
    public float WallJumpHorizontalForce => _wallJumpHorizontalForce;
    public float JumpTime => _jumpTime;
    public int JumpCount => _jumpCount;

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
    public float JetpackFuelConsumption => _jetpackFuelConsumptionAmount;
    public float SprintFuelConsumption => _sprintFuelConsumptionAmount;
    public float FuelRegenerationAmount => _fuelRegenerationAmount;
 
    public float MouseSensitivity => _mouseSensitivity;

    //Non Persistent Fields
    //TODO: Move to Player Controller Script

    public Vector3 Velocity = Vector3.zero;
    public float Gravity => -(8  * _jumpHeight / Mathf.Pow(_jumpTime, 2));
    public float JumpVelocity => (4 * _jumpHeight / _jumpTime);
    public float CurrentJumpCount = 0;
    public float CurrentFuel = 0;
    public ControllerColliderHit Collision { get; private set; }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Collision = hit;
    }

}
