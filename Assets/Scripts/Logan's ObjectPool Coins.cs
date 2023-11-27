using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingCoins : MonoBehaviour
{
    public GameObject coinPrefab;
    public int initialPoolSize = 10;
    public int maxTotalCoins = 20;
    public float spawnHeight = 3f;
    public float maxDistance = 4f;
    private List<GameObject> coinPool = new List<GameObject>();
    private Transform playerTransform;
    private Vector3 spawnOffset;
    private int activeCoinsCount = 0;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        InitializeCoinPool();
        spawnOffset = Vector3.up * spawnHeight;
    }

    private void InitializeCoinPool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewCoinInPool();
        }
    }

    private void CreateNewCoinInPool()
    {
        GameObject newCoin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
        newCoin.SetActive(false);
        coinPool.Add(newCoin);
    }

    private void Update()
    {
        if (ShouldSpawnCoin())
        {
            SpawnCoin();
        }
    }

    private bool ShouldSpawnCoin()
    {
        return activeCoinsCount < maxTotalCoins &&
               Vector3.Distance(transform.position, playerTransform.position) <= maxDistance &&
               Input.GetKey(KeyCode.Mouse0);
    }

    private void SpawnCoin()
    {
        GameObject coin = GetPooledCoin();
        if (coin != null)
        {
            coin.SetActive(true);
            coin.transform.position = transform.position + spawnOffset;
            StartCoroutine(DeactivateCoin(coin));
            activeCoinsCount++;
        }
    }

    private GameObject GetPooledCoin()
    {
        foreach (GameObject coin in coinPool)
        {
            if (!coin.activeSelf)
            {
                return coin;
            }
        }

        if (coinPool.Count < maxTotalCoins)
        {
            GameObject newCoin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            newCoin.SetActive(false);
            coinPool.Add(newCoin);
            return newCoin;
        }

        return null;
    }

    private IEnumerator DeactivateCoin(GameObject coin)
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        coin.SetActive(false);
        activeCoinsCount--;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + spawnOffset, maxDistance);
    }
}