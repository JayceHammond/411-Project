using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterBuilderPT2 : MonoBehaviour
{

    public VisualElement root;
    public JSONDataLoader dataLoader;

    //Setting up UI elements that are going to be used
    private VisualElement AncestriesButton;
    private VisualElement FeatList;
    private VisualElement AddFeatButton;

    // Start is called before the first frame update
    void Start(){
        root = GetComponent<UIDocument>().rootVisualElement;

        //Getting the UI elements
        AncestriesButton = root.Q<VisualElement>("Main").Q<VisualElement>("Builder-Menu").Q<VisualElement>("Left-Side").Q<VisualElement>("Ancestry-Choser").Q<VisualElement>("Ancestries-Button");
        FeatList = root.Q<VisualElement>("Main").Q<VisualElement>("Builder-Menu").Q<VisualElement>("Right-Side").Q<VisualElement>("Feats-Choser").Q<ScrollView>("Feats-Viewer");
        AddFeatButton = root.Q<VisualElement>("Main").Q<VisualElement>("Builder-Menu").Q<VisualElement>("Right-Side").Q<VisualElement>("Feats-Choser").Q<VisualElement>("Feat-Button-Conatiner").Q<VisualElement>("Add-Feats-Button");
        
    }

    // Update is called once per frame
    void Update(){
        
    }
}
