using UnityEngine;
using Assets.Scripts.Interfaces;


public class TakingDamage : MonoBehaviour, IDamageable
{
    protected Health _health;
    protected int heal = 0;

    protected Animator _animator;

    protected void Awake()
    {
        if (GetComponent<Animator>() != null) { _animator = GetComponent<Animator>(); }
        _health = GetComponent<Health>();
        heal = _health._health;
    }
    public void Damage(int damage)
    {
        if (damage > 0)
        {
            heal -= damage;
            if (heal <= 0) { Die(); }
            if (_animator != null) { _animator.SetTrigger("IsTakingDamage"); }
            Debug.Log(heal);
        }
        else { Debug.Log("minus Damage"); }
    }
    virtual protected void Die()
    {
        _animator?.SetBool("Die", true);
        gameObject.GetComponent<Collider>().enabled = false;
        if (_animator == null)
        {
            gameObject.SetActive(false);
        }
    }
}