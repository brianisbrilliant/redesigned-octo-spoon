using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LaserPointer : MonoBehaviour, IItem
{
    public GameObject laser;
    private Rigidbody rb;
    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
       
    }
    public void Pickup(Transform hand) 
    {
        Debug.Log("Picking up laser pointer");
        //make kinematic rigidbody
        rb.isKinematic = true;
        //move to hand and match rotation
        this.transform.SetParent(hand);
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;
    }
    public void Drop()
    {
        Debug.Log("dropping Laser Pointer.");
        rb.isKinematic = false;
        rb.AddRelativeForce(Vector3.forward * 10, ForceMode.Impulse);
        this.transform.SetParent(null);

    }
    public void PrimaryAction()
    {
      laser.SetActive(true);
    }
    public void SecondaryAction() 
    {
        laser.SetActive(false);
    }

}
