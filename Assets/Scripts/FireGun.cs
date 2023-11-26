using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : MonoBehaviour, IItem
{
    public ParticleSystem fireParticleSystem;
    private Color fireColor = Color.red;
    private bool isShooting = false;
    private bool isHeld = false;
    private Rigidbody rb;

    private void Start()
    {
        fireParticleSystem.Stop();
        rb = GetComponent<Rigidbody>();
    }

    public void Pickup(Transform hand)
    {
        if (!isHeld)
        {
            Debug.Log("Picking up Fire Gun");
            isHeld = true;

            // Disable Rigidbody to prevent movement while held
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;  // Ensure no residual velocity

            // Set the trigger to play the pickup animation if needed
            // animator.SetTrigger("PickupTrigger");

            transform.SetParent(hand);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            Collider col = GetComponent<Collider>();
            if (col != null)
            {
                col.enabled = false;
            }
        }
    }

    public void Drop()
    {
        Debug.Log("Dropping Fire Gun");
        isHeld = false;

        // Enable Rigidbody to allow movement when not held
        rb.isKinematic = false;

        rb.AddRelativeForce(Vector3.forward * 10, ForceMode.Impulse);

        transform.SetParent(null);

        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = true;
        }
    }

    public void PrimaryAction()
    {
        Debug.Log("Shooting fire with color: " + fireColor);

        if (isHeld && !isShooting)
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
