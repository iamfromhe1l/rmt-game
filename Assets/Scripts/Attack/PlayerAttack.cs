using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;
using Assets.Scripts.River;

public class PlayerAttack : MonoBehaviour
{
    private Animator _animator;
    [SerializeField]
    private int _damage = 10;
    [SerializeField]
    private DamageArea AttackArea;
    private List<Weapon> _weaponList = new();
    private Weapon _currentWeapon;
    private IDamageable damageable;
    private MagicFire _magicFire;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _magicFire = GetComponent<MagicFire>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // && !_animator.GetBool(_currentWeapon._animationName)) { // :)
        {
            _magicFire.Attack(_damage);
        }
    }
}
