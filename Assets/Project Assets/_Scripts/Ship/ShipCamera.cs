// ensure that the camera is the first child of this gameobject.


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCamera : MonoBehaviour
{
    private float startingFOV = 60;
    private Camera cam;

    void Start()
    {
        cam = this.gameObject.transform.GetChild(0).GetComponent<Camera>();
        startingFOV = cam.fieldOfView;
    }

    public void ZoomOut() {
        cam.fieldOfView = startingFOV + 15;
    }

    public void DefaultZoom() {
        cam.fieldOfView = startingFOV;
    }
}
