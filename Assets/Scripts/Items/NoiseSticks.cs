using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseSticks : MonoBehaviour, IItem {
    private Rigidbody rb;
    private Animator anim;
    private AudioSource audioSound;
    private bool pitch = false;

    private Queue<GameObject> pool = new Queue<GameObject>();
    [SerializeField] private Transform particleSpawnPoint;
    [SerializeField] private float particleTime;
    [SerializeField] private GameObject particleObject;

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
        anim.SetTrigger("SecondTrigger");
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
        SpawnParticles();
    }

    public void SpawnParticles() {
        GameObject particleToSpawn;
        if(pool.Count > 0) {
            particleToSpawn = pool.Dequeue();
            particleToSpawn.gameObject.SetActive(true);

            Debug.Log("Spawn Particles pool.Count > 0");
        } else {
            particleToSpawn = Instantiate(particleObject);
            //particleToSpawn.gameObject.GetComponent<ParticleSystem>();
            Debug.Log("Object was instantiated");
        }
            particleToSpawn.transform.SetParent(this.transform);
            particleToSpawn.transform.SetPositionAndRotation(particleSpawnPoint.position, Quaternion.identity);
            PlayParticle(particleToSpawn);
    }

    private void PlayParticle(GameObject particleToPlay) {
        StartCoroutine(ParticleVisibleTimer(particleToPlay));
        Debug.Log("Should play particles");
    }

    private IEnumerator ParticleVisibleTimer(GameObject particleVisible) {
        yield return new WaitForSeconds(particleTime);
        DisableParticle(particleVisible);
    }

    private void DisableParticle(GameObject particleToDisable) {
        if(particleToDisable.activeSelf == false) return;

        pool.Enqueue(particleToDisable);
        particleToDisable.SetActive(false);
    }
}
