using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SidebarUI : MonoBehaviour
{

    public VisualElement root;
    public GameObject selectedObject;

    void Start(){
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
    }

    void Update(){        
        changeSelectLable();
    }

    private void changeSelectLable(){

        Debug.Log(selectedObject.name);
        Debug.Log(root.Q<Label>("Object_Selected"));
       
        if (null != selectedObject)
        {
            root.Q<Label>("Object_Selected").text = selectedObject.name;
            //Debug.Log(root.Q<Label>("Object_Selected").text + "The Name!!");
        }
        else
        {
            root.Q<Label>("Object_Selected").text.Equals("No Object Selected");
        }
    }
}
