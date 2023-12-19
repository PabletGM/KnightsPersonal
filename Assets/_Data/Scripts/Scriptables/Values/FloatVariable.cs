using System;
using UnityEngine;

//similar a intVariable, para pasarlo como argumento y poer modificar el valor despus onGame
[CreateAssetMenu(fileName ="FloatVariable",menuName = "MicroDungeons/Scriptable Variables/Float")]
public class FloatVariable : ScriptableObject, ISerializationCallbackReceiver
{
    public float value;

    [NonSerialized]
    public float runTimeValue;

    //se accede a este valor que se modifica desde el editor como un ScriptableObject para las pasivas o parametros con el FloatVariable
    public void OnAfterDeserialize()
    {
        runTimeValue = value;
    }

    public void OnBeforeSerialize()
    {
    }
}
