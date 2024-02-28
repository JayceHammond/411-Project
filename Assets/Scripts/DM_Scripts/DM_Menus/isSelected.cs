using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class isSelected : MonoBehaviour, IPointerClickHandler
{
    public SidebarUI sidebarUI;

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log(this.gameObject + " :Is the clicked object");
        sidebarUI.selectedObject = this.gameObject;
    }

    public GameObject OnPointerClickGameObject(PointerEventData eventData)
    {
        return this.gameObject;
    }

}