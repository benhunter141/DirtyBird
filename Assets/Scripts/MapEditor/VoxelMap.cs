using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

[CreateAssetMenu(menuName = "Maps/VoxelMap")]
public class VoxelMap : ScriptableObject
{
    public int width, height;
    public float voxelSize;
    public Vector2Int startPosition;
    public Vector2Int endPosition;
    public string path;

    public bool hasChanged;
    [SerializeField]
    public List<List<Voxel>> map;



    public void WriteMapDataToTextFile()
    {
        File.WriteAllText(path, string.Empty);
        string mapData = string.Empty;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Voxel v = map[i][j];
                if (v == Voxel.Land)
                {
                    mapData += "L";
                }
                if (v == Voxel.Air)
                {
                
                    mapData += "A";
                }
            }
            if (i == width - 1) continue;
            mapData += "\n";
        }
        File.AppendAllText(path, mapData);
        //Re-Import
        AssetDatabase.ImportAsset(path);
    }
    public Mesh MakeMesh()
    {
        Mesh mesh = new Mesh();
        var tris = new List<int>();
        var verts = new List<Vector3>();
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                Voxel v = map[i][j];
                if (v != Voxel.Land) continue;
                //Add Facade
                Vector3 voxelCentre = new Vector3(i * voxelSize, j * voxelSize);
                float halfSize = voxelSize / 2;
                Vector3 botLeft = voxelCentre + new Vector3(-halfSize, -halfSize);
                Vector3 botRight = voxelCentre + new Vector3(halfSize, -halfSize);
                Vector3 topRight = voxelCentre + new Vector3(halfSize, halfSize);
                Vector3 topLeft = voxelCentre + new Vector3(-halfSize, halfSize);
                int triIndex = verts.Count;
                verts.Add(botLeft);
                verts.Add(botRight);
                verts.Add(topRight);
                verts.Add(topLeft);
                tris.Add(triIndex);
                tris.Add(triIndex + 2);
                tris.Add(triIndex + 1);
                tris.Add(triIndex);
                tris.Add(triIndex + 3);
                tris.Add(triIndex + 2);

                if (NeedsTopFace(i,j))
                {
                    //Add Top
                    //Debug.Log($"Add top at {i},{j}");
                }
                if (NeedsRightFace(i,j))
                {
                    //Add Right
                    //Debug.Log($"Add right at {i},{j}");
                }
            }
        }
        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();

        return mesh;

        bool NeedsTopFace(int x, int y) => y == height - 1 || map[x][y + 1] != Voxel.Land;
        bool NeedsRightFace(int x, int y) => x == width - 1 || map[x + 1][y] != Voxel.Land;
    }

    public void ClearMapAndFillWithAir()
    {
        map = new List<List<Voxel>>();
        for(int i = 0; i < width; i++)
        {
            var col = new List<Voxel>();
            map.Add(col);
            for (int j = 0; j < height; j++)
            {
                col.Add(Voxel.Air);
            }
        }
        hasChanged = true;
    }

    public void ChangeVoxelTo(Voxel v, Vector2Int coords)
    {
        if (map[coords.x][coords.y] == v) return;
        map[coords.x][coords.y] = v;
        hasChanged = true;
    }

    public Vector2Int GetCoords(Vector3 pos)
    {
        if (Mathf.Abs(pos.z) > 0.01f) throw new System.Exception("Z coordinate should be zero");
        int xCoord = (int)Mathf.Round(pos.x / voxelSize);
        int yCoord = (int)Mathf.Round(pos.y / voxelSize);
        if (xCoord < 0 || yCoord < 0) throw new System.Exception("Negative coord, out of grid");
        if (xCoord >= width || yCoord >= height) throw new System.Exception("coord too high, out of grid");

        return new Vector2Int(xCoord, yCoord);
    }
}
