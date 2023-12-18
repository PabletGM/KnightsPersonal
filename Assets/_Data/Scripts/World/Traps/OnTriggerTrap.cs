using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//daño de trampa al pisarlo algun character con navmesh ??? o playerAbility
public class OnTriggerTrap : MonoBehaviour
{
    [SerializeField]
    private int damageAmount = 1;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("daño");
        PlayerAbilityCharacter playerAbilityCharacter = other.GetComponent<PlayerAbilityCharacter>();
        NavMeshAgent navmesh = other.GetComponent<NavMeshAgent>();
        if (playerAbilityCharacter != null)
        {
            playerAbilityCharacter.TakeDamage(damageAmount, DamageEmiterType.World);
        }
        else if(navmesh != null)
        {
            other.GetComponent<BasicEnemyAbilityCharacter>().TakeDamage(damageAmount, DamageEmiterType.World);
        }
    }
}
