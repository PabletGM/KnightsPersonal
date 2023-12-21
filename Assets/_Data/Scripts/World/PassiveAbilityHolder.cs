using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//para conseguir el player pasivas cogiendolas
public class PassiveAbilityHolder : MonoBehaviour
{
    [SerializeField]
    private BaseAbility PassiveAbilityPlayer;

    [SerializeField]
    private BaseAbility PassiveAbilityEnemy;

    private void OnTriggerEnter(Collider other)
    {
        PlayerAbilityCharacter character = other.GetComponent<PlayerAbilityCharacter>();
        BasicEnemyAbilityCharacter basicEnemy = other.GetComponent<BasicEnemyAbilityCharacter>();
        if (character != null && PassiveAbilityPlayer!=null)
        {
            character.AddPassiveAbility(PassiveAbilityPlayer);
            Destroy(gameObject);
        }
        else if(basicEnemy != null && PassiveAbilityEnemy!=null) 
        {
            basicEnemy.AddPassiveAbility(PassiveAbilityEnemy);
            Destroy(gameObject);
        }
    }
}
