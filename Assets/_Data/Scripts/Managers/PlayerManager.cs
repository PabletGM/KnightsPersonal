using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //singleton
    public static PlayerManager instance;


    //referencia del PlayerAbilityCharacter
    private PlayerAbilityCharacter playerAbilityCharacter;

    //stat vida inicial
    private int initHealth;

    //stat vida actual
    private int currentHealth;


    //acceder a estos valores
    public int CurrentHealth
    {
        set
        {
            currentHealth = value;
        }
        get
        {
            return currentHealth;
        }
    }

    //singleton
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if(instance != this)
        {
            Destroy(gameObject);
        }
    }


    //conexion con PlayerAbilityCharacter e inicializa valores  initHealth y currentHealth con PlayerStats(que viene de GenericStats)
    private void Start()
    {
        playerAbilityCharacter = GetComponent<PlayerAbilityCharacter>();

        initHealth = playerAbilityCharacter.CharacterStats.health;
        currentHealth = initHealth;
    }

    //devuelve PlayerStats para coger info de alguna stat
    public GenericStats GetPlayerStats()
    {
        return playerAbilityCharacter.CharacterStats;
    }
}
