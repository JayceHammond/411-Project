using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class BottomBarUI : MonoBehaviour
{
    public VisualElement root;
    public Camera DMCamera;

    private VisualElement ObjectListHolder;
    private VisualElement ToggleHolder;
    private StyleSheet ObjectLabelStyle;
    private StyleSheet ObjectVisEleStyle;
    private StyleSheet ToggleBoxCS;

    private List<GameObject> ObjectList;
    private List<String> ToggleNames;
    public List<String> SelectedTags; 

    private GameObject spawnedObject;
    private String path = "Prefabs";
    private String LastToggled;
    private int OldSelectedLenght = 0;


    void OnEnable(){
        root = GetComponent<UIDocument>().rootVisualElement;
        ObjectListHolder = root.Q<VisualElement>("ObjectHolder");

        //Getting the VisualElement that holds the Toggles
        ToggleHolder = root.Q<VisualElement>("BuildMenu").Q<VisualElement>("ToggleBox");
        ToggleNames = new List<string>();

        ObjectList = Resources.LoadAll<GameObject>(path).ToList();
        ObjectLabelStyle = Resources.Load<StyleSheet>("CSS/ObjectLabelStyles");
        ObjectVisEleStyle = Resources.Load<StyleSheet>("CSS/ObjectVisualElement");
        ToggleBoxCS = Resources.Load<StyleSheet>("CSS/ToggleBoxes");

        populateTags();
    }

    void Update(){
        checkToggles();
        if(SelectedTags.Count != OldSelectedLenght){
            displayObjects(sortList(SelectedTags));
        }
    }

    private void mouseObjectPlacing(GameObject item){
        Vector3 IntialPosition = new Vector3 {
            x = 0,
            y = 5,
            z = 0
        };

        Quaternion IntialRotation = new Quaternion {
            x = 0,
            y = 0,
            z = 0,
            w = 1
        };

        spawnedObject = Resources.Load<GameObject>(path + "/" + item.name);
        //Debug.Log(spawnedObject);

        spawnedObject.transform.position = IntialPosition;
        spawnedObject.transform.rotation = IntialRotation;


        Instantiate(spawnedObject);
    }
    private Label makeObjectLabel(GameObject item){
        Label ObjectLabel = new Label
        {
            text = item.name //.Substring(0, item.Name.Length - 7)
        };

        ObjectLabel.styleSheets.Add(ObjectLabelStyle);

        return ObjectLabel;
    }
    private VisualElement makeObjectImage(GameObject item){
        VisualElement ObjectImage = new VisualElement
        {
            name = item.name + "Image"
        };

        ObjectImage.style.backgroundImage = new StyleBackground(item.GetComponent<SpriteRenderer>().sprite);
        ObjectImage.style.width = 100;
        ObjectImage.style.height = 150;

        return ObjectImage;
    }
    private void makeToggle(String tag){
        Toggle NewToggle = new Toggle
        {
            name = tag,
            label = tag,
            value = true,

        };

        NewToggle.styleSheets.Add(ToggleBoxCS);
        ToggleHolder.Add(NewToggle);
    }

    private void checkToggles(){
         OldSelectedLenght = SelectedTags.Count;
        foreach(String toggle in ToggleNames){
            if(ToggleHolder.Q<Toggle>(toggle).value){
                if (!SelectedTags.Contains(toggle)){
                    SelectedTags.Add(toggle);
                    //changed = true;
                }
            }else{
                SelectedTags.Remove(toggle);
                //changed = false;
            }
        }
    }
    private void populateTags(){
        foreach (GameObject item in ObjectList){
            if(!ToggleNames.Contains(item.tag)){
                ToggleNames.Add(item.tag);
                makeToggle(item.tag);
            }
        }
    }

    private void displayObjects(List<GameObject> objectList){
        //Debug.Log("Count");
        ObjectListHolder.Clear();
        foreach(GameObject item in objectList){
            //Debug.Log(item);
            VisualElement newObject = new VisualElement
            {
                name = item.name
            };

            newObject.Add(makeObjectLabel(item));
            newObject.Add(makeObjectImage(item));

            Debug.Log(newObject);
            newObject.AddManipulator(new Clickable(click => mouseObjectPlacing(item)));
            newObject.styleSheets.Add(ObjectVisEleStyle);

            ObjectListHolder.Add(newObject);
        }
    }

    private List<GameObject> sortList(List<String> SelectedTags){
        //ObjectListHolder.Clear();
        List<GameObject> SelectedObjects = new List<GameObject>();
        foreach(String Tag in SelectedTags){
            foreach(GameObject item in ObjectList){
                if(item.tag == Tag){
                    //Debug.Log(item);
                    SelectedObjects.Add(item);
                }
            }
        }

        return SelectedObjects;
    }
    
}
