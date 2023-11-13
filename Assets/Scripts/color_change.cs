using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class color_change : MonoBehaviour
{

    Renderer ren;
    // Start is called before the first frame update
    void Start()
    {
        ren=GetComponent<Renderer>();
        ren.material.SetColor("_Color",Color.blue);

    }


}
