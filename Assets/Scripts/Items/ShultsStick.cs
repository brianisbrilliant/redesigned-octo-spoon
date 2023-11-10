using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShultsStick : MonoBehaviour, IItem
{
    public Renderer baseObjectRenderer;  // Reference to the base object's Renderer component

    private Rigidbody rb;
    private MeshRenderer meshRenderer;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();

        // Ensure there is a MeshRenderer component attached
        if (meshRenderer == null)
        {
            meshRenderer = gameObject.AddComponent<MeshRenderer>();
        }

        // Ensure there is an AudioSource component attached
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Set up audio clip (replace "YourAudioClip" with the actual AudioClip)
        // audioSource.clip = YourAudioClip;
    }

    public void Pickup(Transform hand)
    {
        Debug.Log("Picking up ShultsStick");
        rb.isKinematic = true;
        this.transform.SetParent(hand);
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;
    }

    public void Drop()
    {
        Debug.Log("Dropping ShultsStick");
        rb.isKinematic = false;
        rb.AddRelativeForce(Vector3.forward * 10, ForceMode.Impulse);
        this.transform.SetParent(null);
    }

    public void PrimaryAction()
    {
        Debug.Log("Changing base object color to random");

        // Check if the baseObjectRenderer is assigned
        if (baseObjectRenderer != null)
        {
            // Change the color of the base object to a random color
            baseObjectRenderer.material.color = new Color(Random.value, Random.value, Random.value);
        }
        else
        {
            Debug.LogWarning("Base object Renderer not assigned!");
        }
    }

    public void SecondaryAction()
    {
        Debug.Log("Playing audio clip");

        // Play the assigned audio clip
        if (audioSource.clip != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Audio clip not assigned!");
        }
    }
}
