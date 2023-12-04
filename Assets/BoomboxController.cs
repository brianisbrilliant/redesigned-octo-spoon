using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomboxController : MonoBehaviour
{
    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        if(!anim)
        {
            Debug.Log("No Animator Attached To This Component.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetTrigger("ColorChange");
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            anim.SetTrigger("StartGrow2");
        }
    }
}
