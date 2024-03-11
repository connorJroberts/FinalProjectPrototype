using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    [SerializeField] public PlayerData PlayerData;

    private void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        transform.Rotate(Vector3.right, -Input.GetAxisRaw("Mouse Y") * PlayerData.MouseSensitivity);
        transform.localRotation = new Quaternion(Mathf.Clamp(transform.localRotation.x, -0.7f, 0.7f), 0, transform.localRotation.z, transform.localRotation.w);
    }
}
