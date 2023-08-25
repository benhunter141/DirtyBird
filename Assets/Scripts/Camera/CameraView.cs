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
            return bird.TransformPoint(displacement + displacement.normalized * ServiceLocator.Instance.inputHandler.ZoomLevel);
        }
        else
        {
            return bird.position + displacement + displacement.normalized * ServiceLocator.Instance.inputHandler.ZoomLevel; //world pos
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
