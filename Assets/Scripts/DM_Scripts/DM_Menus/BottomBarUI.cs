using System;
using System.IO;
using System.Linq;
using UnityEditor;
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
    }

    void Update(){
        Debug.Log(ObjectList.Length);
    }

}
