using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//stats genéricos del que heredan atributps PlayerStats y EnemyStats
[CreateAssetMenu(fileName ="GenericStats", menuName = "MicroDungeons/Stats/GenericStats")]
public class GenericStats : ScriptableObject
{
    public int health;

    [Header("Movement parameters")]
    public FloatVariable speed;
    [Range(1f, 10f)]
    public float rotationSpeed;

    [Header("Attack Parameters")]
    public IntVariable baseDamage;
}
