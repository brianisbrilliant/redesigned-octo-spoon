using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireInputHandler : MonoBehaviour
{
    public FireGun fireGun;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fireGun.PrimaryAction();
        }
        if (Input.GetMouseButtonUp(0))
        {
            fireGun.StopShooting();
        }
    }
}
