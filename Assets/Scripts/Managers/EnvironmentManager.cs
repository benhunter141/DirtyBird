using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public Environment currentEnvironment;
    public Transform currentTerrain;
    public float gravityFactor;

    private void Start()
    {
        var go = currentEnvironment.Generate();
        currentTerrain = go.transform;
    }

    public Transform terrain;

    public float Elevation(Vector3 position) => currentEnvironment.Elevation(position, currentTerrain);
    public Vector3 UpAt(Vector3 position) => currentEnvironment.UpAt(position, currentTerrain);
    public float PitchAt(Vector3 position, Vector3 direction) => currentEnvironment.PitchAt(position, direction, currentTerrain);

    //public Vector3 HorizonAt(Vector3 position) => currentEnvironment.Horiz

    //public float Elevation(Vector3 point) => Vector3.Distance(cylinder.position, point) + cylinder.position.y;
    //public Vector3 UpAt(Vector3 point) => (point - cylinder.position).normalized;
    //public Vector3 HorizonAt(Vector3 point) => Vector3.Cross(UpAt(point), cylinder.up);
    //public Vector3 CylinderAxis() => cylinder.up;
}
