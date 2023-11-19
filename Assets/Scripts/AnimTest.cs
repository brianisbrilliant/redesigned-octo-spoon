using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTest : MonoBehaviour
{
    public Animator anim;
    public bool scrubMode = true;

    public Transform cameraPivot;

    float normalizedTime = 0;
    
    // Start is called before the first frame update
    void Start()
    {  
        // you can easily check whether something is assigned a value this way:
        if(!anim) {
            anim = this.GetComponent<Animator>();
        }

        if(scrubMode) {
            anim.speed = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) anim.Play("groundEmbiggen");
        if(Input.GetKeyDown(KeyCode.F)) anim.Play("ReturnGround");

        if(scrubMode) {
            anim.Play("explosion2", -1, normalizedTime);
        }
    }

    public void test() {
        Debug.Log("Booom!");
    }

    public void Scrub(float givenValue) {
        normalizedTime = givenValue;
    }

    public void MoveCamera(float givenValue) {
        Quaternion newRot = Quaternion.Euler(0, givenValue * 360, 0);
        cameraPivot.SetPositionAndRotation(Vector3.zero, newRot);
    }
}
