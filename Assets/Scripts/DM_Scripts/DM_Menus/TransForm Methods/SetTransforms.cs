using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SetTransforms : MonoBehaviour
{
    public VisualElement root;
    private SidebarUI sidebarUI;
    void Start(){
        root = sidebarUI.root;
    }

    public Vector3 setCurrentPosition(GameObject selectedObject){
        if (null != selectedObject){
            UnityEngine.Vector3 newPostion;

            newPostion.x = root.Q<VisualElement>("Position").Q<FloatField>("X").value;
            newPostion.y = root.Q<VisualElement>("Position").Q<FloatField>("Y").value;
            newPostion.z = root.Q<VisualElement>("Position").Q<FloatField>("Z").value;

            return newPostion;
        }

        return selectedObject.transform.position;
    }

    public Quaternion setCurrentRotation(GameObject selectedObject){
        if (null != selectedObject){
            UnityEngine.Quaternion newRotation;

            newRotation.x = root.Q<VisualElement>("Rotation").Q<FloatField>("X").value;
            newRotation.y = root.Q<VisualElement>("Rotation").Q<FloatField>("Y").value;
            newRotation.z = root.Q<VisualElement>("Rotation").Q<FloatField>("Z").value;
            newRotation.w = selectedObject.transform.rotation.w;


            return newRotation;
        }

        return selectedObject.transform.rotation;
    }

    public Vector3 setCurrentScale(GameObject selectedObject){
        if (null != selectedObject){
            UnityEngine.Vector3 newScale;

            newScale.x = root.Q<VisualElement>("Position").Q<FloatField>("X").value;
            newScale.y = root.Q<VisualElement>("Position").Q<FloatField>("Y").value;
            newScale.z = root.Q<VisualElement>("Position").Q<FloatField>("Z").value;

            return newScale;
        }

        return selectedObject.transform.localScale;
    }

}
