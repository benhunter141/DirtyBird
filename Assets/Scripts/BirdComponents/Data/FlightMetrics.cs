using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FlightMetrics : ScriptableObject
{
    public float dragCoefficientMin, dragCoefficientPreStall, dragCoefficientPostStall, dragCoefficientMax;
    public float minLiftCoefficient, maxLiftCoefficient, stallLiftCoefficient;
    public float minAngleOfAttack, maxAngleOfAttack, stallAngleOfAttack;
}
