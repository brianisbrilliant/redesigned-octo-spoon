using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fpstester : MonoBehaviour
{
    FirstPersonController fps;

    // Start is called before the first frame update
    void Start()
    {
        fps = GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            fps.cameraCanMove = false;
        }
    }
}
