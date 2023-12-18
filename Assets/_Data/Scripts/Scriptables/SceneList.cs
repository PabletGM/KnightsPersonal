using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
//scriptable object que se encarga de la lista de escenas
[CreateAssetMenu(fileName ="SceneList", menuName ="MicroDungeons/SceneList")]
public class SceneList : ScriptableObject
{
    //lista de escenas
#if UNITY_EDITOR
    [SerializeField]
    private List<SceneAsset> sceneAssets;
#endif
    //nombre de escenas
    [SerializeField]
    public List<string> sceneNames;

    //las añade automaticamente al hacer un cambio en el editor 
    private void OnValidate()
    {
        sceneNames.Clear();
        if (sceneAssets.Count > 0)
        {
            foreach(SceneAsset asset in sceneAssets)
            {
                if(asset != null)
                {
                    sceneNames.Add(asset.name);
                }
            }
        }
    }
}
