using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public bool FlapDesire { get; private set; }
    public Vector2 InputAxis { get; private set; }
    public float ZoomLevel { get; private set; }
    void Update()
    {
        ReadInputs();
        //better to send inputs than to take them... Current plan: states look here for current inputs (bad?)
    }
    public void ReadInputs()
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
