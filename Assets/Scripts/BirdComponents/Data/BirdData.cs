using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BirdData : ScriptableObject
{
    public float flapForce;
    public float flapAngle; //0 to 1 where 0 is up, 1 is fwd
    public float flapTime;

    public float preLaunchDelay;
    public float launchForce;
    public float launchDuration;
    public float launchTorquePower;

    public float steeringPower;
}
