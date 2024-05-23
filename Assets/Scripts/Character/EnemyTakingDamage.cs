using Assets.Scripts.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTakingDamage : TakingDamage
{
    override protected void Die()
    {
        gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        _animator?.SetBool("Die", true);
        gameObject.GetComponent<Collider>().enabled = false;
        if (_animator == null)
        {
            gameObject.SetActive(false);
        }
        gameObject.GetComponent<Enemy>().IsDied = true;
    }
}
