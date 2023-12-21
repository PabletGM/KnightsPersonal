using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerTrapEnemy : MonoBehaviour
{
    [SerializeField]
    private int damageAmount;


    

    private void OnTriggerEnter(Collider other)
    {

        BasicEnemyAbilityCharacter basicEnemy = other.GetComponent<BasicEnemyAbilityCharacter>();
        if (basicEnemy != null)
        {
            basicEnemy.TakeDamage(damageAmount, DamageEmiterType.World);
            Debug.Log("da�o hecho a enemy");
        }
    }
}
