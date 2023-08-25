using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering
{
    BirdController bird;
    public Steering(BirdController _bird)
    {
        bird = _bird;
    }

    public void FixedUpdate(Vector2 steeringAxis)
    {
        
        if(steeringAxis == Vector2.left)
        {
            NoseUp();
        }
        else if(steeringAxis == Vector2.right)
        {
            NoseDown(); 
        }
        else if(steeringAxis == Vector2.zero)
        {
            //light torque returning bird to normal
        }
    }

    public void NoseDown(float powerFactor = 1f)
    {
        Vector3 pitchAxis = bird.transform.right;
        bird.refHolder.rb.AddTorque(pitchAxis * bird.data.steeringPower * powerFactor * bird.flightInfo.airSpeed * bird.flightInfo.airSpeed);
    }

    public void NoseUp(float powerFactor = 1f)
    {
        Vector3 pitchAxis = bird.transform.right;
        bird.refHolder.rb.AddTorque(-pitchAxis * bird.data.steeringPower * powerFactor * bird.flightInfo.airSpeed * bird.flightInfo.airSpeed);
    }
}
