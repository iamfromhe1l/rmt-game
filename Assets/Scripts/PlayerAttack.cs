using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator _animator;
    [SerializeField]
    private int Damage;
    [SerializeField]
    private AttackArea AttackArea;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_animator.GetBool("SimpleAttack")) { // :)
            _animator.SetTrigger("SimpleAttack");
            Invoke("SimpleAttack",0.2f);
        }
    }
    private void SimpleAttack()
    {
        foreach(var attackAreaDamageable in AttackArea.Damageables) 
        {
            attackAreaDamageable.Damage(Damage);
        }
    }
}
