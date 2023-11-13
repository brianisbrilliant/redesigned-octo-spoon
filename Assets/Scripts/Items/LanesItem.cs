using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanesItem : MonoBehaviour, IItem
{
     private Rigidbody rb;
public AudioSource aud;
public AudioClip audioClip;
public AudioClip audioClip1;
public AudioSource aud1;
     public Transform bulletFSpawnPoint;
     public Transform bulletBSpawnPoint;

     public GameObject ProjectilePrefab;
     public GameObject Projectile1Prefab;

     public float bulletSpeed = 10; 

    // Start is called before the first frame update
    void Start()
    {
         rb = this.GetComponent<Rigidbody>();
             aud = GetComponent<AudioSource>();
              aud1 = GetComponent<AudioSource>();
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
    }

    public void Drop() {
        Debug.Log("Dropping Flashlight");
        // make dynamic rigidbody
        rb.isKinematic = false;
        // throw it away from the player
        rb.AddRelativeForce(Vector3.forward * 10, ForceMode.Impulse);
        // set this parent to null
        this.transform.SetParent(null);
        
    }

    
    public void PrimaryAction() {
var projectile = Instantiate(ProjectilePrefab, bulletFSpawnPoint.position, bulletFSpawnPoint.rotation);
projectile.GetComponent<Rigidbody>().velocity = bulletFSpawnPoint.forward * bulletSpeed;
 aud.PlayOneShot(audioClip);
    }

    public void SecondaryAction() {
        var projectile1 = Instantiate(Projectile1Prefab, bulletBSpawnPoint.position, bulletBSpawnPoint.rotation);
        projectile1.GetComponent<Rigidbody>().velocity = bulletBSpawnPoint.forward * bulletSpeed;
     aud1.PlayOneShot(audioClip1);
    }
}
