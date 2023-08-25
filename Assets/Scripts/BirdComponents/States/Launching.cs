using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BirdStates/LaunchState")]
public class Launching : BirdState
{
    public BirdState gliding, singleFlap;
    public override void ProcessTransitions(BirdController bird)
    {
        if (timer < duration) return;
        if (FlapDesire(bird)) TransitionTo(bird, singleFlap);
        else TransitionTo(bird, gliding);
    }

    public override void EnterState(BirdController bird)
    {
        base.EnterState(bird);
        bird.animationControl.Launch();
    }
}
