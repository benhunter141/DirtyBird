using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour
{
    public Material material;
    public GameObject cursor, cam;
    public VoxelMap map;
    new MeshRenderer renderer;
    MeshFilter filter;

    public void DebugRR()
    {
        
        for(int i = 0; i < 4; i++)
        {
            Debug.Log($"Vertex at: {filter.mesh.vertices[i].x}, {filter.mesh.vertices[i].y}, {filter.mesh.vertices[i].z}");
        }
        for(int i = 0; i < 6; i++)
        {
            Debug.Log($"Tri index {i}: {filter.mesh.triangles[i]}");
        }
    }
    
    public void ClearToAirFill()
    {
        map.ClearMapAndFillWithAir();
    }

    public void WriteToTextFile()
    {
        map.WriteMapDataToTextFile();
    }

    public void ReadFromTextFile()
    {
        map.ReadMapDataFromTextFile();
    }

    private void LateUpdate()
    {
        if (!map.hasChanged) return;
        map.hasChanged = false;
        RefreshRenderer();
    }

    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
        filter = GetComponent<MeshFilter>();
        map.ClearMapAndFillWithAir();
    }

    void Update()
    {
        Vector3 mouseWorldPos = MouseWorldPosition();
        if (cursor.activeSelf)
        {
            UpdatePositionOfCursor(mouseWorldPos);
            if (Input.GetMouseButton(0))
            {
                //Debug.Log($"mouse clicked at world pos: {mouseWorldPos.x}, {mouseWorldPos.y}, {mouseWorldPos.z}");
                CreateLandChunk(mouseWorldPos);
            }
        }
        else
        {
            if(Input.GetMouseButton(0))
            {
                DestroyLandChunk(mouseWorldPos);
            }
        }

    }


    void RefreshRenderer()
    {
        Debug.Log("Refreshing Rendererer");
        var mesh = map.MakeMesh();
        mesh.name = "Freshly-Squeezed Mesh";
        filter.mesh = mesh;
        renderer.material = material;
    }
    void DestroyLandChunk(Vector3 worldPosition)
    {
        Vector2Int coords = map.GetCoords(worldPosition);
        //Debug.Log($"coords:{coords.x}, {coords.y}");
        if (map.CoordsOutOfGrid(coords)) return;
        map.ChangeVoxelTo(Voxel.Air, coords);
    }
    void CreateLandChunk(Vector3 worldPosition)
    {
        Vector2Int coords = map.GetCoords(worldPosition);
        //Debug.Log($"coords:{coords.x}, {coords.y}");
        if (map.CoordsOutOfGrid(coords)) return;
        map.ChangeVoxelTo(Voxel.Land, coords);
    }


    void UpdatePositionOfCursor(Vector3 mouseWorldPos)
    {
        cursor.SetActive(true);
        cursor.transform.position = mouseWorldPos;
    }

    Vector3 MouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(cam.transform.position.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        return worldPos;
    }
    public void ActivatePaintBrush()
    {
        cursor.SetActive(true);
    }

    public void CancelPaintBrush()
    {
        cursor.SetActive(false);
    }
}
