using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInputController : MonoBehaviour
{
    [SerializeField] Transform hand;
    [SerializeField] IItem lastTouchedItem;
    [SerializeField] IItem heldItem;

    // Start is called before the first frame update
    void Start()
    {
        //heldItem = GameObject.Find("FlashLightPivot").GetComponent<FlashLight>();
    }

    // Update is called once per frame
    void Update()
    {
            // Are we holding somthing?
        if(heldItem != null){
            // If so, when we press LM what it do?
            if(Input.GetKeyDown(KeyCode.Mouse0)) {
                heldItem.PrimaryAction();
            }

            if(Input.GetKeyDown(KeyCode.Mouse1)) {
                heldItem.SecondaryAction();
            }

            if(Input.GetKeyDown(KeyCode.Q)) {
                heldItem.Drop();
            }
        }
        // IF no item, change that
        else {
             if(Input.GetKeyDown(KeyCode.E)) {
                // make held
                heldItem = lastTouchedItem;
                lastTouchedItem = null;
                heldItem.Pickup(hand);
            }
        }
    }

    // Pick up by walking
    void OnTriggerEnter(Collider other) {
        Debug.Log("Ive hit a object");
        if(other.gameObject.CompareTag("Item")) {
        Debug.Log("The object is a item");
            lastTouchedItem = other.gameObject.GetComponent<IItem>();
        }
    }

    // Leave, no item
    void onTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Item")) {
            lastTouchedItem = null;
        }
    }
}
