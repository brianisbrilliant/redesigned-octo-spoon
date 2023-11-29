using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserToggle : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    private bool laserOn = false;
    void Start()
    {
        anim = this.GetComponent<Animator>();
        if(!anim)
        {
            Debug.Log("MattSantos Animator not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            anim.Play("LaserOn");
            Debug.Log("Turning Laser On");
            laserOn = true;
        }

        if(Input.GetKeyDown(KeyCode.N) && laserOn)
        {
            anim.Play("LaserOff");
            Debug.Log("Turning Laser off");
            laserOn = false;
        }
    }
}
