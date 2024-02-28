using System;
using System.IO;
using JetBrains.Annotations;
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
        //Debug.Log(ObjectList.Length);
        
    }

    private void gettingGameObjects(Array objectList){
        foreach(FileInfo item in ObjectList){
            VisualElement newObject = new VisualElement();
            newObject.name = item.Name;

            newObject.Add(makeObjectLabel(item));
            //Might need to make a tempary screen to get the pictures or make photos and store them then grab them
            //newObject.Add(makeObjectImage(item));
            newObject.AddManipulator(new Clickable(evt => Debug.Log("Clicked " + item.Name)));
            newObject.focusable = true;
            ObjectListHolder.Add(newObject);
        }
    }

    private Label makeObjectLabel(FileInfo item){
        Label ObjectLabel = new Label
        {
            text = item.Name.Substring(0, item.Name.Length - 7)
        };

        return ObjectLabel;
    }

    private Image makeObjectImage(FileInfo item){
        Image ObjectImage = new Image();
        GameObject objectForImage = GameObject.Find(item.Name);

        return ObjectImage;
    }

    private GameObject spawnSelectedObject(FileInfo itemFile){
        //Player needs to select the object from the UI then based on which object the user chooses
        //then spawn that item and place the object where they release the mouse button

        return null;
    }
}
