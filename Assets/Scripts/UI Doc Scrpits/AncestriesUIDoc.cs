using System;
using UnityEngine;
using UnityEngine.UIElements;

public class AncestriesUIDoc : MonoBehaviour
{
    public VisualElement root;
    public JSONDataLoader dataLoader;
    private VisualElement Ancestries;
    private VisualElement AncestrySummaries;
    private VisualElement AncestryStatIncrease;
    private VisualElement PlayerStatIncrease;
    private StyleSheet AncestriesButtons;
    private StyleSheet AncestriesAbility;
    private bool populate = true;

    // Start is called before the first frame update
    void Start(){
        root = GetComponent<UIDocument>().rootVisualElement;
        Ancestries = root.Q<VisualElement>("Ancestries").Q<VisualElement>("Ancestries-Holder");
        AncestrySummaries = root.Q<VisualElement>("AncestrySummry").Q<VisualElement>("AncestrySummry");
        AncestryStatIncrease = root.Q<VisualElement>("AncestrySummry").Q<VisualElement>("Stat-Increase").Q<VisualElement>("Preset-Stat");
        PlayerStatIncrease = root.Q<VisualElement>("AncestrySummry").Q<VisualElement>("Stat-Increase").Q<VisualElement>("Pickable-Stat");
        AncestriesButtons = Resources.Load<StyleSheet>("CSS/AnceseryButtons");
        AncestriesAbility = Resources.Load<StyleSheet>("CSS/AnceseryAbilities");

    }

    void LateUpdate(){
       
        populateAncestries();

    }

    private void populateAncestries(){
        //Debug.Log("I am here");
        if (populate) //Makessure this runs once
        {
            for (int i = 0; i < dataLoader.ancestryList.Count; i++)
            {
                if (dataLoader.ancestryList[i].type == "Ancestry")
                {
                    Label NewAncestry = new Label
                    {
                        name = dataLoader.ancestryList[i].name,
                        text = dataLoader.ancestryList[i].name
                    };

                    //NewAncestry.onClick.AddListener(delegate { populateAncestSumry(); });
                    //Debug.Log(NewAncestry);
                    NewAncestry.styleSheets.Add(AncestriesButtons);
                    NewAncestry.AddManipulator(new Clickable(click => populateAncestry(NewAncestry.name)));
                    Ancestries.Add(NewAncestry);
                }
            }
        }
        populate = false;
    }

    private void populateAncestry(String RaceName){
        populateAncestSumry(RaceName);
        populateStatIncrease(RaceName);
    }
    private void populateAncestSumry(String RaceName){
        //Debug.Log("IM here!!!");
        AncestrySummaries.Q<Label>("Summery").text = dataLoader.ancestryList.Find(x => x.name == RaceName).summary;
    }
    private void populateStatIncrease(String RaceName){

        String AbilityOne = dataLoader.ancestryList.Find(x => x.name == RaceName).ability[0];
        String AbilityTwo = dataLoader.ancestryList.Find(x => x.name == RaceName).ability[1];

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
                //Do Nothing
            }

        }

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
                //Do Nothing
            }
        }
    
        if(dataLoader.ancestryList.Find(x => x.name == RaceName).ability.Length == 3){
            if(PlayerStatIncrease.Q<DropdownField>("Pickable-Stat-DropDown") == null){
                PlayerStatIncrease.Add(makeTraitDropdown("Pickable-Stat-DropDown"));
            }else{
                //Do Nothing
            }
        }else if(PlayerStatIncrease.Q<DropdownField>("Pickable-Stat-DropDown") != null){
            PlayerStatIncrease.Remove(PlayerStatIncrease.Q<DropdownField>("Pickable-Stat-DropDown"));
        }else{
            //Do Nothing
        }

    }

    private DropdownField makeTraitDropdown(String title){

        DropdownField AbilityChoser = new DropdownField {
            name = title,
            choices = { "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma" },
            label = ""
        };

        AbilityChoser.styleSheets.Add(AncestriesAbility);

        return AbilityChoser;

    }

     private Label makeTraitLabel(String title){

        Label AbilityGiven = new Label {

            name = title,

        };

        AbilityGiven.styleSheets.Add(AncestriesAbility);

        return AbilityGiven;

    }

}