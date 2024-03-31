using UnityEngine;
using Assets.Scripts.Interfaces;


public class TakingDamage : MonoBehaviour, IDamageable
{
    private Health _health;
    private int heal = 0;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
        heal = _health._health;
    }
    public void Damage(int damage)
    {
        if (damage > 0) 
        {
            Debug.Log(heal);
            heal -= damage;
            Debug.Log(damage);
            Debug.Log(heal);
            if (heal <= 0) { Die(); }
            _animator.SetTrigger("TakeDamage");
        }
        else { Debug.Log("отрицательный урон"); }
    }
    public void Heal(int heal) { }
    private void Die()
    {
        gameObject.SetActive(false);
    }
}
