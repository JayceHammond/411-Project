using UnityEngine;
using UnityEngine.UIElements;

public class GetTransforms : MonoBehaviour
{
    public VisualElement root;
    public SidebarUI sidebarUI;
    void Start(){
        root = sidebarUI.root;
    }

   public void getCurrentPosition(GameObject selectedObject){
        if(null != selectedObject){
            root.Q<VisualElement>("Position").Q<FloatField>("X").value = selectedObject.transform.position.x;
            sidebarUI.intialTransformValues["posX"] = selectedObject.transform.position.x;
            root.Q<VisualElement>("Position").Q<FloatField>("Y").value = selectedObject.transform.position.y;
            sidebarUI.intialTransformValues["posY"] = selectedObject.transform.position.y;
            root.Q<VisualElement>("Position").Q<FloatField>("Z").value = selectedObject.transform.position.z;
            sidebarUI.intialTransformValues["posZ"] = selectedObject.transform.position.z;
        }
    }

    public void getCurrentRotation(GameObject selectedObject){
        if(null != selectedObject){
            root.Q<VisualElement>("Rotation").Q<FloatField>("X").value = selectedObject.transform.rotation.x;
            sidebarUI.intialTransformValues["rotX"] = selectedObject.transform.rotation.x;
            root.Q<VisualElement>("Rotation").Q<FloatField>("Y").value = selectedObject.transform.rotation.y;
            sidebarUI.intialTransformValues["rotY"] = selectedObject.transform.rotation.y;
            root.Q<VisualElement>("Rotation").Q<FloatField>("Z").value = selectedObject.transform.rotation.z;
            sidebarUI.intialTransformValues["rotZ"] = selectedObject.transform.rotation.z;
        }
    }

    public void getCurrentScale(GameObject selectedObject){
        if(null != selectedObject){
            root.Q<VisualElement>("Scale").Q<FloatField>("X").value = selectedObject.transform.localScale.x;
            sidebarUI.intialTransformValues["scalX"] = selectedObject.transform.localScale.x;
            root.Q<VisualElement>("Scale").Q<FloatField>("Y").value = selectedObject.transform.localScale.y;
            sidebarUI.intialTransformValues["scalY"] = selectedObject.transform.localScale.y;
            root.Q<VisualElement>("Scale").Q<FloatField>("Z").value = selectedObject.transform.localScale.z;
            sidebarUI.intialTransformValues["scalZ"] = selectedObject.transform.localScale.z;
        }
    }
}
