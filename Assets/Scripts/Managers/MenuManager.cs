using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Transform startingPosition;
    public void Restart()
    {
        BirdController bird = ServiceLocator.Instance.birdController;
        //place bird at start
        bird.transform.position = startingPosition.position;
        //Set state to single flap
        bird.SetStateTo(bird.refHolder.flapping); //???? This isn't enough to actually cause a flap... How is flap normally called?
        bird.state.EnterState(bird);
        //input -> 
        //rotation = aiming fwd
        bird.transform.rotation = startingPosition.rotation;
        bird.refHolder.rb.velocity = Vector3.zero;
    }
}
