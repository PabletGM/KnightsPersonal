using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script sobre el que se ha creado el scriptableObject IncreasePlayerMovementSpeed con la herencia de baseAbility y sus argumentos tambien, para cambiar pasivas poniendolas como variableToChange
[CreateAssetMenu(fileName = "ModifyIntStat", menuName = "MicroDungeons/Abilities/Passives/ModifyIntStat")]
public class ModifyIntStat : BaseAbility
{
    //aquí se elige la variable a cambiar o modificar con pasiva y el multiplicador , si la pasiva sera x2 o lo que quieras
    [Header("Modify Parameters")]
    public IntVariable variableToChange;
    public int multiplier = 1;

    public override void StartAbility(AbilityCharacter character)
    {
        if (variableToChange != null)
        {
            variableToChange.runTimeValue *= multiplier;
        }
    }
}
