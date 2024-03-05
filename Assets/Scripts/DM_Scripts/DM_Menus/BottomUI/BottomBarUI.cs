using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class BottomBarUI : MonoBehaviour
{
    public VisualElement root;
    public getPrefabs prefabs;
    public Camera DMCamera;

    private VisualElement ObjectListHolder;
    private GameObject spawnedObject;
    private String path = "Assets/Resources";
    private List<GameObject> ObjectList;

    void OnEnable(){
        root = GetComponent<UIDocument>().rootVisualElement;
        ObjectListHolder = root.Q<VisualElement>("ObjectHolder");

        ObjectList = Resources.LoadAll<GameObject>("Prefabs").ToList();
        foreach (var item in ObjectList){
            Debug.Log(item.name);
        }


        fillingBottomUI(ObjectList);
    }

    void Update(){
    }

    private void mouseObjectPlacing(GameObject item){
        spawnedObject = Resources.Load<GameObject>(path + item.name);
    }

    private void fillingBottomUI(List<GameObject> objectList){
        foreach(GameObject item in objectList){
            //Debug.Log(item);
            VisualElement newObject = new VisualElement
            {
                name = item.name
            };

            newObject.AddManipulator(new Clickable(click => mouseObjectPlacing(item)));
            newObject.Add(makeObjectLabel(item));
            ObjectListHolder.Add(newObject);
        }
    }
    private Label makeObjectLabel(GameObject item){
        Label ObjectLabel = new Label
        {
            text = item.name //.Substring(0, item.Name.Length - 7)
        };

        return ObjectLabel;
    }
}
