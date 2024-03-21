using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AncestriesUIDoc : MonoBehaviour
{
    public VisualElement root;
    public JSONDataLoader dataLoader;

    //Setting up UI elements that are going to be used
    private VisualElement Ancestries;
    private VisualElement AncestrySummaries;
    private VisualElement AncestryStatIncrease;
    private VisualElement PlayerStatIncrease;

    //Setting up the CSS the UI Elements being made will follow
    private StyleSheet AncestriesButtons;
    private StyleSheet AncestriesAbility;

    //Varibles and List used in the script
    private List<string> TraitChoices = new List<string> { "Select Trait", "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma" };
    private bool populate = true;

    // Start is called before the first frame update
    void Start(){
        //Assigning the UI elements to use
        root = GetComponent<UIDocument>().rootVisualElement;
        Ancestries = root.Q<VisualElement>("Ancestries").Q<VisualElement>("Ancestries-Holder");
        AncestrySummaries = root.Q<VisualElement>("AncestrySummry").Q<VisualElement>("AncestrySummry");
        AncestryStatIncrease = root.Q<VisualElement>("AncestrySummry").Q<VisualElement>("Stat-Increase").Q<VisualElement>("Preset-Stat");
        PlayerStatIncrease = root.Q<VisualElement>("AncestrySummry").Q<VisualElement>("Stat-Increase").Q<VisualElement>("Pickable-Stat");
        //Assigning the CSS to use
        AncestriesButtons = Resources.Load<StyleSheet>("CSS/AnceseryButtons");
        AncestriesAbility = Resources.Load<StyleSheet>("CSS/AnceseryAbilities");

    }

    void LateUpdate(){
       //Calls Once when the ancestry meue pops up. Fills in the Ancesteries
        populateAncestries();

    }

    private void populateAncestries(){
        if (populate) //Makesure this runs once
        {
            //Grabs all the Ancestries from the JSON file
            for (int i = 0; i < dataLoader.ancestryList.Count; i++)
            {
                if (dataLoader.ancestryList[i].type == "Ancestry")
                {
                    Label NewAncestry = new Label
                    {
                        name = dataLoader.ancestryList[i].name,
                        text = dataLoader.ancestryList[i].name
                    };
                    NewAncestry.styleSheets.Add(AncestriesButtons);
                    //Lets the element be clickable and calls the function
                    NewAncestry.AddManipulator(new Clickable(click => populateAncestry(NewAncestry.name)));
                    Ancestries.Add(NewAncestry);
                }
            }
        }
        populate = false;
    }
    
    //The fuction that gets called when someone clicks on an ancestry
    private void populateAncestry(String RaceName){
        updateNameLabel(RaceName);
        populateAncestSumry(RaceName);
        resetTraitList();
        populateStatIncrease(RaceName);
    }

    private void updateNameLabel(String RaceName){
        root.Q<VisualElement>("AncestrySummry").Q<Label>("Ancestry_Name").text = RaceName;
    }

    private void populateAncestSumry(String RaceName){
        //Grabs the Summery UI element and changes the text to the string gotten by the JSON
        AncestrySummaries.Q<Label>("Summery").text = dataLoader.ancestryList.Find(x => x.name == RaceName).summary;
    }
    private void populateStatIncrease(String RaceName){
        //Grabs the ability that the ancestry increasses
        String AbilityOne = dataLoader.ancestryList.Find(x => x.name == RaceName).ability[0];
        String AbilityTwo = dataLoader.ancestryList.Find(x => x.name == RaceName).ability[1];

        //If the ablity is not free then check to see if there a labal already if not check for a dropdown 
        if (AbilityOne != "Free"){
            if(AncestryStatIncrease.Q<Label>("Trait-Increase-one") == null){
                if (AncestryStatIncrease.Q<DropdownField>("Trait-Increase-one") != null)
                {
                    AncestryStatIncrease.Remove(AncestryStatIncrease.Q<DropdownField>("Trait-Increase-one"));
                    AncestryStatIncrease.Add(makeTraitLabel("Trait-Increase-one"));
                }
                else
                {
                    AncestryStatIncrease.Add(makeTraitLabel("Trait-Increase-one"));
                }
            }

            AncestryStatIncrease.Q<Label>("Trait-Increase-one").text = AbilityOne;
            removeItemFromTraitList(AbilityOne);

        }
        else
        {

            if (AncestryStatIncrease.Q<Label>("Trait-Increase-one") != null){
                AncestryStatIncrease.Remove(AncestryStatIncrease.Q<Label>("Trait-Increase-one"));
                AncestryStatIncrease.Add(makeTraitDropdown("Trait-Increase-one"));
            }
            else if(AncestryStatIncrease.Q<DropdownField>("Trait-Increase-one") == null)
            {
                AncestryStatIncrease.Add(makeTraitDropdown("Trait-Increase-one"));
            }
            else{
                AncestryStatIncrease.Remove(AncestryStatIncrease.Q<DropdownField>("Trait-Increase-one"));
                AncestryStatIncrease.Add(makeTraitDropdown("Trait-Increase-one"));
            }

        }

        //If the ablity is not free then check to see if there a labal already if not check for a dropdown 
        if(AbilityTwo != "Free"){
            if(AncestryStatIncrease.Q<Label>("Trait-Increase-two") == null){
                if(AncestryStatIncrease.Q<DropdownField>("Trait-Increase-two") != null){
                    AncestryStatIncrease.Remove(AncestryStatIncrease.Q<DropdownField>("Trait-Increase-two"));
                    AncestryStatIncrease.Add(makeTraitLabel("Trait-Increase-two"));
                    
                }else{
                    AncestryStatIncrease.Add(makeTraitLabel("Trait-Increase-two"));
                }
            }

            AncestryStatIncrease.Q<Label>("Trait-Increase-two").text = AbilityTwo;
            removeItemFromTraitList(AbilityTwo);

        }
        else
        {
            if (AncestryStatIncrease.Q<Label>("Trait-Increase-two") != null){
                AncestryStatIncrease.Remove(AncestryStatIncrease.Q<Label>("Trait-Increase-two"));
                AncestryStatIncrease.Add(makeTraitDropdown("Trait-Increase-two"));
            }
            else if(AncestryStatIncrease.Q<DropdownField>("Trait-Increase-two") == null){
                AncestryStatIncrease.Add(makeTraitDropdown("Trait-Increase-two"));
            }
            else{
                AncestryStatIncrease.Remove(AncestryStatIncrease.Q<DropdownField>("Trait-Increase-two"));
                AncestryStatIncrease.Add(makeTraitDropdown("Trait-Increase-two"));
            }
        }

        //If there is a third ability then make a drop down for the user to chose an ability to increase
        if (dataLoader.ancestryList.Find(x => x.name == RaceName).ability.Length == 3)
        {
            if (PlayerStatIncrease.Q<DropdownField>("Pickable-Stat-DropDown") == null)
            {
                PlayerStatIncrease.Add(makeTraitDropdown("Pickable-Stat-DropDown"));
            }
            else
            {
                PlayerStatIncrease.Remove(PlayerStatIncrease.Q<DropdownField>("Pickable-Stat-DropDown"));
                PlayerStatIncrease.Add(makeTraitDropdown("Pickable-Stat-DropDown"));
            }
        }
        else{
            PlayerStatIncrease.Remove(PlayerStatIncrease.Q<DropdownField>("Pickable-Stat-DropDown"));
        }
    }

    //Maker of all dropdowns used in Ancestry
    private DropdownField makeTraitDropdown(String title){

        DropdownField AbilityChoser = new DropdownField {
            name = title,
            choices = getTraitList(),
            label = ""
        };

        AbilityChoser.value = "Select Trait";

        AbilityChoser.styleSheets.Add(AncestriesAbility);

        return AbilityChoser;

    }

    //Maker of all Lables used in Ancestry
     private Label makeTraitLabel(String title){

        Label AbilityGiven = new Label {

            name = title,

        };

        AbilityGiven.styleSheets.Add(AncestriesAbility);

        return AbilityGiven;

    }

    //The setter, getter, and the resetter for the list of abilities. Used to make sure that the user can't pick the same abilty twice
    private List<string> getTraitList(){
        return TraitChoices;
    }
    private void removeItemFromTraitList(String TraitName){
        TraitChoices.Remove(TraitName);
    }
    private void resetTraitList(){
        TraitChoices = new List<string> { "Select Trait", "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma" };
    }

}