using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGunAnim : MonoBehaviour
{
    private Animator anim;
    private bool isIdlePlaying = false;
    private bool isPrimaryPlaying = false;
    private bool isSecondaryPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if (!anim)
        {
            Debug.Log("No Animator attached to this component.");
            return;
        }

        // Ensure the animations are stopped initially
        anim.Play("Idle", 0, 0f);
        anim.speed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isIdlePlaying)
            {
                // Start playing the idle animation
                anim.Play("Idle");
                isIdlePlaying = true;
                anim.speed = 1f;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isIdlePlaying)
            {
                // Stop the idle animation
                anim.speed = 0f;
                isIdlePlaying = false;
            }
        }

        if (Input.GetMouseButtonDown(0)) // Left Click
        {
            if (!isPrimaryPlaying)
            {
                // Start playing the primary animation after a 1-second delay
                StartCoroutine(PlayPrimaryAfterDelay(1f));
            }
        }
        else if (Input.GetMouseButtonUp(0)) // Left Click released
        {
            if (isPrimaryPlaying)
            {
                // Stop the primary animation when the left click is released
                anim.speed = 0f;
                isPrimaryPlaying = false;
            }
        }

        if (Input.GetMouseButtonDown(1)) // Right Click
        {
            if (!isSecondaryPlaying)
            {
                // Start playing the secondary animation after a 1-second delay
                StartCoroutine(PlaySecondaryAfterDelay(0.5f));
            }
        }
        else if (Input.GetMouseButtonUp(1)) // Right Click released
        {
            if (isSecondaryPlaying)
            {
                // Stop the secondary animation when the right click is released
                anim.speed = 0f;
                isSecondaryPlaying = false;
            }
        }
    }

    IEnumerator PlayPrimaryAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Play the primary animation after the delay
        anim.Play("Primary");
        isPrimaryPlaying = true;
    }

    IEnumerator PlaySecondaryAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Play the secondary animation after the delay
        anim.Play("Secondary");
        isSecondaryPlaying = true;
    }
}
