using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerView : NetworkBehaviour
{

    [SerializeField] private PlayerController _controller;
    public PlayerController Controller => _controller;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
