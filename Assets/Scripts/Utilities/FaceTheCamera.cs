using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTheCamera : MonoBehaviour
{
    Transform cam;

    private void Start()
    {
        cam = Camera.main.transform;
    }

    private void Update()
    {
        AngleCanvasAtCamera();
    }

    void AngleCanvasAtCamera()
    {
        Vector3 displacement = cam.transform.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(displacement, cam.up);
        transform.rotation = rot;
    }
}
