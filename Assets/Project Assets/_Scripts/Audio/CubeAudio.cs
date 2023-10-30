using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAudio : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip ascend;
    [SerializeField] private AudioClip descend;      // declare multiple variables on the same line.

    private AudioSource aud;
    private GlobalAudio globAud;
    

    // Start is called before the first frame update
    void Start()
    {
        aud = this.GetComponent<AudioSource>();
        // aud.PlayOneShot(ascend);         // test code, remove later
        globAud = GameObject.Find("GameManager").GetComponent<GlobalAudio>();
    }

    public void PlayCollectionAudio(bool ascending) {

        globAud.pitchShift += 0.1f;
        if(globAud.pitchShift > 2) globAud.pitchShift = 1;        // reset pitch
        aud.pitch = globAud.pitchShift;

        if(ascending == true) {
            aud.PlayOneShot(ascend);
        }
        else {
            aud.PlayOneShot(descend);
        }
    }

}
