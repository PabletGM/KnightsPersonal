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
    public int damageAmount;
    public float rangeAmount;
    //radio de ataque
    public float attackRadius;

    //a que puede atacar con layers
    public LayerMask attackLayerMask;
    //tipo de daño emitido
    public DamageEmiterType damageEmiterType;

    //cantidad de tiempo sin moverse tras hacer ataque
    [Header("Attack Movement Parameters")]
    public float movementStopTime;

    protected int totalDamageAmount;

    //para que el enemy sepa el range del player
    protected float totalRangeAmount;

    //añade funcionalidad al padre
    public override void StartAbility(AbilityCharacter character) {
        base.StartAbility(character);
        //mientras no haya acabado el tiempo de espera de quedrase quieto tras el aatque el enemy no se puede mover
        if(movementStopTime > 0f) {
            character.StopCharacterMovement();
        }
        //cantidad de daño * baseDamage de cada personaje(sea player o enemy)
        totalDamageAmount = damageAmount * character.CharacterStats.baseDamage.runTimeValue;
        totalRangeAmount = rangeAmount * character.CharacterStats.attackRange.runTimeValue;
        //Debug.Log(totalRangeAmount);
    }
}
