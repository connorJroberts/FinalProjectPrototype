using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillY : MonoBehaviour
{
    [SerializeField] private float _killYHeight;
    [SerializeField] private Vector3 _spawn;

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < _killYHeight)
        {
            gameObject.transform.position = _spawn;
        }
    }
}
