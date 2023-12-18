using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerMovement;

//interfaz generica que puede heredar cualquier clase que quiera
public interface IDamageable
{
    public void TakeDamage(int damage, DamageEmiterType emiterType);
}
