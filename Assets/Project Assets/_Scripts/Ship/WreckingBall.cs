
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckingBall : MonoBehaviour
{
    [SerializeField]
    private float returnDelay = 1;
    [SerializeField]
    private float launchForce = 30;
    [SerializeField]
    private float returnintervalInSeconds = 2;
    [SerializeField]
    private AnimationCurve curve;
    [SerializeField]
    private AudioClip launchAudioClip; // Public AudioClip field


    private Collider collider;
    private AudioSource launchAudioSource; // Reference to the AudioSource component
    private Rigidbody rb;
    private Transform ballStart;
    private bool readyToLaunch = true;

    void Start()
    {
        collider = this.GetComponent<Collider>();
        rb = this.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        ballStart = GameObject.Find("BallStart").transform;

        // Add an AudioSource component and configure it
        launchAudioSource = gameObject.AddComponent<AudioSource>();
        launchAudioSource.playOnAwake = false;
        launchAudioSource.clip = launchAudioClip;
    }

    // remove this when attaching to player ship.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && readyToLaunch)
        {
            Launch();
        }
        if (readyToLaunch)
        {
            this.transform.position = ballStart.position;
            this.transform.rotation = ballStart.rotation;
        }
    }

    public void Launch()
    {
        Debug.Log("Launching!");
        readyToLaunch = false;

        // Play the launch audio clip
        if (launchAudioClip != null)
        {
            launchAudioSource.PlayOneShot(launchAudioClip);
        }

        StartCoroutine(Return());
        rb.isKinematic = false;

        collider.isTrigger = false;
        rb.AddForce(ballStart.forward * launchForce, ForceMode.Impulse);

    }

    private IEnumerator Return()
    {
        yield return new WaitForSeconds(returnDelay);
        rb.isKinematic = true;
        float counter = 0;
        Vector3 startPosition = this.transform.position;
        Quaternion startRotation = this.transform.rotation;

        while (counter < 1)
        {
            counter += Time.deltaTime / returnintervalInSeconds;
            this.transform.position = Vector3.Lerp(startPosition, ballStart.position, curve.Evaluate(counter));
            this.transform.rotation = Quaternion.Lerp(startRotation, ballStart.rotation, curve.Evaluate(counter));
            yield return new WaitForEndOfFrame();
        }

        collider.isTrigger = true;
        readyToLaunch = true;
    }
}
