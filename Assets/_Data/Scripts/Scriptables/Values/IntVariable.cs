using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//scriptableObject donde se pone un argumento que se quiera modificar antes y despues de la ejecucion o durante que sea int
[CreateAssetMenu(fileName = "IntVariable", menuName = "MicroDungeons/Scriptable Variables/Int")]
public class IntVariable : ScriptableObject, ISerializationCallbackReceiver
{
    public int value;

    [NonSerialized]
    public int runTimeValue;

    public void OnAfterDeserialize()
    {
        runTimeValue = value;
    }

    public void OnBeforeSerialize()
    {
    }
}
