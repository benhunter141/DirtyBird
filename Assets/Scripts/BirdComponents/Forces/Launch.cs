using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch
{
    BirdController bird;
    Forces forces;

    public Launch(BirdController _bird, Forces _forces)
    {
        bird = _bird;
        forces = _forces;
    }

    public Vector3 Force()
    {
        if (bird.state != bird.refHolder.launching) throw new System.Exception();
        float progress = bird.state.timer / bird.state.duration;
        if (progress < 0) return Vector3.zero;

        //add torque in first half to tip bird to desired rotation:
        if(progress < 0.5f)
        {
            //UNCLEAN:: This should be called from launch state
            bird.steering.NoseDown(progress * bird.data.launchTorquePower);
        }
            
        if (progress < 0 || progress > 1) throw new System.Exception();
        Vector3 direction = Vector3.up;
        float magnitude = Mathf.Sin(progress * Mathf.PI) * bird.data.launchForce;
        Vector3 force = direction * magnitude;
        forces.UpdateForceGizmo(bird.refHolder.liftForceGizmo, force);
        return force;
    }
}
