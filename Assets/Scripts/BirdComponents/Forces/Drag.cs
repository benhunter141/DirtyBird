using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag
{
    BirdController bird;
    Forces forces;
    public Drag(BirdController _bird, Forces _forces)
    {
        bird = _bird;
        forces = _forces;
    }

    public Vector3 Force(float airSpeed)
    {
        Vector3 direction = -bird.refHolder.rb.velocity.normalized;
        float cDrag = DragCoefficient(bird.flightInfo.angleOfAttack);
        if (cDrag < 0) throw new System.Exception($"negative drag coefficient error. aoa:{bird.flightInfo.angleOfAttack}");


        Vector3 force = direction * airSpeed * airSpeed * cDrag;
        forces.UpdateForceGizmo(bird.refHolder.dragForceGizmo, force);
        return force;
    }

    public float DragCoefficient(float angleOfAttack)
    {
        var fm = bird.state.flightMetrics;
        float aoaStall = fm.stallAngleOfAttack;
        float cdPreStall = fm.dragCoefficientPreStall;
        float cdMin = fm.dragCoefficientMin;
        float cdPostStall = fm.dragCoefficientPostStall;
        float cdMax = fm.dragCoefficientMax;
        float stallAngle = fm.stallAngleOfAttack;
        if(Mathf.Abs(angleOfAttack) > aoaStall)
        {
            if (angleOfAttack < 0) angleOfAttack += 180f;
            if (angleOfAttack > 89) angleOfAttack = 89f;
            //non laminar - upside down parabolic
            float angleCentredOnNinety = angleOfAttack - 90f;
            float A = -(cdPostStall) / ((90 - stallAngle) * (90 - stallAngle));
            float coeff = A * angleCentredOnNinety * angleCentredOnNinety + cdMax;
            return coeff;
        }
        else
        {
            //laminar - parabolic
            float A = (cdPreStall - cdMin) / (aoaStall * aoaStall);
            float coeff = A * angleOfAttack * angleOfAttack + cdMin;
            return coeff;
        }
    }
}
