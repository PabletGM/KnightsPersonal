using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerTrapEnemy : MonoBehaviour
{
    [SerializeField]
    private int damageAmount;


    

    private void OnTriggerStay(Collider other)
    {

        BasicEnemyAbilityCharacter basicEnemy = other.GetComponent<BasicEnemyAbilityCharacter>();
        if (basicEnemy != null)
        {
            basicEnemy.TakeDamage(damageAmount, DamageEmiterType.World);
            Debug.Log("daño hecho a enemy");
        }
    }
}
