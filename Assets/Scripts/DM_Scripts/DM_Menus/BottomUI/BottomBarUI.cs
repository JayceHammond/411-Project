using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BottomBarUI : MonoBehaviour
{
    public VisualElement root;
    public Camera DMCamera;

    private VisualElement ObjectListHolder;
    private GameObject spawnedObject;
    private String path = "Prefabs";
    private StyleSheet ObjectLabelStyle;
    private StyleSheet ObjectVisEleStyle;
    private List<GameObject> ObjectList;

    void OnEnable(){
        root = GetComponent<UIDocument>().rootVisualElement;
        ObjectListHolder = root.Q<VisualElement>("ObjectHolder");

        ObjectList = Resources.LoadAll<GameObject>(path).ToList();
        ObjectLabelStyle = Resources.Load<StyleSheet>("CSS/ObjectLabelStyles");
        ObjectVisEleStyle = Resources.Load<StyleSheet>("CSS/ObjectVisualElement");

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

            newObject.Add(makeObjectLabel(item));
            newObject.Add(makeObjectImage(item));

            newObject.AddManipulator(new Clickable(click => mouseObjectPlacing(item)));
            newObject.styleSheets.Add(ObjectVisEleStyle);

            ObjectListHolder.Add(newObject);
        }
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
}
