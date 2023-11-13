// Brian Foster Interactive Scripting Fall 2023
// A flashlight that can be picked up and dropped
// that can be turned off and get brighter.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight1 : MonoBehaviour, IItem
{
    private Rigidbody rb;
    private Light light;
    private bool bright = false;
    private Collider flashlightCollider;

    void Start() {
        rb = this.GetComponent<Rigidbody>();
        light = this.transform.GetChild(0).gameObject.GetComponent<Light>();
        if(light != null) {
            light.enabled = false;
        }
        flashlightCollider = this.GetComponent<Collider>();
    }


    public void Pickup(Transform hand) {
        Debug.Log("Picking up Flashlight");
        // make kinematic rigidbody
        rb.isKinematic = true;
        // move to hand and match rotation
        this.transform.SetParent(hand);
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;
        // turn off collision so it doesn't push the player off the map
        flashlightCollider.enabled = false;
    }

    public void Drop() {
        Debug.Log("Dropping Flashlight");
        // make dynamic rigidbody
        rb.isKinematic = false;
        // throw it away from the player
        rb.AddRelativeForce(Vector3.forward * 10, ForceMode.Impulse);
        // set this parent to null
        this.transform.SetParent(null);
        flashlightCollider.enabled = true;
        
    }

    public void PrimaryAction() {
        Debug.Log("Turning Flashlight on or off");
        // set light active = false or = true
        light.enabled = !light.enabled;
    }

    public void SecondaryAction() {
        Debug.Log("Toggle brightness");
        // change intensity of light from 2 to 5 and back again.
        // this will flip the setting
        bright = !bright;
        
        // this will set the intensity of the light.
        if(bright) {
            light.intensity = 5;
        }
        else {
            light.intensity = 2;
        }
    }
}
