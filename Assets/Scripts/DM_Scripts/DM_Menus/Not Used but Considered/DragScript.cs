using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragScript : MonoBehaviour
{

    private Vector3 mousePositionOffset;
    private float mouseZCoord;

   private Vector3 GetMouseWorldPosition(){
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mouseZCoord;
        return  GameObject.Find("DM Camera").GetComponent<Camera>().WorldToScreenPoint(mousePosition);
    }

    private void OnMouseDown(){
        mouseZCoord = GameObject.Find("DM Camera").GetComponent<Camera>().WorldToScreenPoint(gameObject.transform.position).z;
        mousePositionOffset = gameObject.transform.position - GetMouseWorldPosition();
        Debug.Log(mousePositionOffset);
    }
    private void OnMouseDrag(){
        transform.position = (GetMouseWorldPosition() - mousePositionOffset)/500;
        Debug.Log(transform.position);
    }
}
