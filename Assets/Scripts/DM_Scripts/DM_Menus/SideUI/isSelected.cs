using UnityEngine;
using UnityEngine.EventSystems;

public class isSelected : MonoBehaviour, IPointerClickHandler
{
    public SidebarUI sidebarUI;

    private float Clicked = 0;
    private float ClickTime = 0;
    private float ClickTimeDelay = 0.75f;

    void Awake(){
        if(sidebarUI == null){
            sidebarUI = GameObject.FindGameObjectWithTag("BuildingUI").GetComponent<SidebarUI>();
        }
        //Debug.Log(sidebarUI);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked++;
        if(Clicked == 1){
            ClickTime = Time.time;
        }
        if((Clicked == 2) && (Time.time - ClickTime) < ClickTimeDelay){
            //Debug.Log(this.gameObject + " :Is the clicked object");
            sidebarUI.selectedObject = this.gameObject;

            Clicked = 0;
            ClickTime = 0;

        }else if(Clicked > 2 || Time.time - ClickTime > 1){
            Clicked = 0; 
        }
    }

}