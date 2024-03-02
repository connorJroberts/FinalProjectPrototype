using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{

    [SerializeField] private PlayerData _playerData;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private PlayerCameraRotation rotation;

    [SerializeField] private GameObject _camera;

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

    public string CurrentState = "";

    public ControllerColliderHit Collision { get; private set; }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Collision = hit;

    }

    private void Start()
    {
        if (IsOwner)
        {
            var cam = Instantiate(_camera, gameObject.transform);
            cam.TryGetComponent(out PlayerCameraRotation rot);
            rotation = rot;
            rot.PlayerData = _playerData;
        }
    }

}
