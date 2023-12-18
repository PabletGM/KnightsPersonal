using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityCharacter : AbilityCharacter
{
    private PlayerMovement playerMovement;

    protected override void InitAbilityCharacter()
    {
        base.InitAbilityCharacter();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void OnPrimaryAttack()
    {
        ExecutePrimaryAbility();
    }

    public void OnMoveAbility()
    {
        playerMovement.CanMove = false;
        ExecuteMoveAbility();
    }

    protected override void ResetCurrentAbility()
    {
        if (!playerMovement.CanMove)
        {
            playerMovement.CanMove = true;
        }

        base.ResetCurrentAbility();
    }

    public override void EnableCharacterMovement() {
        base.EnableCharacterMovement();
        playerMovement.CanMove = true;
    }

    public override void StopCharacterMovement() {
        base.StopCharacterMovement();
        playerMovement.CanMove = false;
    }

    public void ReplacePrimaryAbility(AttackAbility newAbility)
    {
        if(currentAbility != null && currentAbility == slotAttackAbility)
        {
            ResetCurrentAbility();
        }
        slotAttackAbility = newAbility;
    }

    public override void TakeDamage(int damage, DamageEmiterType emiterType)
    {
        if (emiterType == DamageEmiterType.Player)
        {
            return;
        }

        PlayerManager.instance.CurrentHealth -= damage;
        if (PlayerManager.instance.CurrentHealth <= 0)
        {
            PlayerDied();            
        }
    }

    private void PlayerDied()
    {
        ResetCurrentAbility();
        canDoAbilties = false;
        playerMovement.CanMove = false;
        Animator.SetTrigger("IsDied");
    }
}
