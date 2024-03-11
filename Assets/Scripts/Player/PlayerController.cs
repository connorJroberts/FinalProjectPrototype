using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{

    [SerializeField] private PlayerData _playerData;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private PlayerCameraRotation _rotation;

    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _hud;

    public PlayerCameraRotation CameraRotation => _rotation;
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

    public E_PlayerStates CurrentState = 0;

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
            _rotation = rot;
            rot.PlayerData = _playerData;

            var hud = Instantiate(_hud, gameObject.transform);
        }
    }

    public void HandleRotation(float targetRot)
    {
        StartCoroutine(HandleRotationRoutine(targetRot));
    }

    private IEnumerator HandleRotationRoutine(float targetRot)
    {
        while (!Mathf.Approximately(Mathf.Round(Mathf.Rad2Deg * CameraRotation.transform.rotation.z) , targetRot))
        {
            CameraRotation.transform.localRotation = new Quaternion()
            {
                x = CameraRotation.transform.localRotation.x,
                y = 0,
                z = Mathf.LerpAngle(CameraRotation.transform.localRotation.z, Mathf.Deg2Rad * targetRot, 5 * Time.deltaTime),
                w = CameraRotation.transform.localRotation.w,
            };
            yield return null ;
        }

    }

}
