using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{

    [Header("Bullet Data")]
    [SerializeField] private float _bulletDamage;
    [SerializeField] private float _bulletSpeed;

    [Header("Spread")]
    [SerializeField] private float _bulletCount;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _horizontalSpread;
    [SerializeField] private float _verticalSpread;

    [Header("Optimization")]
    [SerializeField] private float _bulletDespawnRange;

    [Header("Forces")]
    [SerializeField] private Vector2 _playerKickbackForce;

    [Header("Recoil")]
    [SerializeField] private AnimationCurve _horizontalRecoilTimeCurve;
    [SerializeField] private AnimationCurve _verticalRecoilTimeCurve;

    [Header("Reload")]
    [SerializeField] private float _magSize;
    [SerializeField] private float _reloadSpeed;
    [SerializeField] private bool _automatic;

    [Header("BulletType")]
    [SerializeField] private bool _hitscan;

    [Header("Aim Down Sights")]
    [SerializeField] private bool _ADS;
    [SerializeField] private float _ADSSpeed;
    [SerializeField] private float _ADSZoom;

}
