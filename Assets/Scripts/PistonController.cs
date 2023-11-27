using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonController : MonoBehaviour
{
    private Animator anim;

    [Range(.01f,.5f)]
    public float speedRange = .1f;

    // Start is called before the first frame update
    void Start()
    {   
        anim = this.GetComponent<Animator>();
        // easy way to check if anim exists
        if(!anim) {
            Debug.Log("No Animator attached to this component.");
            return;
        }

        anim.speed = Random.Range(1-speedRange, 1+speedRange);
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.R)) {
        //     anim.Play("GoingUp");
        // }
        // if(Input.GetKeyDown(KeyCode.F)) {
        //     anim.Play("GoingDown");
        // }
        if(Input.GetKeyDown(KeyCode.R)) {
            anim.SetTrigger("StartPiston");
        }
    }

    public void Slam() {
        Debug.Log("Boom!!");
        // this is where you play the audio clip of the piston hitting the earth.
    }
}
