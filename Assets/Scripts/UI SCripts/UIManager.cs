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
    public UIDocument doc;
    private AncestriesUIDoc ancesteryScript;
    private CharacterBuilderPT2 charBuilderScript;
    private ClassesUIDoc classesScript;

    public void Start(){
        ancesteryScript = GetComponent<AncestriesUIDoc>();
        charBuilderScript = GetComponent<CharacterBuilderPT2>();
        classesScript = GetComponent<ClassesUIDoc>();
        doc = GetComponent<UIDocument>();
        root = doc.rootVisualElement;
        chooseClassButton = root.Q<Label>("UI_ChooseClassButton");
        chooseClassButton.AddManipulator(new Clickable(click => onClassClick()));
    }

    public void onClassClick(){
        assets[0].SetActive(true);
        classesScript.enabled = true;
        charBuilderScript.enabled = false;
        ancesteryScript.enabled = false;
    }

    public void onExit(){
        
    }

}
