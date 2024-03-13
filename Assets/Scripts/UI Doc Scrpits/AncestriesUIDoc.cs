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
    private StyleSheet AncestriesButtons;
    private bool populate = true;

    // Start is called before the first frame update
    void LateUpdate(){
        root = GetComponent<UIDocument>().rootVisualElement;
        Ancestries = root.Q<VisualElement>("Ancestries").Q<VisualElement>("Ancestries-Holder");
        AncestrySummaries = root.Q<VisualElement>("AncestrySummry").Q<VisualElement>("AncestrySummry");
        AncestryStatIncrease = root.Q<VisualElement>("AncestrySummry").Q<VisualElement>("Stat-Increase").Q<VisualElement>("Preset-Stat");
        AncestriesButtons = Resources.Load<StyleSheet>("CSS/AnceseryButtons");

        populateAncestries();

    }

    private void populateAncestries(){
        //Debug.Log("I am here");
        if (populate)
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

        if (dataLoader.ancestryList.Find(x => x.name == RaceName).ability[0] != "Free")
            AncestryStatIncrease.Q<Label>("Trait-Increase-one").text = dataLoader.ancestryList.Find(x => x.name == RaceName).ability[0];
        else
            AncestryStatIncrease.Q<Label>("Trait-Increase-one").text = "None";

        if(dataLoader.ancestryList.Find(x => x.name == RaceName).ability[1] != "Free")
            AncestryStatIncrease.Q<Label>("Trait-Increase-two").text = dataLoader.ancestryList.Find(x => x.name == RaceName).ability[1];
        else
            AncestryStatIncrease.Q<Label>("Trait-Increase-two").text = "None";
    }

}