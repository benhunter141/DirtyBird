using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forces
{
    //All forces are added here, though they may be calculated elsewhere
    BirdController bird;

    Launch launch;
    Flap flap;
    Lift lift;
    Drag drag;
    Gravity gravity;

    public Forces(BirdController _bird)
    {
        bird = _bird;
        launch = new Launch(_bird, this);
        flap = new Flap(_bird, this);
        lift = new Lift(_bird, this);
        drag = new Drag(_bird, this);
        gravity = new Gravity(_bird, this);
        flap = new Flap(_bird, this);
        SetGizmosToZero();
    }

    public void FixedUpdate(float airSpeed)
    {
        if(bird.state.flapping) Flap();
        if (bird.state == bird.refHolder.launching) Launch();
        //flight metrics is used in all of these, provided by state
        Lift(airSpeed);         
        Drag(airSpeed);
        Gravity();
    }

    public void Launch()
    {
        Vector3 force = launch.Force();
        bird.refHolder.rb.AddForce(launch.Force());
    }
    public void Flap()
    {
        bird.refHolder.rb.AddForce(flap.Force());
    }

    public void Lift(float airSpeed)
    {
        bird.refHolder.rb.AddForce(lift.Force(airSpeed));
    }
    public void Drag(float airSpeed)
    {
        bird.refHolder.rb.AddForce(drag.Force(airSpeed));
    }

    public void Gravity()
    {
        bird.refHolder.rb.AddForce(gravity.Force());
    }

    public void StartFlap()
    {
        flap.StartFlap();
    }

    //public void StartLaunch()
    //{
    //    launch.StartLaunch();
    //}

    public void UpdateForceGizmo(Transform gizmo, Vector3 force)
    {
        if (force.sqrMagnitude < 0.01f)
        {
            gizmo.gameObject.SetActive(false);
            return;
        }
        else gizmo.gameObject.SetActive(true);
        Vector3 right = bird.transform.right;
        Vector3 up = Vector3.Cross(force, right);

        if (force.sqrMagnitude < 0.001) return;

       
        gizmo.transform.rotation = Quaternion.LookRotation(force, up);

        Vector3 scale = gizmo.transform.localScale;
        scale.z = force.magnitude / 2;
        gizmo.transform.localScale = scale;
    }

    public void SetGizmosToZero()
    {
        UpdateForceGizmo(bird.refHolder.dragForceGizmo, Vector3.zero);
        UpdateForceGizmo(bird.refHolder.liftForceGizmo, Vector3.zero);
        UpdateForceGizmo(bird.refHolder.flapForceGizmo, Vector3.zero);
        UpdateForceGizmo(bird.refHolder.gravityForceGizmo, Vector3.zero);
    }

}
