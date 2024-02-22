using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;

public class SidebarUI : MonoBehaviour
{

    public VisualElement root;
    public GameObject selectedObject;
    private GameObject lastSelectedObject;
    private ChangesNTransform changesOfObject;
    private GetTransforms transformsOfObject;
    private SetTransforms settingObjectTransform;

    public Dictionary<string, float> intialTransformValues = new Dictionary<string, float>()
    {
        {"posX", 0.00f},
        {"posY", 0.00f},
        {"posZ", 0.00f},

        {"rotX", 0.00f},
        {"rotY", 0.00f},
        {"rotZ", 0.00f},

        {"scalX", 0.00f},
        {"scalY", 0.00f},
        {"scalZ", 0.00f},
    };

    void Start(){
        root = GetComponent<UIDocument>().rootVisualElement;
        lastSelectedObject = null;
    }

    void Update(){     
        //Debug.Log(root.Q<Label>("Object_Selected").ToString());   
        changeSelectLable();
        if(lastSelectedObject != selectedObject){
            transformsOfObject.getCurrentPosition(selectedObject);
            transformsOfObject.getCurrentRotation(selectedObject);
            transformsOfObject.getCurrentScale(selectedObject);

            lastSelectedObject = selectedObject;
        }
        if(changesOfObject.checkForChanges(selectedObject)){
            settingObjectTransform.setCurrentPosition(selectedObject);
            settingObjectTransform.setCurrentRotation(selectedObject);
            settingObjectTransform.setCurrentScale(selectedObject);
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
    
}
