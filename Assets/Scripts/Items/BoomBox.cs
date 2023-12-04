using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBox : MonoBehaviour, IItem
{
    private Rigidbody rb;
    public float explosionForce = 1000f;
    public float explosionRadius = 5f;
    public float burstDuration = 0.5f; // Time in seconds for the burst effect
    public LayerMask affectedLayers;

    private bool isBursting = false;
    private float burstStartTime;

    public AudioClip[] audioClips; // Define an array of audio clips
    private AudioSource audioSource;
    private int currentClipIndex = 0;
    private bool hasPickedUp = false;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Pickup(Transform hand)
    {
        Debug.Log("Picking up BoomBox");
        rb.isKinematic = true;
        this.transform.SetParent(hand);
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;
        if (!hasPickedUp && audioClips.Length > 0)
        {
            audioSource.clip = audioClips[currentClipIndex];
            audioSource.Play();
            hasPickedUp = true;
        }
    }

    public void Drop()
    {
        Debug.Log("Dropping BoomBox");
        rb.isKinematic = false;
        rb.AddRelativeForce(Vector3.forward * 10, ForceMode.Impulse);
        this.transform.SetParent(null);
    }

    public void PrimaryAction()
    {
        Debug.Log("Performing Primary Action");
        // Trigger the burst effect when PrimaryAction is called
        StartBurst();

    }

    public void SecondaryAction()
    {
        if (audioClips.Length > 0)
        {
            currentClipIndex = (currentClipIndex + 1) % audioClips.Length;

            // Play the new audio clip
            audioSource.clip = audioClips[currentClipIndex];
            audioSource.Play();
        }
    }

    void Update()
    {
        if (isBursting && Time.time - burstStartTime > burstDuration)
        {
            isBursting = false;
        }
    }

    void StartBurst()
    {
        isBursting = true;
        burstStartTime = Time.time;

        // Apply the burst effect
        Explode();
    }

    void Explode()
    {
        // Get all colliders within the explosion radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, affectedLayers);

        foreach (Collider collider in colliders)
        {
            Rigidbody objRb = collider.GetComponent<Rigidbody>();

            if (objRb != null)
            {
                // Apply force to push the object back
                objRb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
    }
}
