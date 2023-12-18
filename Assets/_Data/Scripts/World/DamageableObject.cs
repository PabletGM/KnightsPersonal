using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//hereda de IDamageable
public class DamageableObject : MonoBehaviour, IDamageable
{
    //posee 2 argumentos

    //particulas que saldrna al destruir la caja
    [SerializeField]
    private GameObject destroyParticles;

    //posicion o offset de las particulas respecto caja
    [SerializeField]
    private Vector3 particlesOffsetPosition;


    //metodo que en caso de existir las particulas las busca en el object pooler y despues destruye el objeto
    public virtual void TakeDamage(int damage, DamageEmiterType emiterType)
    {
        ParticlesDestroyBox();
        Destroy(gameObject);
    }

    protected void ParticlesDestroyBox()
    {
        if (destroyParticles != null)
        {
            Debug.Log("vfx");
            int poolIndex = ObjectPooler.instance.SearchPool(destroyParticles);
            Debug.Log(poolIndex);
            if (poolIndex != -1)
            {
                GameObject particles = ObjectPooler.instance.GetPooledObject(poolIndex);
                particles.transform.position = this.transform.position + particlesOffsetPosition;
                particles.SetActive(true);
                Debug.Log("vfx");
            }

        }
    }
}
