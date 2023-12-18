using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//para conseguir el player pasivas cogiendolas
public class PassiveAbilityHolder : MonoBehaviour
{
    [SerializeField]
    private BaseAbility PassivAbility;

    private void OnTriggerEnter(Collider other)
    {
        PlayerAbilityCharacter character = other.GetComponent<PlayerAbilityCharacter>();
        if (character != null)
        {
            character.AddPassiveAbility(PassivAbility);
            Destroy(gameObject);
        }
    }
}
