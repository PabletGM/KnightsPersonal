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
        //base.StartAbility(character);
        character.Animator.SetTrigger("PrimaryAttackLong");
        //calcular daño total a ver si hay ataque aumentado por pasiva
        
    }

    //evento de animacion que se llama al atacar el player y hacer un trigger de animacion
    //hace daño con spherecast
    public override void OnReceiveAnimationEvent(AbilityCharacter character)
    {

        Debug.Log("1");
        Vector3 rayOrigin = character.transform.position + new Vector3(0f, 0.5f, 0f);

        RaycastHit[] sphereCastHitInfo = new RaycastHit[10];
        Physics.SphereCastNonAlloc(rayOrigin, attackRadius, character.transform.forward, sphereCastHitInfo, attackRange, attackLayerMask);
        if (sphereCastHitInfo.Length > 0)
        {
            Debug.Log("2");
            for (int i = 0; i < sphereCastHitInfo.Length; i++)
            {
                Debug.Log("3");
                if (sphereCastHitInfo[i].collider != null)
                {
                    IDamageable damageableObject = sphereCastHitInfo[i].collider.GetComponent<IDamageable>();
                    if (damageableObject != null)
                    {
                        Debug.Log("4");
                        Debug.DrawRay(sphereCastHitInfo[i].collider.transform.position, Vector3.up, Color.red, 2f);
                        //daño de habilidad + daño de player
                        //Debug.Log(totalDamageAmount);
                        damageableObject.TakeDamage(totalDamageAmount, damageEmiterType);

                    }
                }
            }
        }
    }


}
