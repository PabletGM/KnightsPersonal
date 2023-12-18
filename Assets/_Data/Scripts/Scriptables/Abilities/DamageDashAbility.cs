using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
//habilidad principal del player de movimiento
[CreateAssetMenu(fileName ="DamageDashAbility", menuName = "MicroDungeons/Abilities/DamageDashAbility")]
public class DamageDashAbility : BaseDashAbility
{
    //hereda toda la funcionalidad del baseDashAbility
    [Header("Damage Parameters")]
    public int ticks;

    public int damageAmount;
    public float damageRadius;
    public float damageRange;

    public LayerMask damageLayerMask;
    public DamageEmiterType damageEmiterType;

    //???
    private float interval;


    //inicia habilidad y añade funcionalidad
    public override void StartAbility(AbilityCharacter character)
    {
        base.StartAbility(character);
        interval = duration / ticks;
    }

    //???
    public override void UpdateAbility(AbilityCharacter character, float deltaTime, float elapsedTime)
    {
        base.UpdateAbility(character, deltaTime, elapsedTime);
        if (elapsedTime % interval <= deltaTime)
        {
            if((elapsedTime / interval) < ticks)
            {
                MakeDamage(character.transform);
            }
        }
    }

    private void MakeDamage(Transform characterTransform)
    {
        Vector3 rayOrigin = characterTransform.position + new Vector3(0f, 0.5f, 0f);
        RaycastHit[] sphereCastHitInfo = new RaycastHit[10];

        Physics.SphereCastNonAlloc(rayOrigin, damageRadius, characterTransform.forward, sphereCastHitInfo, damageRange, damageLayerMask);
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
                        damageableObject.TakeDamage(damageAmount, damageEmiterType);
                    }
                }
            }
        }
    }
}
