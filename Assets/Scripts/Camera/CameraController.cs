using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    new public Transform camera;
    public CameraView sideView;
    public float smoothRotFactor, smoothPosFactor;

    void Update()
    {
        Transform bird = ServiceLocator.Instance.birdController.transform;

        Vector3 idealPosition = sideView.IdealCameraPosition(bird);
        Vector3 position = Vector3.Lerp(camera.position, idealPosition, smoothPosFactor);
        camera.position = position;

        Quaternion idealRot = sideView.IdealCameraRotation(bird, camera);
        Debug.Log($"idealRot: " + idealRot);
        Quaternion rotation = Quaternion.Lerp(camera.rotation, idealRot, smoothRotFactor);
        camera.rotation = rotation;
    }
}
