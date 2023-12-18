using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script que al tenerlo el padre nos ahorramos que se eliminen todos los hijos
public class DontDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
