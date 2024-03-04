using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragScript : MonoBehaviour, IDragHandler, IBeginDragHandler
{

    private Vector3 mousePositionOffset;
    private float mouseZCoord;

    private Vector3 GetMouseWorldPosition(){
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mouseZCoord;
        return  GameObject.Find("DM Camera").GetComponent<Camera>().ScreenToWorldPoint(mousePosition);
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData){
        mouseZCoord = GameObject.Find("DM Camera").GetComponent<Camera>().WorldToViewportPoint(gameObject.transform.position).z; //Causing Issues
        //Changing the Z cord is tricky with out moving the camera to transform the Z axis into an X axis
        mousePositionOffset = gameObject.transform.position - GetMouseWorldPosition();
        Debug.Log(mousePositionOffset);
    }
    void IDragHandler.OnDrag(PointerEventData eventData){
        transform.position = GetMouseWorldPosition() - mousePositionOffset;
        //Debug.Log(transform.position + ":" + GetMouseWorldPosition());
    }

}
