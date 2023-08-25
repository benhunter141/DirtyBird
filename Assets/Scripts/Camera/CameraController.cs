using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform bottomFrame, topFrame;
    [SerializeField]
    Camera mainCam;

    private void LateUpdate()
    {
        float sideViewDistance = 20f;
        float leadDistance = 20f;
        float topClearance = 10f;
        float botClearance = 10f;
        float camHeightLerp = 0.8f; //bias to top position

        //update bot,top positions
        float botX = sideViewDistance;
        float botY = ServiceLocator.Instance.environmentManager.GroundPositionAt(ServiceLocator.Instance.birdController.transform.position.z) - botClearance;
        float botZ = ServiceLocator.Instance.birdController.transform.position.z + leadDistance;

        bottomFrame.position = new Vector3(botX, botY, botZ);

        float topX = sideViewDistance;
        float topY = ServiceLocator.Instance.birdController.transform.position.y + topClearance;
        float topZ = ServiceLocator.Instance.birdController.transform.position.z + leadDistance;

        topFrame.position = new Vector3(topX, topY, topZ);

        Vector3 lerpedPosition = Vector3.Lerp(bottomFrame.position, topFrame.position, camHeightLerp);
        float vertViewDistance = topY - botY;

        mainCam.transform.position = lerpedPosition + new Vector3(10f,0,0);
        mainCam.orthographicSize = vertViewDistance / 2;
    }
}
