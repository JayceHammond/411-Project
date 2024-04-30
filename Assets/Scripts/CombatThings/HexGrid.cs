using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
        //TODO: add properties for grid size, hex size, and hex prefab
        [field:SerializeField] public HexOrientation Orientation { get; private set; }
        [field:SerializeField] public int Width { get; private set; }
        [field:SerializeField] public int Height { get; private set; }
        [field:SerializeField] public float HexSize { get; private set; }
        [field:SerializeField] public GameObject HexPrefab { get; private set; }
        public GameObject activeCam;
        private Vector3 lastCamPosition;
        public float maxDistance;


        //TODO: Create a grid of hexes
        //TODO: Store the individual tiles in an array
        //TODO: Methods to get, change, add, and remove tiles
        //TODO: Gizmo for drawing the grid in the editor

//method to draw grid

public void Start(){
    lastCamPosition = activeCam.transform.position;
}

public void Update(){
    if(activeCam.transform.position != lastCamPosition){
        ClearGridLines();
        DrawHexGrid();
        lastCamPosition = activeCam.transform.position;
    }
    
}
private void DrawHexGrid()
{
    //Camera activeCam = Camera.current;

    Vector3 cameraPos = activeCam.transform.position;
    //float maxDistance = 10f; //will adjust later

    for (int z = 0; z < Height; z++)
    {
        for (int x = 0; x < Width; x++)
        {
            Vector3 centerPosition = HexMetrics.Center(HexSize, x, z, Orientation) + transform.position;
            if(IsChunkVisible(centerPosition, cameraPos, maxDistance)){
                DrawChunk(centerPosition);
            }
        }
    }
}

private bool IsChunkVisible(Vector3 chunkPosition, Vector3 cameraPosition, float maxDistance){
    float distanceToCamera = Vector3.Distance(chunkPosition, cameraPosition);
    return distanceToCamera <= maxDistance;
}

/*
private void DrawChunk(Vector3 centerPosition){
    for(int x = 0; x < HexMetrics.Corners(HexSize, Orientation).Length; x++){
        Gizmos.DrawLine(
            centerPosition + HexMetrics.Corners(HexSize, Orientation)[x % 6],
            centerPosition + HexMetrics.Corners(HexSize, Orientation)[(x + 1) % 6]
        );
    }
}
*/

private void DrawChunk(Vector3 centerPosition)
{
    LineRenderer lineRenderer = new GameObject("LineRenderer").AddComponent<LineRenderer>();
    lineRenderer.transform.SetParent(gameObject.transform);
    lineRenderer.positionCount = 7; // Number of vertices per hexagon

    Terrain terrain = Terrain.activeTerrain;
    TerrainData terrainData = terrain.terrainData;

    Vector3[] corners = HexMetrics.Corners(HexSize, Orientation);

    for (int i = 0; i < corners.Length; i++)
    {
        Vector3 cornerPosition = centerPosition + corners[i];
        float normalizedX = Mathf.InverseLerp(0, terrainData.size.x, cornerPosition.x);
        float normalizedZ = Mathf.InverseLerp(0, terrainData.size.z, cornerPosition.z);
        float terrainHeight = terrainData.GetHeight(Mathf.RoundToInt(normalizedX * (terrainData.heightmapResolution - 1)), Mathf.RoundToInt(normalizedZ * (terrainData.heightmapResolution - 1)));

        cornerPosition.y = terrainHeight + terrain.transform.position.y; // Adjust Y-coordinate based on terrain height
        lineRenderer.SetPosition(i, cornerPosition);
    }

    lineRenderer.startWidth = 0.1f; // Adjust line width as needed
    lineRenderer.endWidth = 0.1f;
    lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
    lineRenderer.material.color = Color.white;

    // Connect the last corner to the first to complete the hexagon
    lineRenderer.SetPosition(6, lineRenderer.GetPosition(0)); // Set the last position to match the first position
}



private void ClearGridLines()
{
    LineRenderer[] lineRenderers = GetComponentsInChildren<LineRenderer>();
    foreach (LineRenderer lineRenderer in lineRenderers)
    {
        Destroy(lineRenderer.gameObject);
    }
}



public enum HexOrientation
{
    FlatTop,
    PointyTop
}
}
