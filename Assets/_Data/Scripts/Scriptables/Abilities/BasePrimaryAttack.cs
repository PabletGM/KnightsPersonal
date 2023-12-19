using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ataque del player principal el cual hereda atributos de attack ability y este a su vez de baseAbility
[CreateAssetMenu(fileName = "BasePrimaryAttack", menuName = "MicroDungeons/Abilities/BasePrimaryAttack")]
public class BasePrimaryAttack : AttackAbility
{
    //añade funcionalidad a el padre ejecutando el startAbility y ejecutando la animacion de ataque del player y accediendo a su animator y activando el trigger
    public override void StartAbility(AbilityCharacter character)
    {
        base.StartAbility(character);
        character.Animator.SetTrigger("PrimaryAttack");
    }

    //evento de animacion que se llama al atacar el player y hacer un trigger de animacion
    //hace daño con spherecast
    public override void OnReceiveAnimationEvent(AbilityCharacter character)
    {
        Vector3 rayOrigin = character.transform.position + new Vector3(0f, 0.5f, 0f);

        RaycastHit[] sphereCastHitInfo = new RaycastHit[10];
        Physics.SphereCastNonAlloc(rayOrigin, attackRadius, character.transform.forward, sphereCastHitInfo, attackRange, attackLayerMask);
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
                        damageableObject.TakeDamage(Convert.ToInt32(damageAmount.runTimeValue), damageEmiterType);
                        Debug.Log(damageAmount.runTimeValue);
                    }
                }
            }
        }
    }
}
