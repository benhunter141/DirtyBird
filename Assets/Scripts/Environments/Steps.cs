using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Steps : Environment
{
    public int landingCount;
    public float width, landingLength, stepLength, stepHeight;
    public Material material;
    public override float Elevation(Vector3 position, Transform terrain)
    {
        //alternative: raycast from position downward. This would break energy calculation if it's based on this elevation
        return 1000 + position.y;
    }

    public override Material GetMaterial() => material;

    protected override Mesh MakeMesh() //evenly spaced landings
    {
        Mesh mesh = new Mesh();
        //verts
        //tris
        var verts = new List<Vector3>();
        var tris = new List<int>();

        //landing pairs
        Vector3 origin = new Vector3(-width / 2, 0, -2); //back right? corner
        int triStart = 0;
        for(int i = 0; i < landingCount; i++)
        {
            //verts:
            Vector3 a = origin;
            Vector3 b = origin + new Vector3(0, 0, landingLength);
            Vector3 c = origin + new Vector3(width, 0, landingLength);
            Vector3 d = origin + new Vector3(width, 0, 0);
            verts.AddRange(new List<Vector3> { a, b, c, d });
            origin += new Vector3(0, -stepHeight, landingLength + stepLength);
            //tris: landing 
            tris.Add(triStart);
            tris.Add(triStart + 1);
            tris.Add(triStart + 2);
            tris.Add(triStart);
            tris.Add(triStart + 2);
            tris.Add(triStart + 3);
            triStart += 4;
            if (i == 0) continue;
            //then step before
            tris.Add(triStart - 7);
            tris.Add(triStart - 4);
            tris.Add(triStart - 1);
            tris.Add(triStart - 7);
            tris.Add(triStart - 1);
            tris.Add(triStart - 6);
       
        }
        //Debug.Log($"verts:{verts.Count}, tris:{tris.Count}");
        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        return mesh;
    }

    public override Vector3 UpAt(Vector3 position, Transform terrain) => Vector3.up;
    protected override Vector3 HorizonAt(Vector3 position, Vector3 direction, Transform terrain) => Vector3.forward;

    public override float GroundPositionAt(float z)
    {
        //Rough height
        float y = Slope() * z;
        return y;
    }

    float Slope() => -stepHeight / (landingLength + stepLength);
}
