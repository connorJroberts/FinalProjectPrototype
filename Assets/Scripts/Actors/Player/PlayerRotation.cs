using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField, ReadOnly] private PlayerData _playerData;

    private void OnValidate()
    {
        TryGetComponent<PlayerData>(out _playerData);
    }

    void Update()
    {
        transform.Rotate(transform.up, Input.GetAxisRaw("Mouse X") * _playerData.MouseSensitivity);
    }
}
