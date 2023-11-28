using System.Collections;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab;
    public int numberOfCubes = 5;
    public float spawnInterval = 2f;
    public float timeOnGround = 5f; // Time a cube stays on the ground before pooling

    private void Start()
    {
        StartCoroutine(SpawnAndPoolCubes());
    }

    IEnumerator SpawnAndPoolCubes()
    {
        while (true)
        {
            for (int i = 0; i < numberOfCubes; i++)
            {
                // Spawn a cube
                GameObject cube = ShultsObjectPooler.Instance.GetPooledObject();

                if (cube == null)
                {
                    Debug.LogWarning("Not enough objects in the pool. Consider increasing the pool size.");
                    yield break;
                }

                // Set the spawn position to a random point within a specified range
                float randomX = Random.Range(0f, 1f);
                float randomZ = Random.Range(-28f, -30f);
                cube.transform.position = new Vector3(randomX, 10f, randomZ);

                cube.SetActive(true);

                yield return new WaitForSeconds(spawnInterval);
            }

            // Wait for all cubes to be on the ground for the specified time
            yield return new WaitForSeconds(timeOnGround);

            // Pool all cubes on the ground
            PoolAllCubes();
        }
    }

    void PoolAllCubes()
    {
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("ShultsCube"); // Assuming cubes have a "Cube" tag

        foreach (GameObject cube in cubes)
        {
            ShultsObjectPooler.Instance.PoolObject(cube);
        }
    }
}

