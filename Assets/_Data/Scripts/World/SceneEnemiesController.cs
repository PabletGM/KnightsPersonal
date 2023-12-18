using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script que controlará las puertas que se pueden abrir en todo el juego
public class SceneEnemiesController : MonoBehaviour
{
    public static SceneEnemiesController Instance;

    public List<DoorController> doors;

    private int currentEnemiesOnScene = 0;

    private void Awake() {
        Instance = this;
    }

    //añadir enemigo a escena
    public void AddEnemyToScene() {
        currentEnemiesOnScene++;
    }

    //eliminar enemigo de escena
    public void RemoveEnemyFromScene() {
        currentEnemiesOnScene--;
        if(currentEnemiesOnScene == 0 ) {
            //Open door to go next scene
            for( int i = 0; i < doors.Count; i++ )
            {
                // se abririan todas las puertas si hacemos más o falta una condicion
                //cada sala es una escena asi que me interesa que se hagan animaciones de abarise de todas las puertas
                DoorController controller = doors[i];
                controller.OpenDoor();
            }
        }
    }
}
