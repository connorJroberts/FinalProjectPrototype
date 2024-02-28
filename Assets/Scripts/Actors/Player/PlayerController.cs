using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private PlayerData _playerData;
    [SerializeField] private CharacterController _controller;

    public PlayerData PlayerData => _playerData;
    public CharacterController Controller => _controller;
    public Vector3 Velocity = Vector3.zero;
    public float JumpVelocity => Mathf.Sqrt(2f * -PlayerData.Gravity * PlayerData.JumpHeight);
    public float WallJumpVelocity => Mathf.Sqrt(2f * -PlayerData.Gravity * PlayerData.WallJumpHeight);
    public float VerticalWallRunVelocity => Mathf.Sqrt(2f * -(PlayerData.Gravity * PlayerData.VerticalWallRunFalloffRate) * PlayerData.VerticalWallRunHeight);
    public float CurrentJumpCount = 0;
    public float CurrentFuel = 0;
    public ControllerColliderHit Collision { get; private set; }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Collision = hit;
    }
}
