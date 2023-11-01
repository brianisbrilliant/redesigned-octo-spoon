using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newspawner : MonoBehaviour
{
    public GameObject[] newObjects;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int randomIndex = Random.Range(0, newObjects.Length);
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-10, 11), 5, Random.Range(-10, 11));

            Instantiate(newObjects[randomIndex], randomSpawnPosition, Quaternion.identity);
        }
    }
}
