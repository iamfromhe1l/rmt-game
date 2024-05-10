using UnityEngine;
using Assets.Scripts.Interfaces;


public class TakingDamage : MonoBehaviour, IDamageable
{
    private Health _health;
    private int heal = 0;

    private Animator _animator;

    private void Awake()
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
            if (_animator != null) { _animator.SetTrigger("TakeDamage"); }
        }
        else { Debug.Log("minus Damage"); }
    }
    private void Die()
    {
        _animator?.SetBool("Die", true);
        gameObject.GetComponent<Collider>().enabled = false;
        if (_animator == null)
        {
            gameObject.SetActive(false);
        }
    }
}