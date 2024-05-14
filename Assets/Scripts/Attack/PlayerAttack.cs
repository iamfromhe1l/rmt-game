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
    private List<Weapon> _weaponList = new();
    private Weapon _currentWeapon;
    private MagicFire _magicFire;
    private MeeleSword _meeleSword;
    private MeeleAxe _meeleAxe;
    private MagicWind _magicWind;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _magicFire = GetComponent<MagicFire>();
        _meeleSword = GetComponent<MeeleSword>();
        _meeleAxe = GetComponent<MeeleAxe>();
        _magicWind = GetComponent<MagicWind>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // && !_animator.GetBool(_currentWeapon._animationName)) { // :)
        {
            _magicWind.Attack();
        }
        if (Input.GetMouseButtonDown(1)) // && !_animator.GetBool(_currentWeapon._animationName)) { // :)
        {
            _meeleSword.Attack();
        }
    }
}
