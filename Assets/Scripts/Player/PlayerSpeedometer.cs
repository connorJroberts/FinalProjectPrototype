using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerSpeedometer : MonoBehaviour
{
    PlayerView _playerView;
    [SerializeField] TextMeshProUGUI _speedometerText;

    private void Start()
    {
        _playerView = GetComponentInParent<PlayerView>();
    }
    private void Update()
    {
        if (_playerView == null) return;
        if (_speedometerText == null) return;

        _speedometerText.text = (Mathf.RoundToInt((_playerView.Controller.Velocity.magnitude)*100)).ToString();
    }
}
