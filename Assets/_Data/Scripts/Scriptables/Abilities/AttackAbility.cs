using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//hereda de base ability y da funcionalidad a su hijo base primary attack, el cual contiene los parametros de ataque del enemy y player
public class AttackAbility : BaseAbility
{
    //parametros de ataque
    [Header("Attack Parameters")]
    //cantidad de daño
    public FloatVariable damageAmount;
    //radio de ataque
    public float attackRadius;
    //radio de rango
    public float attackRange;
    //a que puede atacar con layers
    public LayerMask attackLayerMask;
    //tipo de daño emitido
    public DamageEmiterType damageEmiterType;

    //cantidad de tiempo sin moverse tras hacer ataque
    [Header("Attack Movement Parameters")]
    public float movementStopTime;

    //añade funcionalidad al padre
    public override void StartAbility(AbilityCharacter character) {
        base.StartAbility(character);
        //mientras no haya acabado el tiempo de espera de quedrase quieto tras el aatque el enemy no se puede mover
        if(movementStopTime > 0f) {
            character.StopCharacterMovement();
        }
    }
}
