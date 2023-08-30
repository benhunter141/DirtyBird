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
    public float depth;
    public Vector2Int startPosition;
    public Vector2Int endPosition;
    public string path;

    public bool hasChanged;
    [SerializeField]
    public List<List<Voxel>> map;


    public void ReadMapDataFromTextFile()
    {
        StreamReader sr = new StreamReader(path);
        var lines = new List<string>();
        while(!sr.EndOfStream)
        {
            var line = sr.ReadLine();
            lines.Add(line);
        }
        lines.Reverse();

        for(int j = 0; j < lines.Count; j++)
        {
            for(int i = 0; i < lines[j].Length; i++)
            {
                char c = lines[j][i];
                if (c == 'A')
                {
                    //Debug.Log("A found");
                    map[j][i] = Voxel.Air;
                }
                else if (c == 'L')
                {
                    //Debug.Log("L found");
                    map[j][i] = Voxel.Land;
                }
            }
        }
        hasChanged = true;
    }
    public void WriteMapDataToTextFile()
    {
        File.WriteAllText(path, string.Empty);
        string mapData = string.Empty;
        for (int j = height - 1; j >= 0; j--)
        {
            for (int i = 0; i < width; i++)
            {
                Voxel v = map[j][i];
                if (v == Voxel.Land)
                {
                    mapData += "L";
                }
                if (v == Voxel.Air)
                {
                    mapData += "A";
                }
            }
            if (j == 0) continue;
            mapData += "\n";
        }
        File.AppendAllText(path, mapData);
        //Re-Import
        AssetDatabase.ImportAsset(path);
        Debug.Log("Written to txt");
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
                Voxel v = map[j][i];
                if (v != Voxel.Land) continue;
                //Add Facade
                Vector3 voxelCentre = new Vector3(i * voxelSize, j * voxelSize);
                float halfSize = voxelSize / 2;
                Vector3 botLeft = voxelCentre + new Vector3(-halfSize, -halfSize);
                Vector3 botRight = voxelCentre + new Vector3(halfSize, -halfSize);
                Vector3 topRight = voxelCentre + new Vector3(halfSize, halfSize);
                Vector3 topLeft = voxelCentre + new Vector3(-halfSize, halfSize);
                Vector3 topLeftDeep = topLeft + new Vector3(0, 0, depth);
                Vector3 topRightDeep = topRight + new Vector3(0, 0, depth);
                Vector3 botRightDeep = botRight + new Vector3(0, 0, depth);
                int triIndex = verts.Count;
                verts.Add(botLeft);
                verts.Add(botRight);
                verts.Add(topRight);
                verts.Add(topLeft);
                verts.Add(topLeftDeep);
                verts.Add(topRightDeep);
                verts.Add(botRightDeep);
                tris.Add(triIndex);
                tris.Add(triIndex + 2);
                tris.Add(triIndex + 1);
                tris.Add(triIndex);
                tris.Add(triIndex + 3);
                tris.Add(triIndex + 2);

                if (NeedsTopFace(i,j))
                {
                    tris.Add(triIndex + 3);
                    tris.Add(triIndex + 5);
                    tris.Add(triIndex + 2);
                    tris.Add(triIndex + 3);
                    tris.Add(triIndex + 4);
                    tris.Add(triIndex + 5);
                }
                if (NeedsRightFace(i,j))
                {
                    tris.Add(triIndex + 1);
                    tris.Add(triIndex + 2);
                    tris.Add(triIndex + 5);
                    tris.Add(triIndex + 1);
                    tris.Add(triIndex + 5);
                    tris.Add(triIndex + 6);
                }
            }
        }
        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.RecalculateTangents();
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;

        bool NeedsTopFace(int x, int y) => y == height - 1 || map[y+1][x] != Voxel.Land;
        bool NeedsRightFace(int x, int y) => x == width - 1 || map[y][x+1] != Voxel.Land;
    }

    public void ClearMapAndFillWithAir()
    {
        map = new List<List<Voxel>>();
        for(int j = 0; j < height; j++)
        {
            var row = new List<Voxel>();
            map.Add(row);
            for (int i = 0; i < width; i++)
            {
                row.Add(Voxel.Air);
            }
        }
        hasChanged = true;
    }

    public void ChangeVoxelTo(Voxel v, Vector2Int coords)
    {
        if (map[coords.y][coords.x] == v) return;
        map[coords.y][coords.x] = v;
        hasChanged = true;
    }

    public Vector2Int GetCoords(Vector3 pos)
    {
        if (Mathf.Abs(pos.z) > 0.01f) throw new System.Exception("Z coordinate should be zero");
        int xCoord = (int)Mathf.Round(pos.x / voxelSize);
        int yCoord = (int)Mathf.Round(pos.y / voxelSize);
        //if out of grid, we want methods calling this to know that and to do nothing. 


        return new Vector2Int(xCoord, yCoord);
    }

    public bool CoordsOutOfGrid(Vector2Int coords)
    {
        if (coords.x < 0 || coords.y < 0) return true;
        if (coords.x >= width || coords.y >= height) return true;
        return false;
    }
}
