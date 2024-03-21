using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ClassesUIDoc : MonoBehaviour
{
    public VisualElement root;
    public JSONDataLoader dataLoader;
    
    private VisualElement Classes;
    private StyleSheet AncestriesButtons;

    private bool populate = true; 

    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        Classes = root.Q<VisualElement>("Main").Q<VisualElement>("ClassMenu").Q<VisualElement>("Classes").Q<VisualElement>("Classes-Holder");

        //Asigning the CSS to use 
        AncestriesButtons = Resources.Load<StyleSheet>("CSS/AnceseryButtons");
    }

    // Update is called once per frame
    void LateUpdate(){
        if(populate){
            populateClasses();
        }
    }

     private void populateClasses(){
        if (populate) //Makesure this runs once
        {
            //Grabs all the Ancestries from the JSON file
            for (int i = 0; i < dataLoader.classList.Count; i++)
            {
                //Debug.Log(dataLoader.classList[i].name);
                Label NewClass = new Label
                {
                    name = dataLoader.classList[i].name,
                    text = dataLoader.classList[i].name
                };
                NewClass.styleSheets.Add(AncestriesButtons);
                //Lets the element be clickable and calls the function
                NewClass.AddManipulator(new Clickable(click => populateClass(NewClass.name)));
                Classes.Add(NewClass);
                
            }
        }
        populate = false;
    }

    private void populateClass(string className){

    }

}
