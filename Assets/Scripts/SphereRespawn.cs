using UnityEngine;

public class SphereRespawn : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        Respawn();
    }

    void Respawn()
    {
        SpawnCube spawnCube = GetComponent<SpawnCube>();
        
        if (spawnCube != null)
        {
            Debug.Log("Respawning!");
            transform.position = spawnCube.GetSpawnPosition();
            GetComponent<Rigidbody>().velocity = Vector3.zero; // Reset velocity
        }
        else
        {
            Debug.LogError("SpawnCube component not found!");
        }
    }
}
