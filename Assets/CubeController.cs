using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    // create an integer named cubeSpeed with a random range between 1 and 10 (inclusive)
    
    public int riseSpeed = 0;

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(0, riseSpeed * Time.deltaTime, 0);
    }

    public void GetCollected() {
        // if speed is greater than 6 (7, 8, 9, or 10)
            // turn green (Color.green)
            // move up by changing riseSpeed to 5
            this.GetComponent<Rigidbody>().isKinematic = true;
            // destroy after 5 seconds.
        // else
            // turn red
            // destroy self after 1 second
    }
}
