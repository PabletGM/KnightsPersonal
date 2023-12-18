using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object pooling is a software development design pattern and a container of objects that holds a list of other objects
/// </summary>
public class ObjectPooler : MonoBehaviour
{
	public static ObjectPooler instance;

	/// <summary>
	/// Every pool will have the prefab to instantiate, number of instances and a list of the objects intantiated
	/// </summary>
	[System.Serializable]
	public struct ObjectsToPool
	{
		public GameObject prefab;
		public int amountToPool;
		[HideInInspector]
		public List<GameObject> pooledObjects;
	}

	/// <summary>
	/// List of structures of the different prefabs to pool
	/// </summary>
	[SerializeField]
	private List<ObjectsToPool> objPool;

	private void Awake()
	{
		instance = this;
		InitPooler();
    }

	/// <summary>
	/// Instantiate each number of prefabs that have been set
	/// </summary>
	private void InitPooler()
	{
		for (int i = 0; i < objPool.Count; i++) 
		{
			for (int j = 0; j < objPool[i].amountToPool; j++)
			{
				GameObject obj = Instantiate(objPool[i].prefab);
				obj.SetActive(false);
				obj.transform.parent = gameObject.transform;
                objPool[i].pooledObjects.Add(obj);
			}
		}
	}

	/// <summary>
	/// Look in the list of structs the prefab that has been passed. If its not in any struct, returns -1 (trigger error)
	/// </summary>
	/// <param name="prefab"></param>
	/// <returns></returns>
	public int SearchPool(GameObject prefab)
	{
		for (int i = 0; i < objPool.Count; i++)
		{
			if (objPool[i].prefab == prefab)
				return i;
		}		
		return -1;
	}

	/// <summary>
	/// Passed the position of the struct where the required object of a prefab is, to get an object that can be used
	/// If the object is not active, return it. If all objects of the pool list are activated, create one to use and add it to the list
	/// </summary>
	/// <param name="numPool"></param>
	/// <returns></returns>
	public GameObject GetPooledObject(int numPool) 
	{
		for (int i = objPool[numPool].pooledObjects.Count -1; i >= 0; i--)
		{
			if (objPool[numPool].pooledObjects[i].activeInHierarchy == false)
			{				
				return objPool[numPool].pooledObjects[i];
			}
		}

		GameObject obj = Instantiate(objPool[numPool].prefab);
		obj.SetActive(false);
        obj.transform.parent = gameObject.transform;
        objPool[numPool].pooledObjects.Add(obj);
		return obj;
	}

	public void DespawnAll(){
		for (int i = 0; i < objPool.Count; i++) 
		{
			for (int j = 0; j < objPool[i].pooledObjects.Count; j++)
			{
				GameObject obj = objPool[i].pooledObjects[j];
				obj.SetActive(false);
			}
		}
	}

}
