using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChangesNTransform : MonoBehaviour
{
    public VisualElement root;
    private SidebarUI sidebarUI;
    void Start(){
        root = sidebarUI.root;
    }

   public bool checkForChanges(GameObject selectedObject){
        bool changes = false;
        changes = changesInPosition(selectedObject) || changesInRotaion(selectedObject) || changesInScale(selectedObject);
        return changes;
    }

    private bool changesInPosition(GameObject selectedObject){
        UnityEngine.Vector3 position = selectedObject.transform.position;

        UnityEngine.Vector3 showedPosition;
        showedPosition.x = root.Q<VisualElement>("Position").Q<FloatField>("X").value;
        showedPosition.y = root.Q<VisualElement>("Position").Q<FloatField>("Y").value;
        showedPosition.z = root.Q<VisualElement>("Position").Q<FloatField>("Z").value;

        if (position.Equals(showedPosition))
            return true;

        return false;
    }

    private bool changesInRotaion(GameObject selectedObject){
        UnityEngine.Quaternion rotation = selectedObject.transform.rotation;

        UnityEngine.Quaternion showedRotation;
        showedRotation.x = root.Q<VisualElement>("Rotation").Q<FloatField>("X").value;
        showedRotation.y = root.Q<VisualElement>("Rotation").Q<FloatField>("Y").value;
        showedRotation.z = root.Q<VisualElement>("Rotation").Q<FloatField>("Z").value;
        showedRotation.w = rotation.w;

        if (rotation.Equals(showedRotation))
            return true;

        return false;
    }

    private bool changesInScale(GameObject selectedObject){
        UnityEngine.Vector3 scale = selectedObject.transform.localScale;

        UnityEngine.Vector3 showedScale;
        showedScale.x = root.Q<VisualElement>("Scale").Q<FloatField>("X").value;
        showedScale.y = root.Q<VisualElement>("Scale").Q<FloatField>("Y").value;
        showedScale.z = root.Q<VisualElement>("Scale").Q<FloatField>("Z").value;

        if (scale.Equals(showedScale))
            return true;

        return false;
    }
}
