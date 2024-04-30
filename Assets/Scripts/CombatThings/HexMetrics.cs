using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.U2D;
using static HexGrid;
using Vector3 = UnityEngine.Vector3;

public class HexMetrics 
{
    public static float OuterRadius (float hexSize)
    {
        return hexSize;
    }

    public static float InnerRadius (float hexSize)
    {
        return hexSize * 0.866025404f;
    }

    //method to grab the corners
    public static Vector3[] Corners(float hexSize, HexOrientation orientation)
    {
        Vector3[] corners = new Vector3[6];
        for (int i = 0; i < 6; i++)
        {
            corners[i] = Corner(hexSize, orientation, i);
        }
        return corners;
    }

    //method to grab a single corner
    public static Vector3 Corner(float hextSize, HexOrientation orientation, int index)
    {
        float angle = 60f * index;
        if (orientation == HexOrientation.PointyTop)
        {
            angle += 30f;
        }
        Vector3 corner = new Vector3(hextSize * Mathf.Cos(angle * Mathf.Deg2Rad),
            0f,
            hextSize * Mathf.Sin(angle * Mathf.Deg2Rad)
        );
        return corner;
    }

    //method to get hex center point
    public static Vector3 Center(float hexSize, int x, int z, HexOrientation orientation)
    {
        Vector3 centerPosition;
        if(orientation == HexOrientation.PointyTop)
        {
            centerPosition.x = (x + z * 0.5f - z / 2) * (InnerRadius(hexSize) * 2f);
            centerPosition.y = 0f;
            centerPosition.z = z * (OuterRadius(hexSize) * 1.5f);
        }
        else
        {
            centerPosition.x = (x) * (OuterRadius(hexSize) *1.5f);
            centerPosition.y = 0f;
            centerPosition.z = (z + x * 0.5f - x / 2) * (InnerRadius(hexSize) * 2f);
        }
        return centerPosition;
    }

    public static Vector3 OffsetToCube(int col, int row, HexOrientation orientation)
    {
        if (orientation == HexOrientation.PointyTop)
        {
            return AxialToCube(OffsetToAxialPointy(col, row));
        }
        else
        {
            return AxialToCube(OffsetToAxialFlat(col, row));
        }
    }

    public static Vector3 AxialToCube(Vector2Int axial)
{
    float x = axial.x;
    float z = axial.y;
    float y = -x - z;
    return new Vector3(x, z, y);
}
public static Vector2Int OffsetToAxialFlat(int col, int row)
{
    int q = col;
    int r = row - (col + (col & 1)) / 2;
    return new Vector2Int(q, r);
}

public static Vector2Int OffsetToAxialPointy(int col, int row)
{
    int q = col - (row + (row & 1)) / 2;
    int r = row;
    return new Vector2Int(q, r);
} 


}
