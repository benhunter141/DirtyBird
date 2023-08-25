using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Camera/CameraView")]
public class CameraView : ScriptableObject
{
    public bool relativeDisplacement, relativeRotation, focusOnBird;
    public Vector3 displacement;
    public Quaternion rotation;

    public Vector3 IdealCameraPosition(Transform bird)
    {
        if(relativeDisplacement)
        {
            return bird.TransformPoint(displacement);
        }
        else
        {
            return bird.position + displacement; //world pos
        }
    }

    public Quaternion IdealCameraRotation(Transform bird, Transform camera)
    {
        if(focusOnBird)
        {
            return Quaternion.LookRotation(bird.position - camera.position);
        }
        if(relativeRotation)
        {
            return bird.rotation * rotation;
        }
        else
        {
            return rotation;
        }
    }
}
