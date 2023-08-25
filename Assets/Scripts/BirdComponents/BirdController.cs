using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    [HideInInspector]
    public ReferenceHolder refHolder;
    public AnimationControl animationControl;

    //Physics
    public Steering steering;
    public Forces forces;

    //Data
    public FlightInfo flightInfo;

    //State
    public BirdState state;



    private void Awake()
    {
        refHolder = GetComponent<ReferenceHolder>();
        forces = new Forces(this);
        flightInfo = new FlightInfo(this);
        steering = new Steering(this);
        animationControl = new AnimationControl(this, GetComponent<Animator>());
    }

    private void FixedUpdate()
    {
        state.FUpdate(this);
    }

    public void SetStateTo(BirdState _state)
    {
        ServiceLocator.Instance.uiManager.UpdateBirdState(_state.name);
        state = _state;
    }
}
