using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public bool FlapDesire { get; private set; }
    public Vector2 InputAxis { get; private set; }
    public float ZoomLevel { get; private set; }

    bool keyBoard = true;
    void Update()
    {
        ReadInputs();
        //better to send inputs than to take them... Current plan: states look here for current inputs (bad?)
    }
    public void ReadInputs()
    {
        if (Input.touchCount == 0) KeyboardControls();
        //TouchScreen
        else TouchScreenControls();


    }
    void TouchScreenControls()
    {
        //get pos x of touch in screen space
        Touch t = Input.GetTouch(0);
        Vector2 position = t.position;
        int width = Screen.width;
        //translate to x in (-1,1)
        float relativePositionX = position.x / width;
        relativePositionX -= 0.5f;
        //make power factor based on relativePositionX
        float powerFactor = Mathf.Abs(relativePositionX / 0.5f);
        if (relativePositionX > 0) ServiceLocator.Instance.birdController.steering.NoseDown(powerFactor);
        else ServiceLocator.Instance.birdController.steering.NoseUp(powerFactor);
    }
    void KeyboardControls()
    {
        //Flap
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FlapDesire = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            FlapDesire = false;
        }

        //Tilt
        if (Input.GetKey(KeyCode.A))
        {
            InputAxis = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            InputAxis = Vector2.right;
        }
        else
        {
            InputAxis = Vector2.zero;
        }

        //Zoom
        ZoomLevel += Input.mouseScrollDelta.y;
        ZoomLevel = Mathf.Clamp(ZoomLevel, 0f, 30f);
    }
}
