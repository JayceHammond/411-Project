using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SidebarUI : MonoBehaviour
{

    private VisualElement root;
    public GameObject selectedObject;
    private GameObject lastSelectedObject;

    void Start(){
        root = GetComponent<UIDocument>().rootVisualElement;
    }

    void Update(){     
        //Debug.Log(root.Q<Label>("Object_Selected").ToString());   
        changeSelectLable();
        if(lastSelectedObject != selectedObject){
            setCurrentPosition();
        }
    }

    private void changeSelectLable(){

        //Debug.Log(selectedObject.name);
       
        if (null != selectedObject)
        {
            root.Q<Label>("Object_Selected").text = selectedObject.name;
            //Debug.Log(root.Q<Label>("Object_Selected").text + "The Name!!");
        }
        else
        {
            root.Q<Label>("Object_Selected").text= "No Object Selected";
        }
    }

    private void setCurrentPosition(){
        if(null != selectedObject){
            root.Q<VisualElement>("Position").Q<FloatField>("X").value = selectedObject.transform.position.x;
            root.Q<VisualElement>("Position").Q<FloatField>("Y").value = selectedObject.transform.position.y;
            root.Q<VisualElement>("Position").Q<FloatField>("Z").value = selectedObject.transform.position.z;
        }else{

        }
    }
}
