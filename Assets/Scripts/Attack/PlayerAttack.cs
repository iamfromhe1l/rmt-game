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
    private static MagicFire _magicFire;
    private static MeeleSword _meeleSword;
    private static MeeleAxe _meeleAxe;
    private static MagicWind _magicWind;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _magicFire = GetComponent<MagicFire>();
        _meeleSword = GetComponent<MeeleSword>();
        _meeleAxe = GetComponent<MeeleAxe>();
        _magicWind = GetComponent<MagicWind>();
    }
    public static UpgradableParametr Upgrade(string weapon, string param)
    {
        UpgradableParametr result = new();
        switch(weapon) 
        {
            case "sword": 
                {
                    result = _meeleSword.Upgrade(param); 
                    break;
                }
            case "axe":
                {
                    result = _meeleAxe.Upgrade(param);
                    break;
                }
            case "fire":
                {
                    result = _magicFire.Upgrade(param);
                    break;
                }
            case "wind":
                {
                    result = _magicWind.Upgrade(param);
                    break;
                }
        }
        return result;
    }
}
