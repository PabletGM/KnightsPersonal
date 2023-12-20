using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//clase padre de toda la funcionalidad de los personajes
//-se le asocia un MonoBehaviour para poder usar las funciones start, update, fixedUpdate, etc
//- por qué tiene el IDamageable el AbilityCharacter , porque se usa en los 2, asi los 2 hijos son IDamageable
public class AbilityCharacter : MonoBehaviour, IDamageable
{
    //aquí se asocia el scriptableObject PlayerStats en el caso de player que hereda de GenericStats
    //esto se hace ya que todos los characters poseen atributos comunes como health, speed o rotation speed
    //es protected para que puedan acceder todos los hijos
    [SerializeField]
    protected GenericStats characterStats;



    //se hace variable get para poder acceder a este y a sus atributos
    public GenericStats CharacterStats
    {
        get
        {
            return characterStats;
        }
    }



    //se tiene la habilidad de ataque, en el caso del player se asocia el basePrimaryAttack
    [SerializeField]
    protected AttackAbility slotAttackAbility;

    //se tiene la habilidad de ataque, en el caso del player se asocia el basePrimaryAttack
    [SerializeField]
    protected AttackAbility slotAttackAbilitySecondary;


    //se tiene la habilidad de movimiento, en el caso del player se asocia el damageDASHaBILITY o dashABILITY
    [SerializeField]
    protected BaseAbility slotMoveAbility;


    //pasivas que se pueden recoger durante la partida como 
    //-aumentar speed
    //-aumentar daño
    //-etc
    [SerializeField]
    protected List<BaseAbility> passiveAbilities;


    //rigidbody del player para poder acceder a sus fisicas y moverlo
    private Rigidbody rb;


    //para poedr acceder al rigidbody desde fuera
    public Rigidbody Rigidbody
    {
        get { 
            return rb;
        }
    }

    //para acceder al animator del player
    private Animator anim;


    //para acceder al animator del player desde fuera con get
    public Animator Animator
    {   get { 
            return anim; 
        } 
    }

    private PlayerManager playerManager;

    public PlayerManager PlayerManager
    {
        get
        {
            return playerManager;
        }
    }


    //permite o da permiso de movimiento
    //usos:
    //  -En el PlayerAbilityCharacter
    //  -En el PlayerMovement
    //asi puedes controlar con una variable solo si quieres que el personaje se mueva, en el caso del moveAbility impedimos que se mueva antes de hacer la habilidad y para aplicar fisicas de movimiento en PlayerMovement debe cumplir este requisito
    protected bool canMove = true;




    //permite o da permiso para hacer las habilidades
    //usos:
    //  -En el AbilityCharacter--> depende el ExecutePrimaryAbility y ExecuteMoveAbility de este booleano
    //  -En el PlayerAbilityCharacter--> en el PlayerDied cuando el jugador muere se pone a false para impedir que pueda hacer habilidades 
    //  -En el BasicEnemyAbilityCharacter-> Si muere el enemigo se desactiva con el objectPooling, asi se activa el canDoAbilities para cuando respawnee una vez está desactivado, y tambien cuando muere en ese momento se pone a false.
    protected bool canDoAbilties = true;



    //se iguala a MovementStopTime que es el tiempo despues de hacer el rpimaryAttack que el jugador no puede volver a moverse
    protected float stoppedByExecuteAbilityTime = 0f;


    //para saber la habilidad actual que se está usando, no se puede hacer un dash y un ataque a la vez
    //asi antes de hacer una habilidad debes , definir cual es e iniciarla
    protected BaseAbility currentAbility;

    public BaseAbility CurrentAbility
    {
        get
        {
            return currentAbility;
        }
    }



    //para la duracion de la habilidad, el tiempo que lleva la habilidad actual en proceso
    private float currentAbilityElapsedTime = 0;


    //coolDown de ataque antes de poder atacar otra vez
    protected float attackAbilityCooldown = 0;



    //inicializa el character 
    private void Start()
    {
        InitAbilityCharacter();
    }



