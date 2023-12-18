using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;


//habilidad de movimiento principal del player
[CreateAssetMenu(fileName = "BaseDashAbility", menuName = "MicroDungeons/Abilities/BaseDashAbility")]
public class BaseDashAbility : BaseAbility
{
    //parametros que tambien tendrás los deñ BaseAbility al heredar de él
    [Header("BaseDashAbility Parameters")]
    //velocidad del dash
    public float dashSpeed = 1f;
    //objetos que puede atravesar con layer
    public LayerMask passThrougthLayerMask;

    //efecto de particulas
    public GameObject dashParticles;

    //inicia habilidad de dash
    public override void StartAbility(AbilityCharacter character)
    {
        //pone zonas que pùede atravesar con excludelayers
        character.Rigidbody.excludeLayers = passThrougthLayerMask;
        //activa animacion dash
        character.Animator.SetBool("IsDashing", true);
        //si existe el dashParticles lo activa
        if(dashParticles != null)
        {
            int poolIndex = ObjectPooler.instance.SearchPool(dashParticles);
            if(poolIndex != -1)
            {
                GameObject particles = ObjectPooler.instance.GetPooledObject(poolIndex);
                Debug.Log(dashParticles.name);
                particles.transform.position = character.transform.position;
                particles.SetActive(true);
            }
        }
    }

    //se encarga del movimiento del dash y de modificar el rigidbody velocity
    public override void FixedUpdateAbility(AbilityCharacter character, float fixedDeltaTime, float elapsedTime)
    {
        character.Rigidbody.velocity = character.Rigidbody.transform.forward * fixedDeltaTime * dashSpeed * 100f;
    }

    //al acabar la habilidad del dash, pone los layers como estabasn sin nada excluido y pone animacion IsDashing a false
    public override void EndAbility(AbilityCharacter character)
    {
        character.Rigidbody.excludeLayers = 0;
        character.Animator.SetBool("IsDashing", false);
    }
}
