using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShultsObjectPooler : MonoBehaviour
{
    public static ShultsObjectPooler Instance;

    public GameObject objectToPool;
    public int poolSize = 10;

    private List<GameObject> pooledObjects;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }

    public void PoolObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    internal void PoolObject(object shultsCube)
    {
        throw new NotImplementedException();
    }
}




//using System.Collections;
//using UnityEngine;

//public class CubeSpawner : MonoBehaviour
//{
//    public GameObject cubePrefab;
//    public int numberOfCubes = 5;
//    public float spawnInterval = 2f;

//    private void Start()
//    {
//        StartCoroutine(SpawnCubes());
//    }

//    IEnumerator SpawnCubes()
//    {
//        for (int i = 0; i < numberOfCubes; i++)
//        {
//            // Spawn a cube
//            GameObject cube = ShultsObjectPooler.Instance.GetPooledObject();

//            if (cube == null)
//            {
//                Debug.LogWarning("Not enough objects in the pool. Consider increasing the pool size.");
//                yield break;
//            }

//            // Set the spawn position to a random point within a specified range
//            float randomX = Random.Range(0f, 1f);
//            float randomZ = Random.Range(-28f, -30f);
//            cube.transform.position = new Vector3(randomX, 10f, randomZ);

//            cube.SetActive(true);

//            yield return new WaitForSeconds(spawnInterval);

//            // Pool the cube after it has fallen
//            ShultsObjectPooler.Instance.PoolObject(cube);
//        }
//    }
//}