    // Method to init variables for all ability characters
    //inicializa rigidbody para permitir fisicas
    //animator para permitir animaciones
    //en caso de poseer pasivas el player las inicializa 
    protected virtual void InitAbilityCharacter()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        if(passiveAbilities.Count > 0 )
        {
            foreach (BaseAbility passiveAbility in passiveAbilities)
            {
                passiveAbility.StartAbility(this);
            }
        }
    }




    //metodo para añadir una pasiva nueva a la lista e inicializarla
    public void AddPassiveAbility(BaseAbility passiveAbility)
    {
        passiveAbilities.Add(passiveAbility);
        passiveAbility.StartAbility(this);
    }



    //cuando se le llama se comprueba si se tiene permiso para usar habilidades
    //si puede usarlas y no hay cooldown, inicializa el slotAttackAbility y pone la habilidad como current hablity
    //inicia contadores de cooldown y tiempo sin moverse(movementStopTime)
    protected void ExecutePrimaryAbility()
    {
        if (!canDoAbilties)
        {
            return;
        }

        if (slotAttackAbility != null && attackAbilityCooldown <= 0f)
        {
            slotAttackAbility.StartAbility(this);
            stoppedByExecuteAbilityTime = slotAttackAbility.movementStopTime;
            currentAbility = slotAttackAbility;
            attackAbilityCooldown = slotAttackAbility.cooldown;
        }
    }

    protected void ExecuteSecondaryAbility()
    {
        if (!canDoAbilties)
        {
            return;
        }

        if (slotAttackAbilitySecondary != null && attackAbilityCooldown <= 0f)
        {
            //Debug.Log("hel");
            slotAttackAbilitySecondary.StartAbility(this);
            stoppedByExecuteAbilityTime = slotAttackAbilitySecondary.movementStopTime;
            currentAbility = slotAttackAbilitySecondary;
            attackAbilityCooldown = slotAttackAbilitySecondary.cooldown;
        }
    }



    //comprueba si hay permiso para ejecutar habilidades y poner la habilidad actual como slotMoveAbility
    protected void ExecuteMoveAbility()
    {
        if (!canDoAbilties)
        {
            return;
        }

        if (slotMoveAbility != null)
        {
            slotMoveAbility.StartAbility(this);
            currentAbility = slotMoveAbility;
        }
    }



    //es para resetear la habilidad actual cuando esta cambia y acabar la habilidad actual para que termine de hacerse y reiniciar el contador de tiermpoDeHabilidadActual
    //usos:
    // -AbilityCharacter -->cuando acaba la duracion de una habilidad en el update
    // -PlayerAbilityCharacter --> ejecuta el metodo del abilityCharacter y le añade de funcionalidad darle permiso para moverse con el canMove a true
    // -EnemyAbilityCharacter --> al morir el enemigo para acabar la habilidad actual
    protected virtual void ResetCurrentAbility()
    {
        if(currentAbility != null)
        {
            currentAbility.EndAbility(this);
            currentAbility = null;
        }
        currentAbilityElapsedTime = 0f;
    }



    //para recibir eventos de animacion como triggers de animacion al morir el enemy o al atacar el player mientras tengamos habilidad actual
    protected void SendAbilityAnimationEvent()
    {
        if (currentAbility == null)
        {
            return;
        }

        //recibe el evento de animacion e inicia funcionalidad de ataque en el basePrimaryAttack
        currentAbility.OnReceiveAnimationEvent(this);
    }

    protected virtual void Update()
    {
        //metodo que ejecuta constantemente la habilidad actual si su duracion no ha acabado
        //pasa a los hijos donde sobreescribirá la funcionalidad el metodo UpdateAbility con el tiempo que lleva la habilidad ya hecha, el deltatime y una copia del abilityCharacter
        if (currentAbility != null && currentAbility.duration != 0f)
        {            
            if(currentAbilityElapsedTime < currentAbility.duration)
            {
                currentAbilityElapsedTime += Time.deltaTime;
                currentAbility.UpdateAbility(this, Time.deltaTime, currentAbilityElapsedTime);
            }
            else
            {
                ResetCurrentAbility();
            }
        }
        

        //por otra parte mira si la habilidad actual es la de ataque, si puede moverse y no hay coolDown para hacer el cronometro de cuando podrá moverse(cuando haya acabado la habilidad actual, si se ha acabado, permite el movimiento
        if( currentAbility == slotAttackAbility && slotAttackAbility.movementStopTime > 0f && stoppedByExecuteAbilityTime > 0f) {

            stoppedByExecuteAbilityTime -= Time.deltaTime;
            if(stoppedByExecuteAbilityTime <= 0f) {
                EnableCharacterMovement();
            }
        }


        //cronometro del coolDown
        if(attackAbilityCooldown > 0f)
        {
            attackAbilityCooldown -= Time.deltaTime;
        }
    }



    //se usa en el:
    // -BasicEnemyAbilityCharacter --> activa el navMeshAGENT para permitir el movimiento y persecucion del enemigo al player
    // -PlayerAbilityCharacter --> accede al playerMovement.CanMove para permitir que se mueva el player
    public virtual void EnableCharacterMovement() {}



    //se usa en el:
    // -BasicEnemyAbilityCharacter --> desactiva el navMeshAGENT para impedir el movimiento y persecucion del enemigo al player cuando no se detecta player o este ha muerto
    // -PlayerAbilityCharacter --> accede al playerMovement.CanMove para impedir que se mueva el player, por ejemplo al iniciar una habilidad en el StartAbility del AttackAbility
    public virtual void StopCharacterMovement() {}



    //update utilizado para fisicas en las habilidades , por ejemplo en el BaseDashAbility añade el movimiento de dash, le pasa por parametro lo que lleva ya hecho de dash en time, el fixedTime y el this como instancia
    private void FixedUpdate()
    {
        if (currentAbility != null && currentAbility.duration != 0f)
        {
            currentAbility.FixedUpdateAbility(this, Time.fixedDeltaTime, currentAbilityElapsedTime);
        }
    }



    //metodo que van a heredar toda la jerarquia y los hijos, cualquier objeto que sufra o haga daño
    //usos:
    //  -BasicEnemyAbilityCharacter: --> si la salud es 0 o el daño es el propio enemy evita daño, sino quita 1 vida, sino quedan vidas resetea current ability, canDoAbilities a false, pone animacion de muerte con trigger y corrutina destruir enemigo
    //  -PlayerAbilityCharacter: --> si el daño es el propio player evita daño, sino quita 1 vida, sino quedan vidas llama a metodo PlayerDied que resetea habilidad, canDoAbilities a false, canMove a false y pone animacion de muerte
    //- IDamageable, interfaz que posee metodo para que cualquier objeto que tenga el script pueda recibir daño
    // -BasePrimaryAttack en OnReceiveAnimationEvent al atacar el player envia un damageEmiterType y cantidad de daño que dependeran de los valores del AttackAbility
    // -DamageDashAbility en MakeDamage al atacar el player envia un damageEmiterType y cantidad de daño que dependeran de los valores del AttackAbility
    // -DamageableObject , cualquier objeto que tenga este como hereda de IDamageable, puede recibir daño
    // -HealthDamageableObject le quita vida con el parametro damage y hace funcionalidad del DamageableObject si no le queda vida para que le destruya, se usa este script en cajas oobjetos que pueden recibir daño pero quieres que tengan mas vida
    public virtual void TakeDamage(int damage, DamageEmiterType emiterType)
    {
    }
}
