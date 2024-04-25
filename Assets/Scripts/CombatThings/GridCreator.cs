using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{

    public GameObject hexagonPrefab;
    public int gridSizeX = 500;
    public int gridSizeZ = 500;
    public float hexOffset = 1.1f; //distance between hex centers
    void Start()
    {
        GenerateHexGrid();
    }

    void GenerateHexGrid()
    {
        float xOffset = Mathf.Sqrt(3) * hexOffset;
        float zOffset = 1.5f * hexOffset;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                float xPos = x * xOffset;
                float zPos = z * zOffset;

                //offset every other row
                if (x % 2 ==1)
                    zPos += hexOffset * 0.5f;

                Vector3 position = new Vector3(xPos, 0, zPos);
                Instantiate(hexagonPrefab, position, Quaternion.identity, transform);
            }
        }
    }
}
