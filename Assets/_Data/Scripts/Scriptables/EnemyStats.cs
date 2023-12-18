using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//almacena los stats del enemy como velocidad, radio de deteccion , etc
//para ver los stats de ataque buscar scriptable object que hereda de attack abaility llamado EnemyPrimaryAttack
[CreateAssetMenu(fileName ="EnemyStats", menuName = "MicroDungeons/Stats/EnemyStats")]
public class EnemyStats : GenericStats
{
    [Header("Enemy Stats")]
    public float angularSpeed = 120f;
    public float stoppingDistance = 2f;
    public bool autoBraking = false;

    public float aggroRadius = 5f;
}
