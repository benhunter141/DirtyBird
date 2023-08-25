using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceHolder : MonoBehaviour
{
    public Rigidbody rb;
    public Transform flapForceGizmo, liftForceGizmo, dragForceGizmo, gravityForceGizmo;
    public BirdState standing, launching, gliding, flapping, subsequentFlapping;

}
