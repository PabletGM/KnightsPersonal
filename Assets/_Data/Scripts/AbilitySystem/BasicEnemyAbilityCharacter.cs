using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//posee toda la funcionalidad del enemigo, como el PlayerAbilityCharacter del player
public class BasicEnemyAbilityCharacter : AbilityCharacter
{
    [Header("Enemy Parameters")]

    //parametros de tipos de particulas de destruccion
    [Header("Destroy Particles")]
    public GameObject despawnParticles;
    //donde quieres que spawnee el vfx al morir enemy
    public GameObject particlesPivot;

    //referencia al agent del enemy y sus hijos tambien
    protected NavMeshAgent agent;

    //referencia al playerManager
    private PlayerManager playerManager;

    //referencia  a las stats de GenericStats pero del enemy
    private EnemyStats enemyStats;

    //vida actual
    protected int currentHealth;

    //corrutina destroyEnemy para controlarla
    IEnumerator DestroyEnemyCoroutine;

    //añade funcionalidad al initAbilityCharacter del padre AbilityCharacter
    protected override void InitAbilityCharacter()
    {
        base.InitAbilityCharacter();

        //inicializa el nav mesh Agent
        agent = GetComponent<NavMeshAgent>();
        Debug.Log(agent);

        //haces una conversion con un casteo sabiendo que characterStats de tipo generico es un GenericStats, el cual es el padre de enemyStats inicializando la variable
        enemyStats = (EnemyStats)characterStats;
        //si hay enemyStats saca todos los datos a rellenar del nav mesh de él
        if(enemyStats != null )
        {
            //vida actual del enemy
            currentHealth = enemyStats.health;
            //velocidad del enemy en el navMesh
            agent.speed = enemyStats.speed.runTimeValue;
            //velocidad angular del enemy en el navMesh
            agent.angularSpeed = enemyStats.angularSpeed;
            //distancia de frenado del enemy en el navMesh
            agent.stoppingDistance = enemyStats.stoppingDistance;
            //y autofrenado del enemy en el navMesh
            agent.autoBraking = enemyStats.autoBraking;
        }

        //Get player reference
        playerManager = PlayerManager.instance;
    }



    //añade funcionalidad a el Update del AbilityCharacter
    protected override void Update()
    {
        //se puede ejecutar mientras le quede vida al enemy y pueda hacer habilidades
        if (!canDoAbilties && currentHealth <= 0)
        {
            return;
        }

        //funcionalidad padre abilityCharacter que es updateAbility o resetearla si ha acabado el timer
        base.Update();


        //se calcula distancia entre player y enemy
        float distanceToPlayer = Vector3.Distance(transform.position, playerManager.transform.position);

        //comprueba si enemy está cerca y nav mesh agent esta en movimiento
        if(IsNearToPlayer(distanceToPlayer))
        {
            //comprueba si jugador está vivo
            if (IsPlayerAlive())
            {
                //si está vivo ponemos el destino del enemigo la posicion del player
                agent.SetDestination(playerManager.transform.position);
                //si la distancia al player es< al attack range ejecuta habilidad de ataque
                if(distanceToPlayer <= slotAttackAbility.attackRange)
                {
                    ExecutePrimaryAbility();
                }
            }
            //sino esta vivo paramos agent para que no se mueva enemy
            else
            {
                StopCharacterMovement();
                //ejecutamos animacion risa
                Animator.SetTrigger("IsLaughing");
            }
        }

        //ejecutamos animacion andar todo el rato con booleano true o false si esta cerca del player y le puede eprseguir y si el jugador está vivo
        Animator.SetBool("IsMoving", IsNearToPlayer(distanceToPlayer) && IsPlayerAlive());
    }

    //permite movimiento del enemy
    public override void EnableCharacterMovement() {
        base.EnableCharacterMovement();
        
        agent.isStopped = false;
    }

    //quita movimiento del enemy
    public override void StopCharacterMovement() {
        base.StopCharacterMovement();
        agent.isStopped = true;
    }

    //para eso revisa la vida del player
    protected virtual bool IsPlayerAlive()
    {
        return playerManager.CurrentHealth > 0;
    }

    //metodo que comprueba si enemy tiene el nav mesh agent no parado y con el radio de alcance con el player dentro
    protected bool IsNearToPlayer(float distanceToPlayer)
    {
        return distanceToPlayer <= enemyStats.aggroRadius && !agent.isStopped;
    }

    //coge daño si no es de si mismo, quita vida, sino le quedan impide habilidades, ponemos animacion con trigger y empezamos  corrutina DestroyEnemy que hara lo del object pooler
    public override void TakeDamage(int damage, DamageEmiterType emiterType)
    {
        if(emiterType == DamageEmiterType.Enemy || currentHealth < 0)
        {
            return;
        }

        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            ResetCurrentAbility();
            canDoAbilties = false;
            Animator.SetTrigger("IsDied");
            
            DestroyEnemyCoroutine = DestroyEnemy();
            StartCoroutine(DestroyEnemyCoroutine);
        }
    }


    //corrutina DestroyEnemy
    private IEnumerator DestroyEnemy()
    {
        //object pooler que resta tiempo de espera de muerte 
        float timeToWait = 2.5f;
        while(timeToWait >= 0)
        {
            yield return null;            
            timeToWait -= Time.deltaTime;
        }
        //cuando acaba pone vfx de particulas en la posicion de la cabeza del enemy y las activa
        DestroyEnemyCoroutine = null;        
        int poolIndex = ObjectPooler.instance.SearchPool(despawnParticles);
        if(poolIndex != -1){
            GameObject particles = ObjectPooler.instance.GetPooledObject(poolIndex);
            particles.transform.position = particlesPivot.transform.position;
            particles.SetActive(true);
        }
        //quita enemigo de la escena
        SceneEnemiesController.Instance.RemoveEnemyFromScene();
        //para el pooler reiniciamos valores del enemy para que cuando aparezca sea funcional
        //ponemos salud
        currentHealth = characterStats.health;
        //reinicia propiedades del animator
        Animator.Rebind();
        //permite su movimiento
        agent.isStopped = false;
        //permite sus habilidades
        canDoAbilties = true;
        //lo desactiva
        gameObject.SetActive(false);
    }
}
