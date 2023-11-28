using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RileySword : MonoBehaviour, IItem
{
    private Rigidbody rb;
    private bool hasPickedUp = false;
    Animator anim;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
       
       {
        anim = gameObject.GetComponent<Animator>();
       }
    }
    public void Pickup(Transform hand)
    {
        Debug.Log("Picking up Sword");
        rb.isKinematic = true;
        this.transform.SetParent(hand);
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;
    }

    public void Drop()
    {
        Debug.Log("Dropping Sword");
        rb.isKinematic = false;
        rb.AddRelativeForce(Vector3.forward * 10, ForceMode.Impulse);
        this.transform.SetParent(null);
    }

    public void PrimaryAction()
    {
       Debug.Log("Swinging Sword");
       if (Input.GetMouseButtonDown(0))
       {
        anim.SetTrigger("Active");
       }       
    }

    public void SecondaryAction()
    {
       Debug.Log("Throwing Sword");
    }
}