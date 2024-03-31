using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;

public class AttackArea : MonoBehaviour
{
    public List<IDamageable> Damageables { get; } = new();

    public void OnTriggerEnter(Collider other)
    {
        var damagable = other.GetComponent<IDamageable>();
        if (damagable != null)
        {
            Damageables.Add(damagable);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        var damageable = other.GetComponent<IDamageable>();
        if (damageable != null && Damageables.Contains(damageable)) 
        {
            Damageables.Remove(damageable);
        }
    }

}
