using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Plane : Environment
{
    public float x, z;
    public Material ground;

    public override float Elevation(Vector3 position, Transform terrain) => position.y;
    public override Material GetMaterial() => ground;
    public override Vector3 UpAt(Vector3 position, Transform terrain) => Vector3.up;
    protected override Vector3 HorizonAt(Vector3 position, Vector3 direction, Transform terrain)
    {
        direction.y = 0;
        return direction.normalized;
    }
    protected override Mesh MakeMesh()
    {
        //tris, verts
        var tris = new List<int>();
        var verts = new List<Vector3>();

        verts.Add(new Vector3(-x / 2, 0, -z / 2));
        verts.Add(new Vector3(x / 2, 0, -z / 2));
        verts.Add(new Vector3(x / 2, 0, z / 2));
        verts.Add(new Vector3(-x / 2, 0, z / 2));

        tris.AddRange(new List<int> { 2, 1, 0 });
        tris.AddRange(new List<int> { 3, 2, 0 });

        Mesh mesh = new Mesh();
        
        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();

        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        //later: uvs, normals etc.

        return mesh;
    }


}
