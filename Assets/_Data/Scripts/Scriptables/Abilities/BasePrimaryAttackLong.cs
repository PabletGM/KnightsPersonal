using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ataque del player principal el cual hereda atributos de attack ability y este a su vez de baseAbility
[CreateAssetMenu(fileName = "BasePrimaryAttack2", menuName = "MicroDungeons/Abilities/BasePrimaryAttack2")]
public class BasePrimaryAttackLong : BasePrimaryAttack
{

   
   //sobreescribe la animacion del startAbility del BasePrimaryAttack
    public override void StartAbility(AbilityCharacter character)
    {
        base.StartAbility(character);
        character.Animator.SetTrigger("PrimaryAttackLong");
        //calcular daño total a ver si hay ataque aumentado por pasiva
        
    }

    //evento de animacion que se llama al atacar el player y hacer un trigger de animacion
    //hace daño con spherecast
    public override void OnReceiveAnimationEvent(AbilityCharacter character)
    {
        base.OnReceiveAnimationEvent(character);
    }


}
