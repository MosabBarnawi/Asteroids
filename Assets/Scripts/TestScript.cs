using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Transform cam;
    public Vector3 cameraRelative;
    private void Update()
    {


        cam = Camera.main.transform;
        cameraRelative = cam.TransformDirection(transform.position);

        if (cameraRelative.z > 0)
            print("The object is in front of the camera");
        else
            print("The object is behind the camera");

    }
}
