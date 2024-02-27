using System;
using System.IO;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class BottomBarUI : MonoBehaviour
{
    public VisualElement root;
    public getPrefabs prefabs;
    private VisualElement ObjectListHolder;
    private Array ObjectList;

    void OnEnable(){
        root = GetComponent<UIDocument>().rootVisualElement;
        ObjectListHolder = root.Q<VisualElement>("ObjectHolder");

        ObjectList = prefabs.getAllPrefabs();
        gettingGameObjects(ObjectList);
    }

    void Update(){
        Debug.Log(ObjectList.Length);
    }

    void gettingGameObjects(Array objectList){
        foreach(FileInfo item in ObjectList){
            ObjectField newObject = new ObjectField();
            newObject.name = item.Name;
            newObject.label = item.Name;

            ObjectListHolder.Add(newObject);
        }
    }

}
