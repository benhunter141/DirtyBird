using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI debugInfo, stallWarning;
    string state;
    string airSpeed;
    string groundSpeed;
    string pitch;
    string elevation;
    string energy;
    string angleOfAttack;
    string lossRate;
    string efficiency;
    public StallState stallState = StallState.stalling;

    private void Update()
    {
        UpdateDebugText();
    }
    public void RescindStallMessage()
    {
        if (stallState == StallState.notStalling) return;
        stallState = StallState.notStalling;
        stallWarning.text = string.Empty;
    }
    public void IssueStallMessage()
    {
        if (stallState == StallState.stalling) return;
        stallState = StallState.stalling;
        stallWarning.text = "Stall!!!";
        stallWarning.color = ServiceLocator.Instance.colorManager.brightRed;
        //flash / fade in out red
    }
    public void IssueStallWarning()
    {
        if (stallState == StallState.stallWarning) return;
        stallState = StallState.stallWarning;
        stallWarning.text = "Stall warning!";
        stallWarning.color = ServiceLocator.Instance.colorManager.darkRed;
        
    }
    public void UpdateDebugText()
    {
        debugInfo.text = state + "<br>" +
    pitch + "<br>" +
    angleOfAttack + "<br>" +
    "<br>" +
    airSpeed + "<br>" +
    groundSpeed + "<br>" +
    "<br>" +
    elevation + "<br>" +
    "<br>" +
    energy + "<br>" +
    lossRate + "<br>" +
    efficiency;
    }
    public void UpdateEfficiency(string _efficiency)
    {
        efficiency = "Efficiency: " + _efficiency + " Js/m";
    }
    public void UpdateEnergyLossRate(string _loss)
    {
        lossRate = "Energy Loss Rate:" + _loss + " J/s";
    }
    public void UpdatePitch(string _pitch)
    {
        pitch = "Pitch: " + _pitch;
    }
    public void UpdateAirSpeed(string _airSpeed)
    {
        airSpeed = "Air Speed: " + _airSpeed + " m/s";
    }
    public void UpdateBirdState(string _state)
    {
        state = "State: " + _state;
    }

    public void UpdateElevation(string _elevation)
    {
        elevation = "El: " + _elevation + " m";
    }

    public void UpdateGroundSpeed(string _groundSpeed)
    {
        groundSpeed = "Ground Speed: " + _groundSpeed + " m/s";
    }

    public void UpdateEnergy(string _energy)
    {
        energy = "Total Energy: " + _energy + " J";
    }

    public void UpdateAngleOfAttack(string _angle)
    {
        angleOfAttack = "Angle of Attack: " + _angle + " degrees";
    }
}

public enum StallState
{ 
    notStalling,
    stalling,
    stallWarning
}

