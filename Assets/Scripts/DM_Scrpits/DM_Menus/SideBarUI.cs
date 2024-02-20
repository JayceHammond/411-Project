using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildMenuUI : MonoBehaviour
{

    private VisualElement root;
    private SelectObject selectedObject;

    void Start(){
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        selectedObject = SelectObject.Instance;
        //Debug.Log(root.Q<Label>("Object_Selected").text);
    }

    void Update(){
        Debug.Log("Try here: " + selectedObject.objectSelection());
    }

    private void changeSelectLable(){
        GameObject objectGiven;

        objectGiven = selectedObject.objectSelection();

        if ("emptyObject" != objectGiven.name)
        {
            root.Q<Label>("Object_Selected").text = objectGiven.name;
            Debug.Log(root.Q<Label>("Object_Selected").text + "The Name!!");
        }
        else
        {
            root.Q<Label>("Object_Selected").text.Equals("No Object Selected");
        }
    }
}
