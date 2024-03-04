using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

public class AncestriesUIDoc : MonoBehaviour
{
    public VisualElement root;
    public JSONDataLoader dataLoader;
    private VisualElement Ancestries;
    private VisualElement AncestrySummaries;
    private bool populate = true;

    // Start is called before the first frame update
    void LateUpdate(){
        root = GetComponent<UIDocument>().rootVisualElement;
        Ancestries = root.Q<VisualElement>("Ancestries").Q<VisualElement>("Ancestries-Holder");
        AncestrySummaries = root.Q<VisualElement>("AncestrySummry").Q<VisualElement>("AncestrySummry");

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
                    Button NewAncestry = new Button
                    {
                        name = dataLoader.ancestryList[i].name,
                        text = dataLoader.ancestryList[i].name
                    };
                    
                    //NewAncestry.onClick.AddListener(delegate { populateAncestSumry(); });
                    //Debug.Log(NewAncestry);
                    Ancestries.Add(NewAncestry);
                }
            }
        }
        populate = false;
    }

    private void populateAncestSumry(){

    }
}
