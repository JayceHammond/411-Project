using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;



public class UIManager : MonoBehaviour
{
    public VisualElement root;
    public List<GameObject> assets;
    private VisualElement chooseClassButton;
    private VisualElement chooseAncestryButton;
    private VisualElement ClosePopup;
    public UIDocument doc;
    private AncestriesUIDoc ancesteryScript;
    private CharacterBuilderPT2 charBuilderScript;
    private ClassesUIDoc classesScript;
    private GameObject instantiatedUI;
    public GameObject escMenu;

    public void Start(){
        ancesteryScript = GetComponent<AncestriesUIDoc>(); //Redo like line 32 so it can update the right UI Doc
        charBuilderScript = GetComponent<CharacterBuilderPT2>();
       
        doc = GetComponent<UIDocument>();
        root = doc.rootVisualElement;
        chooseClassButton = root.Q<VisualElement>("Class-Button");
        chooseAncestryButton = root.Q<VisualElement>("UI_ChooseAncestryButton");
        chooseClassButton.AddManipulator(new Clickable(click => onClassClick()));
        chooseAncestryButton.AddManipulator(new Clickable(click => onAncestryClick()));
    }

    public void LateUpdate(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            escMenu.SetActive(true);
        }


        if(instantiatedUI == null){ return; }
        if(instantiatedUI.GetComponent<ClassesUIDoc>().populate == true){
            instantiatedUI.GetComponent<ClassesUIDoc>().populateClasses(instantiatedUI.GetComponent<ClassesUIDoc>().populate);
            instantiatedUI.GetComponent<ClassesUIDoc>().populate = false;
        }

        if(instantiatedUI.GetComponent<AncestriesUIDoc>().populate == true){
            instantiatedUI.GetComponent<AncestriesUIDoc>().populateAncestries(instantiatedUI.GetComponent<AncestriesUIDoc>().populate);
            instantiatedUI.GetComponent<AncestriesUIDoc>().populate = false;
        }
    }

    public void onClassClick(){
        instantiatedUI = Instantiate(assets[0]);
        instantiatedUI.GetComponent<ClassesUIDoc>().dataLoader = GameObject.Find("DataLoader").GetComponent<JSONDataLoader>();

        root = instantiatedUI.GetComponent<UIDocument>().rootVisualElement;
        instantiatedUI.GetComponent<ClassesUIDoc>().populate = true;
        

        ClosePopup = root.Q<VisualElement>("Main").Q<VisualElement>("ClassMenu").Q<VisualElement>("ClassSummry").Q<VisualElement>("ClassNameANDClose").Q<VisualElement>("ExitElement").Q<VisualElement>("Icon");
        ClosePopup.AddManipulator(new Clickable(click => onExit()));
    }

    public void onAncestryClick(){
        instantiatedUI = Instantiate(assets[1]);
        instantiatedUI.GetComponent<AncestriesUIDoc>().dataLoader = GameObject.Find("DataLoader").GetComponent<JSONDataLoader>();

        root = instantiatedUI.GetComponent<UIDocument>().rootVisualElement;
        instantiatedUI.GetComponent<AncestriesUIDoc>().populate = true;
        

        ClosePopup = root.Q<VisualElement>("ExitElement").Q<VisualElement>("Icon");
        ClosePopup.AddManipulator(new Clickable(click => onExit()));
    }

    

    public void onExit(){
        Debug.Log("Exit");
        Destroy(instantiatedUI);
    }

    public void openNethys(){
        Application.OpenURL("https://2e.aonprd.com/");
    }

    public void closeGame(){
        Application.Quit();
    }

    public void onMainMenuPress(){
        SceneManager.LoadScene("MainMenu");
    }

    public void onContinuePress(){
        escMenu.SetActive(false);
    }

}
