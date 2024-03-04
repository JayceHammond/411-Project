using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SidebarUI : MonoBehaviour
{

    public VisualElement root;
    public GameObject selectedObject;
    private GameObject lastSelectedObject;

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

    void OnEnable(){
        root = GetComponent<UIDocument>().rootVisualElement;
        lastSelectedObject = null;
    }

    void Update(){     
        //Debug.Log(root.Q<Label>("Object_Selected").ToString());   
        changeSelectLable();
        if (selectedObject != null)
        {
            if (lastSelectedObject == selectedObject){
                
                root.Q<VisualElement>("Position").Q<Button>("Reset").clickable.clicked += () => resetPosition(selectedObject);
                root.Q<VisualElement>("Rotation").Q<Button>("Reset").clickable.clicked += () => resetRotaion(selectedObject);
                root.Q<VisualElement>("Scale").Q<Button>("Reset").clickable.clicked += () => resetScale(selectedObject);

            }else{

                getCurrentPosition(selectedObject);
                getCurrentRotation(selectedObject);
                getCurrentScale(selectedObject);

                lastSelectedObject = selectedObject;
            }

            if (checkForChanges(selectedObject)){
                selectedObject.transform.position = setCurrentPosition(selectedObject);
                selectedObject.transform.rotation = setCurrentRotation(selectedObject);
                selectedObject.transform.localScale = setCurrentScale(selectedObject);
            }

        }else{
            selectedObject = null;
            lastSelectedObject = null;
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
    
    public void getCurrentPosition(GameObject selectedObject){
        if(null != selectedObject){
            root.Q<VisualElement>("Position").Q<FloatField>("X").value = selectedObject.transform.position.x;
            intialTransformValues["posX"] = selectedObject.transform.position.x;
            root.Q<VisualElement>("Position").Q<FloatField>("Y").value = selectedObject.transform.position.y;
            intialTransformValues["posY"] = selectedObject.transform.position.y;
            root.Q<VisualElement>("Position").Q<FloatField>("Z").value = selectedObject.transform.position.z;
            intialTransformValues["posZ"] = selectedObject.transform.position.z;
        }
    }

    public void getCurrentRotation(GameObject selectedObject){
        if(null != selectedObject){
            root.Q<VisualElement>("Rotation").Q<FloatField>("X").value = selectedObject.transform.rotation.x;
            intialTransformValues["rotX"] = selectedObject.transform.rotation.x;
            root.Q<VisualElement>("Rotation").Q<FloatField>("Y").value = selectedObject.transform.rotation.y;
            intialTransformValues["rotY"] = selectedObject.transform.rotation.y;
            root.Q<VisualElement>("Rotation").Q<FloatField>("Z").value = selectedObject.transform.rotation.z;
            intialTransformValues["rotZ"] = selectedObject.transform.rotation.z;
        }
    }

    public void getCurrentScale(GameObject selectedObject){
        if(null != selectedObject){
            root.Q<VisualElement>("Scale").Q<FloatField>("X").value = selectedObject.transform.localScale.x;
            intialTransformValues["scalX"] = selectedObject.transform.localScale.x;
            root.Q<VisualElement>("Scale").Q<FloatField>("Y").value = selectedObject.transform.localScale.y;
            intialTransformValues["scalY"] = selectedObject.transform.localScale.y;
            root.Q<VisualElement>("Scale").Q<FloatField>("Z").value = selectedObject.transform.localScale.z;
            intialTransformValues["scalZ"] = selectedObject.transform.localScale.z;
        }
    }

    public bool checkForChanges(GameObject selectedObject){
        bool changes = false;
        changes = changesInPosition(selectedObject) || changesInRotaion(selectedObject) || changesInScale(selectedObject);
        return changes;
    }

    private bool changesInPosition(GameObject selectedObject){
        Vector3 position = selectedObject.transform.position;

        Vector3 showedPosition;
        showedPosition.x = root.Q<VisualElement>("Position").Q<FloatField>("X").value;
        showedPosition.y = root.Q<VisualElement>("Position").Q<FloatField>("Y").value;
        showedPosition.z = root.Q<VisualElement>("Position").Q<FloatField>("Z").value;

        if (position.Equals(showedPosition))
            return true;

        return false;
    }

    private bool changesInRotaion(GameObject selectedObject){
        Quaternion rotation = selectedObject.transform.rotation;

        Quaternion showedRotation;
        showedRotation.x = root.Q<VisualElement>("Rotation").Q<FloatField>("X").value;
        showedRotation.y = root.Q<VisualElement>("Rotation").Q<FloatField>("Y").value;
        showedRotation.z = root.Q<VisualElement>("Rotation").Q<FloatField>("Z").value;
        showedRotation.w = rotation.w;

        if (rotation.Equals(showedRotation))
            return true;

        return false;
    }

    private bool changesInScale(GameObject selectedObject){
        Vector3 scale = selectedObject.transform.localScale;

        Vector3 showedScale;
        showedScale.x = root.Q<VisualElement>("Scale").Q<FloatField>("X").value;
        showedScale.y = root.Q<VisualElement>("Scale").Q<FloatField>("Y").value;
        showedScale.z = root.Q<VisualElement>("Scale").Q<FloatField>("Z").value;

        if (scale.Equals(showedScale))
            return true;

        return false;
    }

    public Vector3 setCurrentPosition(GameObject selectedObject){
        if (null != selectedObject){
            Vector3 newPostion;

            newPostion.x = root.Q<VisualElement>("Position").Q<FloatField>("X").value;
            newPostion.y = root.Q<VisualElement>("Position").Q<FloatField>("Y").value;
            newPostion.z = root.Q<VisualElement>("Position").Q<FloatField>("Z").value;

            return newPostion;
        }

        return selectedObject.transform.position;
    }

    public Quaternion setCurrentRotation(GameObject selectedObject){
        if (null != selectedObject){
            Quaternion newRotation;

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
            Vector3 newScale;

            newScale.x = root.Q<VisualElement>("Scale").Q<FloatField>("X").value;
            newScale.y = root.Q<VisualElement>("Scale").Q<FloatField>("Y").value;
            newScale.z = root.Q<VisualElement>("Scale").Q<FloatField>("Z").value;

            return newScale;
        }

        return selectedObject.transform.localScale;
    }

    public void resetPosition(GameObject selectedObject){
        Vector3 oldPosition;

        oldPosition.x = intialTransformValues["posX"];
        oldPosition.y = intialTransformValues["posY"];
        oldPosition.z = intialTransformValues["posZ"];

        selectedObject.transform.localPosition = oldPosition;
        getCurrentPosition(selectedObject);
        Debug.Log(selectedObject.transform.localPosition + " Reset Vals: " + oldPosition);

    }

    public void resetRotaion(GameObject selectedObject){
        Quaternion oldRotation;

        oldRotation.x = intialTransformValues["rotX"];
        oldRotation.y = intialTransformValues["rotY"];
        oldRotation.z = intialTransformValues["rotZ"];
        oldRotation.w = selectedObject.transform.rotation.w;

        selectedObject.transform.localRotation = oldRotation;
        getCurrentRotation(selectedObject);
        Debug.Log(selectedObject.transform.localRotation + " Reset Vals: " + oldRotation);

    }

    public void resetScale(GameObject selectedObject){
        Vector3 oldScale;

        oldScale.x = intialTransformValues["scalX"];
        oldScale.y = intialTransformValues["scalY"];
        oldScale.z = intialTransformValues["scalZ"];

        selectedObject.transform.localScale = oldScale;
        getCurrentScale(selectedObject);
        Debug.Log(selectedObject.transform.localScale + " Reset Vals: " + oldScale);

    }
}
