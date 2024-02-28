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
        transform.rotation *= Quaternion.AngleAxis(Input.GetAxisRaw("Mouse X") * _playerData.MouseSensitivity, Vector3.up);
    }
}
