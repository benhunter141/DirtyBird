using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FlightInfo
{
    BirdController bird;
    public float airSpeed, pitch, elevation, groundSpeed, totalEnergy, angleOfAttack, energyLossRate, efficiency;
    List<float> recentEnergies = new();
    List<float> oldEnergies = new();

    public FlightInfo(BirdController _bird)
    {
        bird = _bird;
    }

    public void FixedUpdate()
    {
        airSpeed = UpdateAirSpeed();
        pitch = UpdatePitch();
        elevation = UpdateElevation();
        groundSpeed = UpdateGroundSpeed();
        totalEnergy = UpdateTotalEnergy();
        angleOfAttack = UpdateAngleOfAttack();
        energyLossRate = UpdateEnergyLossRate();
        efficiency = UpdateEfficiency();
        //Debug.Log($"efficiency: " + efficiency);
    }
    float UpdateEfficiency()
    {
        float efficiency = energyLossRate / groundSpeed;
        float rounded = (float)System.Math.Round(efficiency, 2);
        ServiceLocator.Instance.uiManager.UpdateEfficiency(rounded.ToString());
        return efficiency;
    }
    float UpdateEnergyLossRate()
    {
        int dataPoints = 10;
        recentEnergies.Add(totalEnergy);
        if (recentEnergies.Count < dataPoints) return energyLossRate;
        if(recentEnergies.Count >= dataPoints && oldEnergies.Count < dataPoints)
        {
            oldEnergies = new List<float>(recentEnergies);
            recentEnergies.Clear();
            return energyLossRate;
        }
        if(recentEnergies.Count == dataPoints && oldEnergies.Count == dataPoints)
        {
            float recentAverage = recentEnergies.Sum() / recentEnergies.Count;
            float oldAverage = oldEnergies.Sum() / oldEnergies.Count;
            float smoothedAverage = recentAverage - oldAverage;
            float rounded = (float)System.Math.Round(smoothedAverage, 1);
            ServiceLocator.Instance.uiManager.UpdateEnergyLossRate(rounded.ToString());
            recentEnergies.Clear();
            oldEnergies.Clear();
            return smoothedAverage;
        }
        throw new System.Exception("fucked!");
    }

    float UpdateAngleOfAttack()
    {
        Vector3 facingDirection = bird.transform.forward;
        Vector3 directionOfTravel = bird.refHolder.rb.velocity;
        float angle = Vector3.SignedAngle(facingDirection, directionOfTravel, bird.transform.right);
        float rounded = (float)System.Math.Round(angle, 2);
        ServiceLocator.Instance.uiManager.UpdateAngleOfAttack(rounded.ToString());
        return angle;
    }

    float UpdateTotalEnergy()
    {
        //gpe - simplified mgh, later:(E = GmM/r^2)
        float height = ServiceLocator.Instance.environmentManager.Elevation(bird.transform.position);
        float gpe = height * ServiceLocator.Instance.environmentManager.gravityFactor; //should be 9.8
        //ke
        float ke = bird.refHolder.rb.velocity.sqrMagnitude / 2;
        totalEnergy = gpe + ke;
        totalEnergy = (float)System.Math.Round(totalEnergy, 1);
        ServiceLocator.Instance.uiManager.UpdateEnergy(totalEnergy.ToString());
        return totalEnergy;
    }
    float UpdateGroundSpeed()
    {
        Vector3 velocity = bird.refHolder.rb.velocity;
        velocity.y = 0;
        float groundSpeed = velocity.magnitude;
        groundSpeed = (float)System.Math.Round(groundSpeed, 1);
        ServiceLocator.Instance.uiManager.UpdateGroundSpeed(groundSpeed.ToString());
        return groundSpeed;
    }

    float UpdateElevation()
    {
        double elevation = ServiceLocator.Instance.environmentManager.Elevation(bird.transform.position);
        float rounded = (float)System.Math.Round(elevation, 1);
        ServiceLocator.Instance.uiManager.UpdateElevation(rounded.ToString());
        return (float)elevation;
    }
    float UpdatePitch()
    {
        float pitch = ServiceLocator.Instance.environmentManager.PitchAt(bird.transform.position, bird.transform.forward);
        pitch = (float)System.Math.Round(pitch, 1);
        ServiceLocator.Instance.uiManager.UpdatePitch(pitch.ToString());
        return pitch;
    }

    float UpdateAirSpeed()
    {
        double speed = bird.refHolder.rb.velocity.magnitude;
        speed = System.Math.Round(speed, 2);
        ServiceLocator.Instance.uiManager.UpdateAirSpeed(speed.ToString());
        return (float)speed;
    }

}
