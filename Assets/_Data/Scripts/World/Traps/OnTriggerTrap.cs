using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//daño de trampa al pisarlo algun character con navmesh ??? o playerAbility
public class OnTriggerTrap : MonoBehaviour
{
    [SerializeField]
    private int damageAmount = 1;


    //isKinematic es para quitarle el motor de fisicas al rigidbody, no entra el enemigo al OnTriggerEnter porque no tiene rigidbody
    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("daño");
    //    PlayerAbilityCharacter playerAbilityCharacter = other.GetComponent<PlayerAbilityCharacter>();
    //    NavMeshAgent navmesh = other.GetComponent<NavMeshAgent>();
    //    if (playerAbilityCharacter != null)
    //    {
    //        playerAbilityCharacter.TakeDamage(damageAmount, DamageEmiterType.World);
    //    }
    //    else if(navmesh != null)
    //    {
    //        other.GetComponent<BasicEnemyAbilityCharacter>().TakeDamage(damageAmount, DamageEmiterType.World);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damageAmount, DamageEmiterType.World);
        }
    }
}
