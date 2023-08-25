using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BirdStates/SubsequentFlapState")]
public class SubsequentFlapping : BirdState
{
    public BirdState gliding;
    public override void ProcessTransitions(BirdController bird)
    {
        if (timer < duration) return;
        if (FlapDesire(bird)) EnterState(bird);
        else TransitionTo(bird, gliding);
    }

    public override void EnterState(BirdController bird)
    {
        base.EnterState(bird);
        bird.animationControl.SubsequentFlap();
    }
}
