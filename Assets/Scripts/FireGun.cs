using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : MonoBehaviour, IItem
{
    public ParticleSystem fireParticleSystem;
    
    private Color fireColor = Color.red; // Initial color of the fire
    private bool isShooting = false;
    private bool isHeld = false; // Flag to track if the item is in your hand

    public void Start()
    {
        // Ensure the Particle System starts paused
        fireParticleSystem.Stop();
    }

    public void Pickup(Transform hand)
    {
        Debug.Log("Picking up Fire Gun");
        isHeld = true; // Set the flag to true when picked up
        // Make kinematic rigidbody
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        // Move to hand and match rotation
        transform.SetParent(hand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        // Turn off collision so it doesn't push the player off the map
    }

    public void Drop()
    {
        Debug.Log("Dropping Fire Gun");
        isHeld = false; // Set the flag to false when dropped
        // Make dynamic rigidbody
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        // Throw it away from the player
        rb.AddRelativeForce(Vector3.forward * 10, ForceMode.Impulse);
        // Set this parent to null
        transform.SetParent(null);
    }

    public void PrimaryAction()
    {
        Debug.Log("Shooting fire with color: " + fireColor);
        
        if (isHeld && !isShooting) // Check if item is in hand and not shooting
        {
            isShooting = true;
            StartCoroutine(ShootFire());
        }
    }

    public void SecondaryAction()
    {
        Debug.Log("Change fire color");
        ChangeFireColor();
    }

    private void ChangeFireColor()
    {
        // Change the color of the fire
        fireColor = new Color(Random.value, Random.value, Random.value);
        Debug.Log("Fire color changed to: " + fireColor);
    }

    private IEnumerator ShootFire()
    {
        fireParticleSystem.Play();
        
        while (isShooting)
        {
            var mainModule = fireParticleSystem.main;
            mainModule.startColor = fireColor;
            yield return null;
        }
        
        fireParticleSystem.Stop();
    }

    public void StopShooting()
    {
        isShooting = false;
    }
}
