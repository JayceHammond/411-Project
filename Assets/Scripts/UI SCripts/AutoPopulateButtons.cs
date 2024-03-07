using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using TMPro;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class AutoPopulateButtons : MonoBehaviour
{
    public Button ancestryButton;
    public Transform ancestryButtonParent;
    public TextMeshProUGUI ancestryDescTitle;
    public TextMeshProUGUI ancestrySummary;
    public JSONDataLoader dataLoader;
    public bool populating = true;
    public List<TextMeshProUGUI> selectedAbilityBon;
    public TMP_Dropdown freeStat;
    private string fullText;
    private string truncText;
    // Start is called before the first frame update
    void Awake()
    {
        dataLoader.ancestryList.Sort((x, y) => x.name.CompareTo(y.name));
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(populating == true){
            float offset = 0;
            for(int i = 0; i < dataLoader.ancestryList.Count; i++){
                if(dataLoader.ancestryList[i].type == "Ancestry"){
                    Button button = Instantiate(ancestryButton, ancestryButtonParent);
                    button.transform.position = new UnityEngine.Vector3(ancestryButtonParent.transform.position.x, ancestryButtonParent.transform.position.y - offset, ancestryButtonParent.transform.position.z);
                    offset = button.GetComponent<RectTransform>().rect.height * i;
                    button.GetComponentInChildren<TextMeshProUGUI>().text = dataLoader.ancestryList[i].name;
                    button.onClick.AddListener(delegate {onAncestryClick(button);});

                    //ancestryAbilityBon = dataLoader.ancestryList[i].ability;
                }
            }
            populating = false;
        }
    }

    public void onAncestryClick(Button button){
        ancestryDescTitle.text = button.GetComponentInChildren<TextMeshProUGUI>().text;
        JSONDataLoader.AncestryData selectedAncestry = dataLoader.ancestryList.Find(x => x.name == ancestryDescTitle.text);
        fullText = selectedAncestry.summary;
        truncText = fullText.Truncate(300, "...");
        toggleSummary(false);

        for(int i = 0; i < selectedAncestry.ability.Length; i++){
            if(selectedAncestry.ability[i] == "Free"){
                freeStat.options.Clear();
                freeStat.options.Add(new TMP_Dropdown.OptionData() {text = "None Selected"});
                foreach(string stat in CharacterSheet.statsGen){
                    if(!selectedAncestry.ability.Contains(stat)){
                        freeStat.options.Add(new TMP_Dropdown.OptionData() {text = stat});
                    }
                }
                
            }
            else{
                selectedAbilityBon[i].text = selectedAncestry.ability[i];
            }
            
        }
        //Debug.Log(dataLoader.ancestryList.Find(x => ))
    }

    public void toggleSummary(bool toggled){
        if(toggled == true){
            ancestrySummary.text = fullText;
        }
        else{
            ancestrySummary.text = truncText;
        }
    }


}
