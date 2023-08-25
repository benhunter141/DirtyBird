using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BirdStates/GlidingState")]
public class Gliding : BirdState
{
    public BirdState singleFlap;
    public override void ProcessTransitions(BirdController bird)
    {
        if (FlapDesire(bird)) TransitionTo(bird, singleFlap);
    }

    public override void EnterState(BirdController bird)
    {
        base.EnterState(bird);
        bird.animationControl.Glide();
    }
}
