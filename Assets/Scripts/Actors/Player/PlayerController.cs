using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private PlayerData _playerData;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private PlayerCameraRotation rotation;

    public PlayerCameraRotation CameraRotation => rotation;
    public PlayerData PlayerData => _playerData;
    public CharacterController Controller => _controller;
    public Vector3 Velocity = Vector3.zero;
    public float JumpVelocity => Mathf.Sqrt(2f * -PlayerData.Gravity * PlayerData.JumpHeight);
    public float WallJumpVelocity => Mathf.Sqrt(2f * -PlayerData.Gravity * PlayerData.WallJumpHeight);
    public float DashSpeedMultiplier => Mathf.Sqrt(Mathf.Pow(Velocity.magnitude, -PlayerData.DashSpeedMultiplier + 2));
    public float VerticalWallRunVelocity => Mathf.Sqrt(2f * -(PlayerData.Gravity * PlayerData.VerticalWallRunFalloffRate) * PlayerData.VerticalWallRunHeight);
    public float CurrentJumpCount = 0;
    public float CurrentDashCount = 1;
    public float CurrentFuel = 0;
    public float WallRunSpeed = 0;
    public ControllerColliderHit Collision { get; private set; }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Collision = hit;

    }

}
