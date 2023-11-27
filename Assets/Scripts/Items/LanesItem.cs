using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanesItem : MonoBehaviour, IItem
{
     private Rigidbody rb;
     public Animator anim;
public AudioSource aud;
public AudioClip audioClip;
public AudioClip audioClip1;
public AudioSource aud1;
     public Transform bulletFSpawnPoint;
     public Transform bulletBSpawnPoint;

    public GameObject projectilePrefab;
    public GameObject projectile1Prefab;

     public float bulletSpeed = 10; 

    // Start is called before the first frame update
    void Start()
    {
         rb = this.GetComponent<Rigidbody>();
             aud = GetComponent<AudioSource>();
              aud1 = GetComponent<AudioSource>();
              anim = gameObject.GetComponent<Animator>();
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

        anim.SetTrigger("PickUp");
    }

    public void Drop() {
        Debug.Log("Dropping Flashlight");
        // make dynamic rigidbody
        rb.isKinematic = false;
        // throw it away from the player
        rb.AddRelativeForce(Vector3.forward * 10, ForceMode.Impulse);
        // set this parent to null
        this.transform.SetParent(null);

        anim.SetTrigger("OnDrop");        
    }
    public void PrimaryAction()
    {
        // Use object pooling for the projectile
        GameObject projectile = LanesObjectPooler.Instance.PullFromPool(projectilePrefab);
        projectile.transform.position = bulletFSpawnPoint.position;
        projectile.transform.rotation = bulletFSpawnPoint.rotation;
        projectile.GetComponent<Rigidbody>().velocity = bulletFSpawnPoint.forward * bulletSpeed;
        aud.PlayOneShot(audioClip);
        anim.SetTrigger("Function2");
    }

    public void SecondaryAction()
    {
        // Use object pooling for the projectile
        GameObject projectile = LanesObjectPooler.Instance.PullFromPool(projectile1Prefab);
        projectile.transform.position = bulletBSpawnPoint.position;
        projectile.transform.rotation = bulletBSpawnPoint.rotation;
        projectile.GetComponent<Rigidbody>().velocity = bulletBSpawnPoint.forward * bulletSpeed;
        aud1.PlayOneShot(audioClip1);
        anim.SetTrigger("Function2");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetTrigger("Function1");
        }
    }
}
