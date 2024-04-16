using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.UIElements;



public class UIManager : MonoBehaviour
{
    public VisualElement root;
    public List<GameObject> assets;
    private VisualElement chooseClassButton;
    private VisualElement ClosePopup;
    public UIDocument doc;
    private AncestriesUIDoc ancesteryScript;
    private CharacterBuilderPT2 charBuilderScript;
    private ClassesUIDoc classesScript;

    public void Start(){
        ancesteryScript = GetComponent<AncestriesUIDoc>(); //Redo like line 32 so it can update the right UI Doc
        charBuilderScript = GetComponent<CharacterBuilderPT2>();
       
        doc = GetComponent<UIDocument>();
        root = doc.rootVisualElement;
        chooseClassButton = root.Q<VisualElement>("Class-Button");
        chooseClassButton.AddManipulator(new Clickable(click => onClassClick()));
    }

    public void onClassClick(){
        assets[0].SetActive(true);
        classesScript = assets[0].GetComponent<ClassesUIDoc>();
        classesScript.enabled = true;
        charBuilderScript.enabled = false;
        //ancesteryScript.enabled = false;

        root = assets[0].GetComponent<UIDocument>().rootVisualElement;
        classesScript.populate = true;
        ClosePopup = root.Q<VisualElement>("Main").Q<VisualElement>("ClassMenu").Q<VisualElement>("ClassSummry").Q<VisualElement>("ClassNameANDClose").Q<VisualElement>("ExitElement").Q<VisualElement>("Icon");
        ClosePopup.AddManipulator(new Clickable(click => onExit()));
    }

    

    public void onExit(){
        classesScript.populate = false;
        assets[0].SetActive(false);
        assets[0].SetActive(false);
    }

}
