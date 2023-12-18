using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAbilityItemHolder : MonoBehaviour
{
    [SerializeField]
    private AttackAbility AbilityToChange;

    private void OnTriggerEnter(Collider other)
    {
        PlayerAbilityCharacter abilityCharacter = other.GetComponent<PlayerAbilityCharacter>();
        if(abilityCharacter != null)
        {
            abilityCharacter.ReplacePrimaryAbility(AbilityToChange);
            Debug.Log("hola");
            this.gameObject.SetActive(false);
        }
    }
}
