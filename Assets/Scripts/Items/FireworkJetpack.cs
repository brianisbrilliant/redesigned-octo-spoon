// Caleb Richardson Interactive Scripting, Fall Semester 2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A Firework Jetpack that allows for more vertical movement, and attack options.
/// </summary>
public class FireworkJetpack : MonoBehaviour, IItem
{
    [Header("Options")]
    [SerializeField] private bool showParticles = true;
    [SerializeField] private float particleLiveTime = 3f;

    [Header("Movement Variables")]
    [SerializeField, Range(1, 100)] private float upwardForceAmount;
    [SerializeField, Range(1, 100)] private float downwardForceAmount;

    [Header("Particle System References")]
    [SerializeField] private ParticleObject rocketParticlePrefab;
    [SerializeField] private Transform rocketParticleSpawn;

    private Rigidbody Rb => GetComponent<Rigidbody>();
    private Collider JetPackCollider => GetComponent<Collider>();
    private Vector3 RocketSpawnPos => rocketParticleSpawn.position;
    private Quaternion RocketSpawnRotation => rocketParticlePrefab.transform.localRotation;

    private Rigidbody playerRb;
    private FirstPersonController playerController;

    // Object pooling to save performance
    private Queue<ParticleObject> particleObjectPool = new Queue<ParticleObject>();

    // Used to avoid creating to much garbage for the GC.
    private WaitForSeconds rocketWaitTimer = new WaitForSeconds(0.75f);

    private bool hasLauched = false;
    private bool isFloating = false;

    public void Pickup(Transform hand){
        if(playerRb == null || playerController == null){
            // Get the player transform.
            var playerControllerTransform = hand.root;
            // Get the FirstPersonController.
            playerControllerTransform.TryGetComponent(out playerController);
            // Get the player rigidbody
            playerControllerTransform.TryGetComponent(out playerRb);

            if(playerController == null || playerRb == null){
                Debug.Log("Didn't find the player controller or player rigidbody.");
                return;
            }

            playerController.OnGroundedCallback += Reset;
        }

        Debug.Log("Picking up Firework Jetpack");
        // make kinematic rigidbody
        Rb.isKinematic = true;
        // move to hand and match rotation
        transform.SetParent(hand);
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        // turn off collision so it doesn't push the player off the map
        JetPackCollider.enabled = false;
    }

    public void Drop(){
        Debug.Log("Dropping Firework Jetpack");
        // make dynamic rigidbody
        Rb.isKinematic = false;
        // throw it away from the player
        Rb.AddRelativeForce(Vector3.forward * 10, ForceMode.Impulse);
        // set this parent to null
        transform.SetParent(null);
        // Enable collider
        JetPackCollider.enabled = true;
        // Resetting the values.
        Reset();
    }

    public void PrimaryAction(){
        // Launch player.
        if(!hasLauched){
            playerRb.velocity = new Vector3(playerRb.velocity.x, transform.up.y * upwardForceAmount, playerRb.velocity.z);
            hasLauched = true;
            if(showParticles) FireParticles();
        }
        // Starting floating.
        else if(!isFloating){
            playerRb.velocity = new Vector3(playerRb.velocity.x, 0, playerRb.velocity.z);
            playerRb.useGravity = false;
            isFloating = true;
            if(showParticles) StartCoroutine(FireParticleTimer());
        }
        // Stop floating.
        else{
            playerRb.useGravity = true;
            isFloating = false;
            StopAllCoroutines();
        }
    }

    public void SecondaryAction(){
       if(hasLauched){
            // Launch the player down.
            playerRb.velocity = new Vector3(playerRb.velocity.x, -transform.up.y * downwardForceAmount, playerRb.velocity.z);
            if(showParticles) FireParticles();
       }
    }

    private void OnDestroy() {
        // Unsubscribe from the OnGroundCallback to avoid memory leaks.
        if(playerController != null){
            playerController.OnGroundedCallback -= Reset;
        }
    }

    private void Reset(){
        playerRb.useGravity = true;
        isFloating = false;
        hasLauched = false;
        StopAllCoroutines();
    }

    private void FireParticles(){
        ParticleObject particleObj;
        // Check if there is particles in the pool
        if(particleObjectPool.Count > 0){
            // Reuse a particle from the object pool
            particleObj = particleObjectPool.Dequeue();
            particleObj.gameObject.SetActive(true);
            particleObj.transform.SetPositionAndRotation(RocketSpawnPos, RocketSpawnRotation);
            particleObj.PlayParticle();
            return;
        }
        // If no particle in the pool then create a new one.
        var spawnedParticle = Instantiate(rocketParticlePrefab, RocketSpawnPos, RocketSpawnRotation);
        spawnedParticle.SetupParticleObj(particleObjectPool, particleLiveTime);
    }

    // Async and await might be better here.
    private IEnumerator FireParticleTimer(){
        while(isFloating){
            FireParticles();
            yield return rocketWaitTimer;
        }
    }
}
