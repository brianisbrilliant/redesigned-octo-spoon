using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserONOFF : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
       anim = this.GetComponent<Animator>(); 

       if(!anim)
       {
        Debug.Log("No Animator on Santos Item");
        return;
       }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
         anim.Play("LaserOn");

        }
    }
}
