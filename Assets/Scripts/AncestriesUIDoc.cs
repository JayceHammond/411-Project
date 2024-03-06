using UnityEngine;
using UnityEngine.UIElements;

public class AncestriesUIDoc : MonoBehaviour
{
    public VisualElement root;
    public JSONDataLoader dataLoader;
    private VisualElement Ancestries;
    private VisualElement AncestrySummaries;
    private StyleSheet AncestriesButtons;
    private bool populate = true;

    // Start is called before the first frame update
    void LateUpdate(){
        root = GetComponent<UIDocument>().rootVisualElement;
        Ancestries = root.Q<VisualElement>("Ancestries").Q<VisualElement>("Ancestries-Holder");
        AncestrySummaries = root.Q<VisualElement>("AncestrySummry").Q<VisualElement>("AncestrySummry");
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
                    NewAncestry.AddManipulator(new Clickable(click => populateAncestSumry(NewAncestry.name)));
                    Ancestries.Add(NewAncestry);
                }
            }
        }
        populate = false;
    }

    private void populateAncestSumry(System.String name2){
        Debug.Log("IM here!!!");
        AncestrySummaries.Q<Label>("Summery").text = dataLoader.ancestryList.Find(x => x.name == name2).summary;
    }
}
