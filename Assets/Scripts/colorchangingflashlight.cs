using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorchangingflashlight : MonoBehaviour, IItem
{
    private Rigidbody rb;
    private Light light;
    private bool bright = false;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        light = this.transform.GetChild(0).gameObject.GetComponent<Light>();
        if (light != null)
        {
            light.enabled = false;
        }
    }


    public void Pickup(Transform hand)
    {
        Debug.Log("Picking up Flashlight");
        rb.isKinematic = true;
        this.transform.SetParent(hand);
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;
    }

    public void Drop()
    {
        Debug.Log("Dropping Color Changing Flashlight");
        rb.isKinematic = false;
        rb.AddRelativeForce(Vector3.forward * 10, ForceMode.Impulse);
        this.transform.SetParent(null);
    }

    public void PrimaryAction()
    {
        Debug.Log("Turning Color Changing Flashlight from blue to pink");
        light .enabled = !light.enabled;
    }

    public void SecondaryAction()
    {
        Debug.Log("Toggle Pink Brightness");
        bright = !bright;

        if(bright) {
            light.intensity = 10;
        }
        else {
            light.intensity = 3;
        }
    }
}