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
    public CharacterSheet selectedChar;
    public bool populating = true;
    public List<TextMeshProUGUI> selectedAbilityBon;
    public TMP_Dropdown freeStat;
    private string fullText;
    private string truncText;
    private TextMeshProUGUI selectedAncestryText;


    //DELETE AFTER MIDTERM
    private bool waitForFreeChoice;
    private bool hasRun = false;


    void Awake()
    {
        dataLoader.ancestryList.Sort((x, y) => x.name.CompareTo(y.name));
        selectedAncestryText = GameObject.Find("SelectedAncestryText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(waitForFreeChoice);
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
        if(freeStat.options.Count > 0 && freeStat.options[freeStat.value].text != "None Selected" && hasRun == false){
            waitForFreeChoice = true;
        }
        if(waitForFreeChoice == true){
            //Debug.Log(freeStat.options[freeStat.value].text);
            selectedChar.statsGen[freeStat.options[freeStat.value].text] = 2;
            selectedChar.updateStats();
            waitForFreeChoice = false;
            hasRun = true;
        }

    }

    public void onAncestryClick(Button button){
        selectedChar.resetStats();
        freeStat.options.Clear();
        freeStat.options.Add(new TMP_Dropdown.OptionData() {text = "None Selected"});
        freeStat.value = 0;
        ancestryDescTitle.text = button.GetComponentInChildren<TextMeshProUGUI>().text;
        selectedAncestryText.text = ancestryDescTitle.text;
        JSONDataLoader.AncestryData selectedAncestry = dataLoader.ancestryList.Find(x => x.name == ancestryDescTitle.text);
        fullText = selectedAncestry.summary;
        truncText = fullText.Truncate(300, "...");
        ancestrySummary.text = fullText;

        for(int i = 0; i < selectedAncestry.ability.Length; i++){
            if(selectedAncestry.ability[i] == "Free"){
                foreach(string stat in selectedChar.statsGen.Keys){
                    if(!selectedAncestry.ability.Contains(stat)){
                        freeStat.options.Add(new TMP_Dropdown.OptionData() {text = stat});
                    }
                }
            }
            else{
                selectedAbilityBon[i].text = selectedAncestry.ability[i];
                selectedChar.statsGen[selectedAbilityBon[i].text] = 2;
            }
            
        }
        //selectedChar.updateStats();
    }






}
