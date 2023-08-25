using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FlightMetrics : ScriptableObject
{
    //Lift & Drag
    public float dragCoefficientMin, dragCoefficientPreStall, dragCoefficientPostStall, dragCoefficientMax;
    public float minLiftCoefficient, maxLiftCoefficient, stallLiftCoefficient;
    public float minAngleOfAttack, maxAngleOfAttack, stallAngleOfAttack;
    public float minAirSpeed, maxAirSpeed;

    //Flap
    public float flapForce;
    public float flapAngle; //0 to 1 where 0 is up, 1 is fwd
    public float flapTime;

    //Launch
    public float preLaunchDelay;
    public float launchForce;
    public float launchDuration;
    public float launchTorquePower;

    //Steering
    public float steeringPower;
}
