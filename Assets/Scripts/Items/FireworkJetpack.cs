// Caleb Richardson Interactive Scripting, Fall Semester 2023
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
/// <summary>
/// A Firework Jetpack that allows for more vertical movement, and attack options.
/// </summary>
public class FireworkJetpack : MonoBehaviour, IItem
{
    // members

    [Header("Options")]
    [SerializeField] private bool showParticles = true;
    [SerializeField] private float particleLiveTime = 3f;

    [Header("Movement Variables")]
    [SerializeField, Range(1, 100)] private float upwardForceAmount;
    [SerializeField, Range(1, 100)] private float downwardForceAmount;

    [Header("Particle System References")]
    [SerializeField] private ParticleObject rocketParticlePrefab;
    [SerializeField] private Transform rocketParticleSpawn;

    [Header("Animation References")]
    [SerializeField] private AnimationClip idleAnimation;
    [SerializeField] private AnimationClip launchAnimation;
    [SerializeField] private AnimationClip slamAnimation;
    [SerializeField] private AnimationClip startFloatingAnimation;
    [SerializeField] private AnimationClip floatAnimation;
    [SerializeField] private AnimationClip stopFloatingAnimation;

    private Animator JetPackAnimator => GetComponent<Animator>();
    private Rigidbody Rb => GetComponent<Rigidbody>();
    private Collider JetPackCollider => GetComponent<Collider>();
    private Vector3 RocketSpawnPos => rocketParticleSpawn.position;
    private Quaternion RocketSpawnRotation => rocketParticlePrefab.transform.localRotation;

    private Dictionary<AnimationClip, Animation> clipDictionary;
    private Rigidbody playerRb;
    private FirstPersonController playerController;
    private JetpackAnimationState currentAnimationState = JetpackAnimationState.None;

    // Object pooling to save performance
    private Queue<ParticleObject> particleObjectPool = new Queue<ParticleObject>();

    // Used to avoid creating to much garbage for the GC.
    private WaitForSeconds rocketWaitTimer = new WaitForSeconds(0.75f);

    private bool hasLauched = false;
    private bool isFloating = false;

#region Unity Functions
    private void OnDestroy() {
        // Unsubscribe from the OnGroundCallback to avoid memory leaks.
        if(playerController != null){
            playerController.OnGroundedCallback -= Reset;
        }
    }
#endregion

#region Public Functions
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
        JetPackAnimator.enabled = true;
        IdleAnimation();
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
        // Reset Animation
        currentAnimationState = JetpackAnimationState.None;
        JetPackAnimator.enabled = false;
        // Resetting the values.
        Reset();

    }

    public void PrimaryAction(){
        // Launch player.
        if(!hasLauched){
            playerRb.velocity = new Vector3(playerRb.velocity.x, transform.up.y * upwardForceAmount, playerRb.velocity.z);
            hasLauched = true;
            ChangeAnimationState(JetpackAnimationState.LAUNCH);
            if(showParticles) FireParticles();
        }
        // Starting floating.
        else if(!isFloating){
            playerRb.velocity = new Vector3(playerRb.velocity.x, 0, playerRb.velocity.z);
            playerRb.useGravity = false;
            isFloating = true;
            ChangeAnimationState(JetpackAnimationState.FLOAT_START);
            if(showParticles) StartCoroutine(FireParticleTimer());
            Invoke(nameof(IdleAnimation), startFloatingAnimation.averageDuration);
        }
        // Stop floating.
        else{
            playerRb.useGravity = true;
            isFloating = false;
            ChangeAnimationState(JetpackAnimationState.FLOAT_STOP);
            Invoke(nameof(IdleAnimation), stopFloatingAnimation.averageDuration);
            StopAllCoroutines();
        }
    }

    public void SecondaryAction(){
    if(hasLauched){
        // Launch the player down.
        playerRb.velocity = new Vector3(playerRb.velocity.x, -transform.up.y * downwardForceAmount, playerRb.velocity.z);
        ChangeAnimationState(JetpackAnimationState.SLAM);
        Invoke(nameof(IdleAnimation), slamAnimation.averageDuration);
        if(showParticles) FireParticles();
    }
}
#endregion

#region Private Functions
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

    private void IdleAnimation(){
        if(currentAnimationState == JetpackAnimationState.FLOAT_START){
            ChangeAnimationState(JetpackAnimationState.FLOAT_IDLE);
            return;
        }
        ChangeAnimationState(JetpackAnimationState.IDLE);
    }

    private void ChangeAnimationState(JetpackAnimationState newState){
        if(currentAnimationState == newState){
            // Guard to stop the same animation from interrupting itself
            return;
        }

        // Play the new animation
        switch (newState){
            case JetpackAnimationState.IDLE: JetPackAnimator.Play(idleAnimation.name);
                break;
            case JetpackAnimationState.LAUNCH: JetPackAnimator.Play(launchAnimation.name);
                break;
            case JetpackAnimationState.SLAM: JetPackAnimator.Play(slamAnimation.name);
                break;
            case JetpackAnimationState.FLOAT_START: JetPackAnimator.Play(startFloatingAnimation.name);
                break;
            case JetpackAnimationState.FLOAT_IDLE: JetPackAnimator.Play(floatAnimation.name);
                break;
            case JetpackAnimationState.FLOAT_STOP: JetPackAnimator.Play(stopFloatingAnimation.name);
                break;
        }

        currentAnimationState = newState;
    }

    private enum JetpackAnimationState{
        None,
        IDLE,
        LAUNCH,
        SLAM,
        FLOAT_START,
        FLOAT_IDLE,
        FLOAT_STOP
    }
    #endregion
}
