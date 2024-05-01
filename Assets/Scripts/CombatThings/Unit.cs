using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour 
{
    private HexGrid hexGrid;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private int moveSpeed;

    public void Init(HexGrid grid)
    {
        hexGrid = grid;
    }

    public void MoveTo(Vector3 destination)
    {
        targetPosition = destination;
        isMoving = true;
    }

    private void Update()
    {
        if(isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);
            if(transform.position == targetPosition)
            {
                isMoving = false;
                //hexGrid.UnitMovementComplete();
            }
        }
    }
}
