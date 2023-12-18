using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//script que se encaraga de cambiar de escena a una aleatoria
public class SceneLoaderManager : MonoBehaviour
{
    public static SceneLoaderManager instance;

    public SceneList SceneList;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else if(instance != this)
        {
            Destroy(gameObject);
        }
        //nada mas aparecer carga una escena aleatoria
        LoadRandomScene();
    }

    //coge un numero de escena y carga la escena
    public void LoadRandomScene()
    {
        if(SceneList != null)
        {
            int sceneIndex = Random.Range(0, SceneList.sceneNames.Count);
            Debug.Log("Scene To Load -> " + SceneList.sceneNames[sceneIndex]);
            LoadScene(SceneList.sceneNames[sceneIndex]);
        }
    }

    //la carga de manera normal
    public void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
}
