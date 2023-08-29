using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Environment : ScriptableObject, IHasMaterial
{
    //public Transform terrain;
    protected abstract Mesh MakeMesh();
    public GameObject Generate()
    {
        Mesh mesh = MakeMesh();
        var obj = new GameObject();
        var filter = obj.AddComponent<MeshFilter>();
        filter.mesh = mesh;
        var rr = obj.AddComponent<MeshRenderer>(); //multiple materials? later
        rr.material = GetMaterial();
        obj.AddComponent<MeshCollider>();
        obj.name = this.name;
        return obj;
    }
    public abstract float GroundPositionAt(float z);
    public abstract float Elevation(Vector3 position, Transform terrain);
    public abstract Vector3 UpAt(Vector3 position, Transform terrain);
    protected abstract Vector3 HorizonAt(Vector3 position, Vector3 direction, Transform terrain);
    protected Vector3 HorizonRight(Vector3 position, Vector3 direction, Transform terrain)
    {
        return Vector3.Cross(HorizonAt(position, direction, terrain), UpAt(position, terrain));
    }
    public float PitchAt(Vector3 position, Vector3 direction, Transform terrain)
    {
        Vector3 horizon = HorizonAt(position, direction, terrain);
        Vector3 right = HorizonRight(position, direction, terrain);
        float pitch = Vector3.SignedAngle(direction, horizon, -right);
        return pitch;
    }

    public abstract Material GetMaterial();

    Material IHasMaterial.GetMaterial()
    {
        throw new System.NotImplementedException();
    }
}
