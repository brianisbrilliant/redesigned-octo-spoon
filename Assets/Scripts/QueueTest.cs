using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueTest : MonoBehaviour
{
    // create a new array of GameObjects with a size of 6.
    [SerializeField] GameObject[] items = new GameObject[6];

    Queue<GameObject> inventory = new Queue<GameObject>();

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0)) { SpawnItem(); }
        if(Input.GetKeyDown(KeyCode.Alpha9)) { DropItem(); }
    }

    void SpawnItem() {
        GameObject copy = Instantiate(items[Random.Range(0,6)], Vector3.up * 5, Quaternion.identity);
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Item")) {
            inventory.Enqueue(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }

    void DropItem() {
        if(inventory.Count <= 0) return;

        GameObject itemmm = inventory.Dequeue();
        itemmm.SetActive(true);
        itemmm.transform.position = new Vector3(0,5,5);
    }
}
