using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleGunInitializer : MonoBehaviour
{
   public GameObject  leftGrappleHook, rightGrappleHook;
   GrappleHook lGun, rGun;

    private void Start()
    {
        lGun = leftGrappleHook.GetComponent<GrappleHook>();
        rGun = rightGrappleHook.GetComponent<GrappleHook>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lGun.StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            lGun.StopGrapple();
        }

        if (Input.GetMouseButtonDown(1))
        {
            rGun.StartGrapple();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            rGun.StopGrapple();
        }
    }





}
