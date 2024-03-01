using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class BoostPad : MonoBehaviour 
{

    [SerializeField] private float _playerSpeedMultiplier = 1f;

    private void OnValidate()
    {
        TryGetComponent(out BoxCollider box);
        box.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.TryGetComponent(out PlayerController player);
        if (player != null)
        {
            player.Velocity.x *= _playerSpeedMultiplier;
            player.Velocity.z *= _playerSpeedMultiplier;
            player.WallRunSpeed *= _playerSpeedMultiplier;
        }
    }
}
