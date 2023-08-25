using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flap
{
    //this state is a single flap, returning to glide when finished
    BirdController bird;
    Forces forces;
    public float flapTimer;
    

    public Flap(BirdController _bird, Forces _forces)
    {
        bird = _bird;
        forces = _forces;
        flapTimer = 0;
    }

    public void StartFlap()
    {
        flapTimer = 0;
        bird.animationControl.Flap();
    }

    public Vector3 Force()
    {
        float progress = bird.state.timer / bird.state.duration;
        
        //force direction depends on orientation of the bird
        Vector3 direction = bird.transform.forward;
        
        //force should peak like first half of a sin wave. force is zero at progress = 0,1. force is max at progress = 0.5
        float progressFactor = Mathf.Sin(progress * Mathf.PI);

        Vector3 force = direction * bird.state.flightMetrics.flapForce * progressFactor;
        forces.UpdateForceGizmo(bird.refHolder.flapForceGizmo, force);

        return force;
    }
}
