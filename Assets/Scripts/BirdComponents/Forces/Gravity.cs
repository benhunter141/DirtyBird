using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity
{
    BirdController bird;
    Forces forces;

    public Gravity(BirdController _bird, Forces _forces)
    {
        bird = _bird;
        forces = _forces;
    }

    public Vector3 Force()
    {
        Vector3 force = -ServiceLocator.Instance.environmentManager.UpAt(bird.transform.position) * ServiceLocator.Instance.environmentManager.gravityFactor;

        forces.UpdateForceGizmo(bird.refHolder.gravityForceGizmo, force);
        return force;
    }

}
