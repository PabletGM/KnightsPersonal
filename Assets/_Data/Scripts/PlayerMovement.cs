using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //metodo del que dependeerá el permiso de movimiento del player
    private bool canMove = true;

    //para acceder a el desde fuera y poder modificarlo desde el PlayerAbilityCharacter segun si queremos impedir movimiento o no
    public bool CanMove
    {
        set {
            canMove = value;
            //sino puede moverse cambiamos velocity del rigidbody a 0
            if (!canMove) {
                rb.velocity = Vector3.zero;
            }
        }
        get { 
            return canMove; 
        }
    }

    //Player movement Components
    private Rigidbody rb;
    private Animator anim;

    //Movement general variables
    private Vector3 playerMovement;

    //referencia al PlayerManager
    private PlayerManager playerManager;

   
    //referencias generales
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        playerManager = PlayerManager.instance;
    }


    //lee todo el rato y modifica en el animator permisos de andar o no para iniciar una animacion u otra
    private void Update()
    {
        anim.SetFloat("Speed", playerMovement.sqrMagnitude);
        anim.SetBool("CanMove", canMove);
    }


    //fisicas que permiten moverse y rotar  con rigidbody mientras que canMove = true
    void FixedUpdate()
    {
        if(!canMove)
        {
            return;
        }

        //movimiento de player al andar
        rb.velocity = playerMovement * Time.fixedDeltaTime * playerManager.GetPlayerStats().speed.runTimeValue * 100f;

        //rotacion de player si se esta moviendo
        if (playerMovement != Vector3.zero)
        {
            //mirar en sentido del vector de posicion
            Quaternion playerRot = Quaternion.LookRotation(playerMovement, Vector3.up);
            //rotar al rigidbody de una rotacion a otra con una velocidad
            rb.rotation = Quaternion.RotateTowards(rb.rotation, playerRot, Time.fixedDeltaTime * playerManager.GetPlayerStats().rotationSpeed * 100f);
        }
    }


    //registra vector de movimiento y lo normaliza para saber direccion
    public void OnMovement(InputValue value)
    {
        Vector2 playerInput = value.Get<Vector2>();
        playerMovement = new Vector3(playerInput.x, 0f, playerInput.y);
        playerMovement = playerMovement.normalized;
    }
}
