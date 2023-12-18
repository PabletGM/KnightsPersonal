using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//hereda de DamageableObject por lo que sus comportamientos excepto efectos vfx los hereda ???
public class HealthDamageableObject : DamageableObject
{
    //posee las vidas que quieras y le pongas en el inspector
    public int health = 3;

    public override void TakeDamage(int damage, DamageEmiterType emiterType)
    {
        //se le quita vida
        health -= damage;
        //sino le quedan vidas adquiere funcionalidad de su padre
        if (health <= 0)
        {
            base.TakeDamage(damage, emiterType);
        }
    }


}
