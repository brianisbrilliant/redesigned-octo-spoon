using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseSticks : MonoBehaviour, IItem {
    private Rigidbody rb;
    private Animator anim;
    private AudioSource audioSound;
    private bool pitch = false;

    void Start() {
        rb = this.GetComponent<Rigidbody>();
        anim = this.GetComponent<Animator>();
        audioSound = this.GetComponent<AudioSource>();
    }


    public void Pickup(Transform hand) {
        Debug.Log("Picking up Sticks");
        // make kinematic rigidbody
        rb.isKinematic = true;
        // move to hand and match rotation
        this.transform.SetParent(hand);
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;
        // turn off collision so it doesn't push the player off the map
    }

    public void Drop() {
        Debug.Log("Dropping Sticks");
        // make dynamic rigidbody
        rb.isKinematic = false;
        // throw it away from the player
        rb.AddRelativeForce(Vector3.forward * 10, ForceMode.Impulse);
        // set this parent to null
        this.transform.SetParent(null);
        
    }

    public void PrimaryAction() {
        Debug.Log("Hit Sticks Together");
        // set light active = false or = true
        anim.SetTrigger("StickSmackTrigger");
    }

    public void SecondaryAction() {
        Debug.Log("Toggle Sound Effect");
        // change pitch of the sound from 1 to 0.5
        // this will flip the setting
        pitch = !pitch;
        
        // this will change the pitch of the sound
        if(pitch) {
            audioSound.pitch = 1;
        }
        else {
            audioSound.pitch = 0.5f;
        }
    }

    public void PlaySound() {
        audioSound.Play();
    }
}
