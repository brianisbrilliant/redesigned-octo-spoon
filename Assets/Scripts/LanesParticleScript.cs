using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanesParticleScript : MonoBehaviour
{

    public AudioSource aud;
    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound()
    {
aud.Play();
    }
}
