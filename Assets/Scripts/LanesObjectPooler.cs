using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanesObjectPooler : MonoBehaviour
{
    public static LanesObjectPooler Instance;

    [System.Serializable]
    public class Pool
    {
        public GameObject prefab;
        public int poolSize;
    }

    public List<Pool> pools;

    private Dictionary<GameObject, List<GameObject>> objectPools;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        InitializePools();
    }

    void InitializePools()
    {
        objectPools = new Dictionary<GameObject, List<GameObject>>();

        foreach (var pool in pools)
        {
            MakePool(pool.prefab, pool.poolSize);
        }
    }

    void MakePool(GameObject prefab, int poolSize)
    {
        List<GameObject> objectPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            objectPool.Add(obj);
        }

        objectPools[prefab] = objectPool;
    }

    public GameObject PullFromPool(GameObject prefab)
    {
        if (objectPools.ContainsKey(prefab))
        {
            foreach (var obj in objectPools[prefab])
            {
                if (!obj.activeInHierarchy)
                {
                    obj.SetActive(true);
                    return obj;
                }
            }

           
            GameObject newObj = Instantiate(prefab);
            newObj.SetActive(true);
            objectPools[prefab].Add(newObj);

            return newObj;
        }

        Debug.LogError("Pool not found for prefab: " + prefab.name);
        return null;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
}