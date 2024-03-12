using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillY : MonoBehaviour
{
    [SerializeField]
    private GameObject SpawnPoint;

    [SerializeField]
    private float killZ = -1.0f;

    private Vector3 _resetPoint = new Vector3();

    void Awake()
    {

        _resetPoint = gameObject.transform.position;
        if (SpawnPoint != null)
        {
            _resetPoint = SpawnPoint.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y > killZ)
            return;

        // reset
        Debug.Log($"Time to reset to {_resetPoint}");
        transform.position = _resetPoint;
    }
}
