using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//no hereda directamente de abilityCharacter pero las 2 habilidades de abilitCharacter(slotMove y slotAtack son baseAbility
public class BaseAbility : ScriptableObject
{
    //para distinguir si son pasivas o no 
    [Header("BaseAbility Parameters")]
    public bool isPassive = false;

    // 0 means instant ability, esto es pasiva
    public float duration = 0f;
    public float cooldown = 0f;


    //inician todos los metodos como virtual y los hijos pueden acceder a ellos y modificarlos a su antojo
    public virtual void StartAbility(AbilityCharacter character)
    {

    }

    public virtual void UpdateAbility(AbilityCharacter character, float deltaTime, float elapsedTime)
    {

    }

    public virtual void FixedUpdateAbility(AbilityCharacter character, float fixedDeltaTime, float elapsedTime)
    {

    }

    public virtual void OnReceiveAnimationEvent(AbilityCharacter character)
    {

    }

    public virtual void EndAbility(AbilityCharacter character)
    {

    }
}
