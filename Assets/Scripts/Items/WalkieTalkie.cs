using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkieTalkie : MonoBehaviour, IItem
{
    private Rigidbody rb;
    private AudioSource onOffAudioSource;
    private AudioSource walkieTalkieAudioSource;
    private bool isOn = false;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        onOffAudioSource = this.GetComponent<AudioSource>();
        walkieTalkieAudioSource = this.transform.GetChild(1).gameObject.GetComponent<AudioSource>();
        if (onOffAudioSource != null)
        {
            onOffAudioSource.enabled = false;
        }
        if (walkieTalkieAudioSource != null)
        {
            walkieTalkieAudioSource.enabled = false;
            walkieTalkieAudioSource.loop = true; // Enable looping for the transmission sound
        }
    }

    public void Pickup(Transform hand)
    {
        Debug.Log("Picking up Walkie-Talkie");
        rb.isKinematic = true;
        this.transform.SetParent(hand);
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;
    }

    public void Drop()
    {
        Debug.Log("Dropping Walkie-Talkie");
        rb.isKinematic = false;
        rb.AddRelativeForce(Vector3.forward * 10, ForceMode.Impulse);
        this.transform.SetParent(null);
        isOn = false;
        ToggleAudioSources();
    }

    public void PrimaryAction()
    {
        Debug.Log("Toggle Walkie-Talkie Transmission");
        // Toggle transmission audio on and off only if Walkie-Talkie is on
        if (isOn && Input.GetKeyDown(KeyCode.Mouse0))
        {
            walkieTalkieAudioSource.enabled = !walkieTalkieAudioSource.enabled;
        }
    }

    public void SecondaryAction()
    {
        Debug.Log("Turning Walkie-Talkie on or off");
        // If turning off the Walkie-Talkie, stop the transmission audio
        if (!isOn && walkieTalkieAudioSource.isPlaying)
        {
            walkieTalkieAudioSource.Stop();
        }

        isOn = !isOn;
        ToggleAudioSources();
    }

    // Helper method to toggle audio sources based on the Walkie-Talkie's state
    private void ToggleAudioSources()
    {
        if (onOffAudioSource != null)
        {
            onOffAudioSource.enabled = isOn;
        }
        if (walkieTalkieAudioSource != null)
        {
            // If the Walkie-Talkie is off, ensure the transmission audio is off
            walkieTalkieAudioSource.enabled = isOn && walkieTalkieAudioSource.enabled;
        }
    }
}
