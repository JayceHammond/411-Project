using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildMenuUI : MonoBehaviour
{

    private VisualElement root;
    private SelectItem selectedItem;

    void Start(){
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        //Debug.Log(root.Q<Label>("Object_Selected").text);
    }

    void Update(){
        Debug.Log("Try here: " + selectedItem.getSelectedName());
    }

    private void changeSelectLable(){
        GameObject selectedObject;

        Debug.Log("Game Object: " + selectedItem.objectSelection().GetInstanceID());

        selectedObject = selectedItem.objectSelection();

        if ("emptyObject" != selectedObject.name)
        {
            root.Q<Label>("Object_Selected").text = selectedObject.name;
            Debug.Log(root.Q<Label>("Object_Selected").text + "The Name!!");
        }
        else
        {
            root.Q<Label>("Object_Selected").text.Equals("No Object Selected");
        }
    }
}
