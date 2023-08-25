using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BirdState : ScriptableObject
{
    public FlightMetrics flightMetrics;
    public bool flapping;
    public bool physicsActive;

    public float timer;
    public float duration;

    public void FUpdate(BirdController bird)
    {
        timer += Time.fixedDeltaTime;
        ProcessTransitions(bird);
        ProcessPhysics(bird);
    }
    public void ProcessPhysics(BirdController bird)
    {
        if (!physicsActive) return;
        bird.flightInfo.FixedUpdate();
        bird.forces.FixedUpdate(bird.flightInfo.airSpeed);
        bird.steering.FixedUpdate(ServiceLocator.Instance.inputHandler.InputAxis);
    }
    public abstract void ProcessTransitions(BirdController bird);
    protected bool FlapDesire(BirdController bird) => ServiceLocator.Instance.inputHandler.FlapDesire;
    protected void TransitionTo(BirdController bird, BirdState state)
    {
        bird.SetStateTo(state);
        state.EnterState(bird);
    }

    public virtual void EnterState(BirdController bird)
    {
        timer = 0;
    }
    
}
