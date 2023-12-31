using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ataque del player principal el cual hereda atributos de attack ability y este a su vez de baseAbility
[CreateAssetMenu(fileName = "BasePrimaryAttackLong", menuName = "MicroDungeons/Abilities/BasePrimaryAttackLong")]
public class BasePrimaryAttackLong : AttackAbility
{

   
   //sobreescribe la animacion del startAbility del BasePrimaryAttack
    public override void StartAbility(AbilityCharacter character)
    {
        base.StartAbility(character);
        character.Animator.SetTrigger("PrimaryAttackLong");
        //calcular da�o total a ver si hay ataque aumentado por pasiva
        ParticlePlayerAttack(character);

    }

    //evento de animacion que se llama al atacar el player y hacer un trigger de animacion
    //hace da�o con spherecast
    public override void OnReceiveAnimationEvent(AbilityCharacter character)
    {


        Vector3 rayOrigin = character.transform.position + new Vector3(0f, 0.5f, 0f);

        RaycastHit[] sphereCastHitInfo = new RaycastHit[10];
        Debug.Log(character.CharacterStats.attackRange.runTimeValue);
        Physics.SphereCastNonAlloc(rayOrigin, attackRadius, character.transform.forward, sphereCastHitInfo, totalRangeAmount, attackLayerMask);
        if (sphereCastHitInfo.Length > 0)
        {

            for (int i = 0; i < sphereCastHitInfo.Length; i++)
            {
 
                if (sphereCastHitInfo[i].collider != null)
                {
                    IDamageable damageableObject = sphereCastHitInfo[i].collider.GetComponent<IDamageable>();
                    if (damageableObject != null)
                    {
            
                        Debug.DrawRay(sphereCastHitInfo[i].collider.transform.position, Vector3.up, Color.red, 2f);
                        //da�o de habilidad + da�o de player
                        //Debug.Log(totalDamageAmount);
                        damageableObject.TakeDamage(totalDamageAmount, damageEmiterType);

                    }
                }
            }
        }
    }


}
