using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashIndicator : MonoBehaviour
{
    PlayerView _playerView;
    [SerializeField] GameObject _dodgeIndicator;

    private void Start()
    {
        _playerView = GetComponentInParent<PlayerView>();
    }
    private void Update()
    {
        if (_playerView == null) return;
        if (_dodgeIndicator == null) return;
        if (_playerView.CurrentDashCount > 0)
        {
            _dodgeIndicator.SetActive(true);
        }
        else
        {
            _dodgeIndicator.SetActive(false);
        }
    }
}
