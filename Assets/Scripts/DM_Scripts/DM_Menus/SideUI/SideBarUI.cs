using System;
using UnityEngine;
using UnityEngine.UIElements;

public class SidebarUI : MonoBehaviour
{

    public VisualElement root;
    public GameObject selectedObject;
    private GameObject lastSelectedObject;

    private Vector3 lastPosition;
    private Quaternion lastRotaion;
    private Vector3 lastScale;

    void OnEnable(){
        root = GetComponent<UIDocument>().rootVisualElement;
        //selectedObject = null;
        lastSelectedObject = null;
    }

    void Update(){     
        //Debug.Log(selectedObject);   
        changeSelectLable();
        if (selectedObject != null){

            if (lastSelectedObject == selectedObject){
                //Debug.Log("The First Part");
                root.Q<VisualElement>("Position").Q<Button>("Reset").clickable.clicked += () => resetPosition(selectedObject);
                root.Q<VisualElement>("Rotation").Q<Button>("Reset").clickable.clicked += () => resetRotaion(selectedObject);
                root.Q<VisualElement>("Scale").Q<Button>("Reset").clickable.clicked += () => resetScale(selectedObject);

            }else{
                //Debug.Log("Im Here!!");
                getCurrentPosition(selectedObject, false);
                getCurrentRotation(selectedObject, false);
                getCurrentScale(selectedObject, false);
                changeName();

                lastSelectedObject = selectedObject;
            }

            if (checkForChanges(selectedObject)){
                selectedObject.transform.position = setCurrentPosition(selectedObject);
                selectedObject.transform.rotation = setCurrentRotation(selectedObject);
                selectedObject.transform.localScale = setCurrentScale(selectedObject);
                selectedObject.name = setName(selectedObject);
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

    private void changeName(){
        TextField NameChanger = root.Q<TextField>("ChangeName");
        if(selectedObject == null){
            NameChanger.value = "No Object Selected";
        }else{
            NameChanger.value = selectedObject.name;
        }
    }
    
    public void getCurrentPosition(GameObject selectedObject, bool reset){
        root.Q<VisualElement>("Position").Q<FloatField>("X").value = selectedObject.transform.position.x;
        root.Q<VisualElement>("Position").Q<FloatField>("Y").value = selectedObject.transform.position.y;
        root.Q<VisualElement>("Position").Q<FloatField>("Z").value = selectedObject.transform.position.z;
            
        if(!reset){
            lastPosition.x = selectedObject.transform.position.x;
            lastPosition.y = selectedObject.transform.position.y;
            lastPosition.z = selectedObject.transform.position.z;
        }
    }

    public void getCurrentRotation(GameObject selectedObject, bool reset){

        root.Q<VisualElement>("Rotation").Q<FloatField>("X").value = selectedObject.transform.rotation.x;
        root.Q<VisualElement>("Rotation").Q<FloatField>("Y").value = selectedObject.transform.rotation.y;
        root.Q<VisualElement>("Rotation").Q<FloatField>("Z").value = selectedObject.transform.rotation.z;

        if(!reset){
            lastRotaion.x = selectedObject.transform.rotation.x;
            lastRotaion.y = selectedObject.transform.rotation.y;
            lastRotaion.z = selectedObject.transform.rotation.z;
            lastRotaion.w = selectedObject.transform.rotation.w;
        }
    }

    public void getCurrentScale(GameObject selectedObject, bool reset){

        root.Q<VisualElement>("Scale").Q<FloatField>("X").value = selectedObject.transform.localScale.x;
        root.Q<VisualElement>("Scale").Q<FloatField>("Y").value = selectedObject.transform.localScale.y;
        root.Q<VisualElement>("Scale").Q<FloatField>("Z").value = selectedObject.transform.localScale.z;

        if(!reset){
            lastScale.x = selectedObject.transform.localScale.x;
            lastScale.y = selectedObject.transform.localScale.y;
            lastScale.z = selectedObject.transform.localScale.z;
        }
    }

    public bool checkForChanges(GameObject selectedObject){
        bool changes = false;
        changes = changesInPosition(selectedObject) || changesInRotaion(selectedObject) || changesInScale(selectedObject) || changesInName(selectedObject);
        return changes;
    }

    private bool changesInPosition(GameObject selectedObject){
        Vector3 position = selectedObject.transform.position;

        Vector3 showedPosition;
        showedPosition.x = root.Q<VisualElement>("Position").Q<FloatField>("X").value;
        showedPosition.y = root.Q<VisualElement>("Position").Q<FloatField>("Y").value;
        showedPosition.z = root.Q<VisualElement>("Position").Q<FloatField>("Z").value;

        if (position.Equals(showedPosition))
        {
            //Debug.Log("Changed");
            return false;
        }

        return true;
    }

    private bool changesInRotaion(GameObject selectedObject){
        Quaternion rotation = selectedObject.transform.rotation;

        Quaternion showedRotation;
        showedRotation.x = root.Q<VisualElement>("Rotation").Q<FloatField>("X").value;
        showedRotation.y = root.Q<VisualElement>("Rotation").Q<FloatField>("Y").value;
        showedRotation.z = root.Q<VisualElement>("Rotation").Q<FloatField>("Z").value;
        showedRotation.w = rotation.w;

        if (rotation.Equals(showedRotation))
        {
            //Debug.Log("Changed");
            return false;
        }
        return true;
    }

    private bool changesInScale(GameObject selectedObject){
        Vector3 scale = selectedObject.transform.localScale;

        Vector3 showedScale;
        showedScale.x = root.Q<VisualElement>("Scale").Q<FloatField>("X").value;
        showedScale.y = root.Q<VisualElement>("Scale").Q<FloatField>("Y").value;
        showedScale.z = root.Q<VisualElement>("Scale").Q<FloatField>("Z").value;

        if (scale.Equals(showedScale))
            return false;

        return true;
    }

    private bool changesInName(GameObject selectedObject){
        String name = selectedObject.name;

        String ShowedName;
        TextField NameChanger = root.Q<TextField>("ChangeName");
        ShowedName = NameChanger.value;

        if (name.Equals(ShowedName))
            return false;

        return true;
        
    }

    public Vector3 setCurrentPosition(GameObject selectedObject){

        //Debug.Log("Changed Position");

        Vector3 newPostion;

        newPostion.x = root.Q<VisualElement>("Position").Q<FloatField>("X").value;
        newPostion.y = root.Q<VisualElement>("Position").Q<FloatField>("Y").value;
        newPostion.z = root.Q<VisualElement>("Position").Q<FloatField>("Z").value;

        return newPostion;
    }

    public Quaternion setCurrentRotation(GameObject selectedObject){

        Debug.Log("Changed Rotation");

        Quaternion newRotation;

        newRotation.x = root.Q<VisualElement>("Rotation").Q<FloatField>("X").value;
        newRotation.y = root.Q<VisualElement>("Rotation").Q<FloatField>("Y").value;
        newRotation.z = root.Q<VisualElement>("Rotation").Q<FloatField>("Z").value;
        newRotation.w = selectedObject.transform.rotation.w;

        return newRotation;
    }

    public Vector3 setCurrentScale(GameObject selectedObject){

        Vector3 newScale;

        newScale.x = root.Q<VisualElement>("Scale").Q<FloatField>("X").value;
        newScale.y = root.Q<VisualElement>("Scale").Q<FloatField>("Y").value;
        newScale.z = root.Q<VisualElement>("Scale").Q<FloatField>("Z").value;

        return newScale;
    }

    private String setName(GameObject selectedObject){
        String newName;
        TextField NameChanger = root.Q<TextField>("ChangeName");

        newName = NameChanger.value;
        return newName;
    }

    public void resetPosition(GameObject selectedObject){
        selectedObject.transform.position = lastPosition;
        getCurrentPosition(selectedObject,true);
        //Debug.Log(selectedObject.transform.localPosition + " Reset Vals: " + oldPosition);
    }

    public void resetRotaion(GameObject selectedObject){
        //Debug.Log("Im Being called");
        selectedObject.transform.rotation = lastRotaion;
        getCurrentRotation(selectedObject,true);
    }

    public void resetScale(GameObject selectedObject){
        selectedObject.transform.localScale = lastScale;
        getCurrentScale(selectedObject,true);
        //Debug.Log(selectedObject.transform.localScale + " Reset Vals: " + oldScale);
    }
}
