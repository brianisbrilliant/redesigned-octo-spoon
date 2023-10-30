using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    // create an integer named cubeSpeed 
    private int cubeSpeed = 0;
    public int riseSpeed = 0;

    private CubeAudio cubeAudio;        // a reference to the cubeAudio component script on this cube

    void Start() {
        // ...with a random range between 1 and 10 (inclusive)
        cubeSpeed = Random.Range(1,10);
        cubeAudio = this.GetComponent<CubeAudio>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(0, riseSpeed * Time.deltaTime, 0, Space.World);
    }

    public void GetCollected() 
    {
        Debug.Log("This cube is getting collected. Their speed is " + cubeSpeed);
        // if speed is greater than 6 (7, 8, 9, or 10)
        if(cubeSpeed > 6) 
        {
            // turn green (Color.green)
            this.GetComponent<Renderer>().material.color = Color.green;
            // move up by changing riseSpeed to 5
            riseSpeed = 5;
            // make the cube ignore gravity
            this.GetComponent<Rigidbody>().isKinematic = true;
            // remove collider
            Destroy(this.GetComponent<Collider>());
            // destroy after 5 seconds.
            Destroy(this.gameObject, 5f);
            // playAudio
            cubeAudio.PlayCollectionAudio(true);
        }
        else 
        {
            // turn red
            this.GetComponent<Renderer>().material.color = Color.red;
            // destroy self after 1 second
            Destroy(this.gameObject, 1f);
            // playAudio
            cubeAudio.PlayCollectionAudio(false);
        }  
            
    }
}
