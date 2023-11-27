using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SnowflakesGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] items = new GameObject[6];
    Queue<GameObject> Inventory = new Queue<GameObject>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            StartCoroutine(SpawnItemCoroutine());
            Debug.Log("SnowSpawned");
        }
    }

    IEnumerator SpawnItemCoroutine()
    {
        while (true)
        {
            GameObject copy = Instantiate(items[Random.Range(0, 6)], transform.position + new Vector3(0, 2, 0), Quaternion.identity);

            yield return new WaitForSeconds(0.5f); // Wait for half a second before spawning the next snowflake
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item")) 
        { 
        Inventory.Enqueue(other.gameObject);
            other.gameObject.SetActive(false);
        
        }

    }
}