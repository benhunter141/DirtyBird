using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift
{
    BirdController bird;
    Forces forces;
    public Lift(BirdController _bird, Forces _forces)
    {
        bird = _bird;
        forces = _forces;
    }

    public Vector3 Force(float airSpeed)
    {
        //change to be perpendicular to rb.vel
        Vector3 direction = Vector3.Cross(bird.refHolder.rb.velocity.normalized, bird.transform.right);
        float coeff = LiftCoefficient(bird.flightInfo.angleOfAttack);
        Vector3 force = direction * airSpeed * airSpeed * coeff;
        forces.UpdateForceGizmo(bird.refHolder.liftForceGizmo, force);
        return force;
    }

    

    public float LiftCoefficient(float angleOfAttack)
    {
        var fm = bird.state.flightMetrics;
        if (angleOfAttack < fm.minAngleOfAttack ||
            angleOfAttack > fm.stallAngleOfAttack)
        {
            //Stall!!!
            ServiceLocator.Instance.uiManager.IssueStallMessage();
            return 0;
        }

        if(angleOfAttack  < fm.maxAngleOfAttack)
        {
            //main regime, cL increases as angle of attack increases up to cLMax/aoaMax
            ServiceLocator.Instance.uiManager.RescindStallMessage();

            float m1 = M1(fm.minLiftCoefficient,
                fm.maxLiftCoefficient,
                fm.minAngleOfAttack,
                fm.maxAngleOfAttack);
            float b1 = B1(fm.minLiftCoefficient,
                m1,
                fm.minAngleOfAttack);
            float cL = m1 * angleOfAttack + b1;
            return cL;
        }
        else if(angleOfAttack >= fm.maxAngleOfAttack)
        {
            //cL decreases as angle approaches stall angle
            ServiceLocator.Instance.uiManager.IssueStallWarning();

            float m2 = M2(fm.maxLiftCoefficient,
                fm.stallLiftCoefficient,
                fm.maxAngleOfAttack,
                fm.stallAngleOfAttack);
            float b2 = B2(fm.maxLiftCoefficient, 
                m2,
                fm.maxAngleOfAttack);
            float cL = m2 * angleOfAttack + b2;
            return cL;
        }


        throw new System.Exception();
        

        //Optimize by doing this once:
        float M1(float clMin, float clMax, float aoaMin, float aoaMax) => (clMin - clMax) / (aoaMin - aoaMax);

        float M2(float clMax, float clStall, float aoaMax, float aoaStall) => (clMax - clStall) / (aoaMax - aoaStall);

        float B1(float clMin, float m1, float aoaMin) => clMin - m1 * aoaMin;

        float B2(float clMax, float m2, float aoaMax) => clMax - m2 * aoaMax;
    }
}
