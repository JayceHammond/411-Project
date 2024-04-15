using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ClassesUIDoc : MonoBehaviour
{
    public VisualElement root;
    public JSONDataLoader dataLoader;
    
    private VisualElement Classes;
    private VisualElement Charaistics;
    private VisualElement Abilities;
    private VisualElement Proficiencies;
    private VisualElement Saves;
    private VisualElement Attack_Defense;
    private VisualElement Resistance_Weakness;
    private VisualElement Skills;
    private VisualElement Health;
    private VisualElement CloseClasses;
    private Label SelectedClassName;
    private StyleSheet AncestriesButtons;

    private bool populate = true; 

    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        Classes = root.Q<VisualElement>("Main").Q<VisualElement>("ClassMenu").Q<VisualElement>("Classes").Q<VisualElement>("Classes-Holder");
        SelectedClassName = root.Q<VisualElement>("Main").Q<VisualElement>("ClassMenu").Q<VisualElement>("ClassSummry").Q<VisualElement>("ClassNameANDClose").Q<Label>("ClassName");
        CloseClasses = root.Q<VisualElement>("Main").Q<VisualElement>("ClassMenu").Q<VisualElement>("ClassSummry").Q<VisualElement>("ClassNameANDClose").Q<VisualElement>("ExitElement").Q<VisualElement>("Icon");

        Charaistics = root.Q<VisualElement>("Main").Q<VisualElement>("ClassMenu").Q<VisualElement>("ClassSummry").Q<VisualElement>("Chartistics-of-Class");
        Abilities = Charaistics.Q<VisualElement>("Abilities");
        Proficiencies = Charaistics.Q<VisualElement>("Proficiencies-Holder");

        Saves = Proficiencies.Q<VisualElement>("Saves");
        Attack_Defense = Proficiencies.Q<VisualElement>("Attack-Defense");
        Resistance_Weakness = Proficiencies.Q<VisualElement>("Resistance-Weakness");
        Skills = Proficiencies.Q<VisualElement>("Skill");
        Health = Proficiencies.Q<VisualElement>("Health");


        //Asigning the CSS to use 
        AncestriesButtons = Resources.Load<StyleSheet>("CSS/AnceseryButtons");

        //Adding Close Feature to Class Popup
        CloseClasses.AddManipulator(new Clickable(click => closeClassPopup()));
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
            //Grabs all the Classes from the JSON file
            for (int i = 0; i < dataLoader.classList.Count; i++)
            {
                //Debug.Log(dataLoader.classList[i].name);
                Label NewClass = new Label
                {
                    name = dataLoader.classList[i].name,
                    text = dataLoader.classList[i].name
                };

                if(AncestriesButtons != null)
                    NewClass.styleSheets.Add(AncestriesButtons);
                else
                    NewClass.styleSheets.Add(Resources.Load<StyleSheet>("CSS/AnceseryButtons"));

                //Lets the element be clickable and calls the function
                NewClass.AddManipulator(new Clickable(click => populateClass(NewClass.name)));

                if(Classes != null)
                    Classes.Add(NewClass);
                
            }
        }
        populate = false;
    }

    private void populateClass(string className){
        changeSelectedName(className); //Changes the Label at the top
        populateSummery(className); //Changes the summery for the selected class
        populateCharaistics(className); //Changes the charaistics for the selected class
    }

    private void changeSelectedName(String className){
        SelectedClassName.text = dataLoader.classList.Find(x => x.name == className).name;
    }

    private void populateSummery(string className){
        //Populates Class Summery, whenever it gets added to the JSON
    }

    //Populates the attack, defense, skill proficiency, health, and saves
    private void populateCharaistics(string className){
        //Get the class aspects from the JSON using the name
        String Health = dataLoader.classList.Find(x => x.name == className).hp;
        Proficiencies.Q<VisualElement>("Health").Q<Label>("Amount").text = Health;


        String Fortitude = dataLoader.classList.Find(x => x.name == className).fortitude_proficiency;
        String Reflex = dataLoader.classList.Find(x => x.name == className).reflex_proficiency;
        String Will = dataLoader.classList.Find(x => x.name == className).will_proficiency;
        String Perception = dataLoader.classList.Find(x => x.name == className).perception_proficiency;

        Saves.Q<VisualElement>("Fortitude").Q<Label>("Proficiency").text = Fortitude;
        Saves.Q<VisualElement>("Reflex").Q<Label>("Proficiency").text = Reflex;
        Saves.Q<VisualElement>("Will").Q<Label>("Proficiency").text = Will;
        Saves.Q<VisualElement>("Perception").Q<Label>("Proficiency").text = Perception;

        String[] Abilities = dataLoader.classList.Find(x => x.name == className).ability;
        //Might have to go through all of the dot refences for each class and set it from there
        gettingAttackProficiency(className);
        gettingDefenseProficiency(className);

    }

    private void gettingAttackProficiency(String ClassName){
        VisualElement Attack = Attack_Defense.Q<VisualElement>("Attack");

        if(dataLoader.classList.Find(x => x.name == ClassName).attack_proficiency.advanced_weapons == null){
            Attack.Q<VisualElement>("Advanced").Q<Label>("Proficiency").text = "Untrained";
        }else{
            Attack.Q<VisualElement>("Advanced").Q<Label>("Proficiency").text = dataLoader.classList.Find(x => x.name == ClassName).attack_proficiency.advanced_weapons;
        }

        if(dataLoader.classList.Find(x => x.name == ClassName).attack_proficiency.simple_weapons == null){
            Attack.Q<VisualElement>("Simple").Q<Label>("Proficiency").text = "Untrained";
        }else{
            Attack.Q<VisualElement>("Simple").Q<Label>("Proficiency").text = dataLoader.classList.Find(x => x.name == ClassName).attack_proficiency.simple_weapons;
        }

        if(dataLoader.classList.Find(x => x.name == ClassName).attack_proficiency.martial_weapons == null){
            Attack.Q<VisualElement>("Martial").Q<Label>("Proficiency").text = "Untrained";
        }else{
            Attack.Q<VisualElement>("Martial").Q<Label>("Proficiency").text = dataLoader.classList.Find(x => x.name == ClassName).attack_proficiency.martial_weapons;
        }

        if(dataLoader.classList.Find(x => x.name == ClassName).attack_proficiency.unarmed_attacks == null){
            Attack.Q<VisualElement>("Unarmed").Q<Label>("Proficiency").text = "Untrained";
        }else{
            Attack.Q<VisualElement>("Unarmed").Q<Label>("Proficiency").text = dataLoader.classList.Find(x => x.name == ClassName).attack_proficiency.unarmed_attacks;
        }
    }

    private void gettingDefenseProficiency(String ClassName){
        VisualElement Defense = Attack_Defense.Q<VisualElement>("Defence");

        if(dataLoader.classList.Find(x => x.name == ClassName).defense_proficiency.light_armor == null){
            Defense.Q<VisualElement>("Light-Armor").Q<Label>("Proficiency").text = "Untrained";
        }else{
            Defense.Q<VisualElement>("Light-Armor").Q<Label>("Proficiency").text = dataLoader.classList.Find(x => x.name == ClassName).defense_proficiency.light_armor;
        }

        if(dataLoader.classList.Find(x => x.name == ClassName).defense_proficiency.medium_armor == null){
            Defense.Q<VisualElement>("Medium-Armor").Q<Label>("Proficiency").text = "Untrained";
        }else{
            Defense.Q<VisualElement>("Medium-Armor").Q<Label>("Proficiency").text = dataLoader.classList.Find(x => x.name == ClassName).defense_proficiency.medium_armor;
        }

        if(dataLoader.classList.Find(x => x.name == ClassName).defense_proficiency.heavy_armor == null){
            Defense.Q<VisualElement>("Heavy-Armor").Q<Label>("Proficiency").text = "Untrained";
        }else{
            Defense.Q<VisualElement>("Heavy-Armor").Q<Label>("Proficiency").text = dataLoader.classList.Find(x => x.name == ClassName).defense_proficiency.heavy_armor;
        }

        if(dataLoader.classList.Find(x => x.name == ClassName).defense_proficiency.unarmored_defense == null){
            Defense.Q<VisualElement>("Unarmored-Defense").Q<Label>("Proficiency").text = "Untrained";
        }else{
            Defense.Q<VisualElement>("Unarmored-Defense").Q<Label>("Proficiency").text = dataLoader.classList.Find(x => x.name == ClassName).defense_proficiency.unarmored_defense;
        }

    }

    private void closeClassPopup(){
        
    }
}