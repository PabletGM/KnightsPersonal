using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//spawnear enemigos
public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<Transform> spawnPoints;

    [SerializeField]
    private GameObject EnemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        int poolIndex = ObjectPooler.instance.SearchPool(EnemyPrefab);
        if(poolIndex != -1)
        {
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                GameObject enemyGO = ObjectPooler.instance.GetPooledObject(poolIndex);
                enemyGO.transform.position = spawnPoints[i].position;
                enemyGO.transform.rotation = spawnPoints[i].rotation;
                enemyGO.SetActive(true);
                SceneEnemiesController.Instance.AddEnemyToScene();
            }
        }
    }
}
