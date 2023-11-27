using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainDrop : MonoBehaviour
{

    [SerializeField] GameObject[] items = new GameObject[4];   
    
    Queue<GameObject> inventory = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) {DropsOfRain(); }
        if(Input.GetKeyDown(KeyCode.Alpha8)) {DoRMaybe(); }
    }

    void DropsOfRain() {
    GameObject copy = Instantiate(items[Random.Range(0,6)], Vector3.up * 5, Quaternion.identity);
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Rain"));    {
            inventory.Enqueue(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }

    void DoRMaybe() {
        GameObject itemmm = inventory.Dequeue();
        itemmm.SetActive(true);
        itemmm.transform.position = new Vector3(0,5,5);
    }
}
