using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSaberController : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        if(!anim)
        {
          Debug.Log("No Animator attached to this component.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            anim.Play("SaberSwing");
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            anim.Play("SaberSwingBack");
        }
        if(Input.GetKeyDown(KeyCode.V))
        {
            anim.Play("SaberStab");
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            anim.Play("SaberStabBack");
        }
    }
}
