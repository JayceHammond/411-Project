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
        changeSelectLable();
    }

    private void changeSelectLable(){
        GameObject selectedObject;
        selectedObject = selectedItem.objectSelection();

        Debug.Log(selectedObject);

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
