using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BirdStates/StandingState")]
public class Standing : BirdState
{
    public BirdState launching;
    public override void ProcessTransitions(BirdController bird)
    {
        if (FlapDesire(bird)) TransitionTo(bird, launching);
    }

    //need animation for standing?
}
