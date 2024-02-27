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

    private void gettingGameObjects(Array objectList){
        foreach(FileInfo item in ObjectList){
            VisualElement newObject = new VisualElement();
            newObject.name = item.Name;

            newObject.Add(makeObjectLabel(item));
            newObject.Add(makeObjectImage(item));

            ObjectListHolder.Add(newObject);
        }
    }

    private Label makeObjectLabel(FileInfo item){
        Label ObjectLabel = new Label();
        ObjectLabel.text = item.Name.Substring(0,item.Name.Length-7);

        return ObjectLabel;
    }

    private Image makeObjectImage(FileInfo item){
        Image ObjectImage = new Image();
        GameObject objectForImage = GameObject.Find(item.Name);

        return ObjectImage;
    }

}
