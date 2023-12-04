using UnityEngine;

public class SpawnCube : MonoBehaviour
{
    public GameObject spherePrefab;
    public Transform spawnPoint;

    void Start()
    {
        SpawnSphere();
    }

    void SpawnSphere()
    {
        GameObject sphere = Instantiate(spherePrefab, spawnPoint.position, Quaternion.identity);
        sphere.SetActive(true);
    }

    public Vector3 GetSpawnPosition()
    {
        return spawnPoint.position;
    }
}
