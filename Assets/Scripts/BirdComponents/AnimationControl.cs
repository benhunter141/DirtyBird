using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl
{
    BirdController bird;
    Animator anim;
    

    public AnimationControl(BirdController _bird, Animator _anim)
    {
        bird = _bird;
        anim = _anim;
 
    }

    public void Launch()
    {
        anim.Play("CompleteLaunch");
        //when finished, anim should auto move the head
    }

    public void Flap()
    {
        anim.Play("SingleFlap");
    }

    public void SubsequentFlap()
    {
        anim.Play("SubsequentFlap");
    }

    public void Glide()
    {
        anim.Play("FlyingPosition");
    }

}
