using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraRotation : MonoBehaviour
{

    [SerializeField] private PlayerData _playerData;

    private void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {



        transform.Rotate(Vector3.right, -Input.GetAxisRaw("Mouse Y") * _playerData.MouseSensitivity);
        transform.localRotation = new Quaternion(Mathf.Clamp(transform.localRotation.x, -0.7f, 0.7f), 0, 0, transform.localRotation.w);
    }
}
