using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentalObject : MonoBehaviour
{
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 down = -ServiceLocator.Instance.environmentManager.UpAt(transform.position);
        rb.AddForce(down * ServiceLocator.Instance.environmentManager.gravityFactor * rb.mass); //gravity factor should be elsewhere... enviroData
    }
}
