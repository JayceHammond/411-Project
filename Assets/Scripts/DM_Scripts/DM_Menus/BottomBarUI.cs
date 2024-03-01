using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class BottomBarUI : MonoBehaviour
{
    public VisualElement root;
    public getPrefabs prefabs;
    public Camera DMCamera;
    public GameObject objectToSpawn;

    private VisualElement ObjectListHolder;
    private Array ObjectList;
    private GameObject spawnedObject;
    private bool holding = false;

    void OnEnable(){
        root = GetComponent<UIDocument>().rootVisualElement;
        ObjectListHolder = root.Q<VisualElement>("ObjectHolder");

        ObjectList = prefabs.getAllPrefabs();
        fillingBottomUI(ObjectList);
    }

    void Update(){
        if(spawnedObject != null){
            spawnedObject.transform.localPosition = DMCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, 10f));
        }else{
            holding = mouseObjectPlacing(holding);
        }
    }


    private bool mouseObjectPlacing(bool holding){
        if (Input.GetMouseButton(0) && holding){
            Debug.Log("Placing");
            Vector3 mousePosition = DMCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, 50f));

            spawnedObject = Instantiate(objectToSpawn, mousePosition, Quaternion.identity);

            spawnedObject = null;
            return !holding;
        }else if(Input.GetMouseButton(1) && holding){
            Debug.Log("Canceled");

            spawnedObject = null;
            return !holding;
        }else{
             return holding;
        }
    }

    private void fillingBottomUI(Array objectList){
        foreach(FileInfo item in objectList){
            VisualElement newObject = new VisualElement
            {
                name = item.Name
            };

            newObject.AddManipulator(new Clickable(click => holding = true));
            newObject.Add(makeObjectLabel(item));
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
}
