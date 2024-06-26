using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class CharacterSheet : NetworkBehaviour
{
    public int finalRoll = 0;
    public string playerName;
    public int playerLevel;
    public int playerXP;
    public int playerHP;
    public int playerTempHP;
    public List<string> playerConditions;
    public List<GameObject> dice;
    public int selectedDie;
    public int playerClassDC;
    public int playerHeroPoint;
    public int playerSpeed;
    public string ancestry;
    public bool updatingStats = false;


    //WILL DELETE LATER
    public TextMeshProUGUI currentDieText;


    public Dictionary<string, int> statsGen = new Dictionary<string, int>(){
        {"Strength", 0},
        {"Dexterity", 0},
        {"Constitution", 0},
        {"Intelligence", 0},
        {"Wisdom", 0},
        {"Charisma", 0}
    };

    //public JSONDataLoader.AncestryData selectedAncestry;

    //SKILLS
    public Dictionary<string, string> skills = new Dictionary<string, string>(){
        {"Acrobatics", "EXPERT"},
        {"Arcana", "UNTRAINED"},
        {"Athletics", "TRAINED"},
        {"Crafting", "UNTRAINED"},
        {"Deception", "UNTRAINED"},
        {"Diplomacy", "UNTRAINED"},
        {"Intimidation", "TRAINED"},
        {"Medicine", "TRAINED"},
        {"Nature", "UNTRAINED"},
        {"Occultism","TRAINED"},
        {"Performance", "UNTRAINED"},
        {"Religion", "UNTRAINED"},
        {"Society", "UNTRAINED"},
        {"Stealth", "TRAINED"},
        {"Survival", "EXPERT"},
        {"Thievery", "UNTRAINED"}
    };

    

    //STATS
    public int STR;
    public int DEX;
    public int CON;
    public int INT;
    public int WIS;
    public int CHAR;

    //RAW STATS
    public int rawSTR;
    public int rawDEX;
    public int rawCON;
    public int rawINT;
    public int rawWIS;
    public int rawCHAR;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.name == "LocalGamePlayer"){
            if(Input.GetKeyDown(KeyCode.J)){
                Debug.Log("roll");
                if(gameObject.GetComponent<PlayerObjectController>().PlayerIdNumber == 1){
                    hostRollFlatDie();
                }else{
                    rollFlatDie();
                }
                

            }
            if(Input.GetKeyDown(KeyCode.RightBracket)){
                if(selectedDie + 1 <= dice.Count - 1){
                    selectedDie += 1;
                }else{
                    selectedDie = 0;
                }
                currentDieText.text = dice[selectedDie].name;
            }
            if(Input.GetKeyDown(KeyCode.LeftBracket)){
                if(selectedDie - 1 >= 0){
                    selectedDie -= 1;
                }else{
                    selectedDie = 5;
                }
                currentDieText.text = dice[selectedDie].name;
            }

            if(Input.GetKeyDown(KeyCode.H)){
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene(0);
            }
        }

    }

    public void updateStats(){
        STR += statsGen["Strength"];
        DEX += statsGen["Dexterity"];
        CON += statsGen["Constitution"];
        INT += statsGen["Intelligence"];
        WIS += statsGen["Wisdom"];
        CHAR += statsGen["Charisma"];
    }

    public void resetStats(){
        STR = rawSTR;
        DEX = rawDEX;
        CON = rawCON;
        INT = rawINT;
        WIS = rawWIS;
        CHAR = rawCHAR;

        statsGen["Strength"] = 0;
        statsGen["Dexterity"] = 0;
        statsGen["Constitution"] = 0;
        statsGen["Intelligence"] = 0;
        statsGen["Wisdom"] = 0;
        statsGen["Charisma"] = 0;
    }



    public int calculateStatBonus(int stat){
        if(stat <= 0){
            return 0;
        }
        return (stat - 10)/ 2;
    }


    
    public void ClientCalculateRoll(int stat, int profBon){
        int statBonus = calculateStatBonus(stat);
        GameObject.Find("GameplayCam").GetComponent<DiceController>().rollDie(dice[selectedDie],this.transform);
        
        StartCoroutine(WaitForRoll(statBonus, profBon));
    }
    public void HostCalculateRoll(int stat, int profBon){
        int statBonus = calculateStatBonus(stat);
        GameObject.Find("GameplayCam").GetComponent<DiceController>().HostRollDie(dice[selectedDie],this.transform);
        
        StartCoroutine(WaitForRoll(statBonus, profBon));    
    }

    IEnumerator WaitForRoll(int statBonus, int profBon){
        SideChecker.allowedToCalculate = false;
        yield return new WaitUntil(SideChecker.isDone);
        //D20 CRIT STATEMENTS
        if(SideChecker.sharedSideVal == 1){
            DiceController.displayText.color = Color.red;
        }
        else if(SideChecker.sharedSideVal == 20){
            DiceController.displayText.color = Color.green;
        }
        else{
            DiceController.displayText.color = Color.white;
        }
        finalRoll = SideChecker.sharedSideVal + statBonus + profBon;
        DiceController.displayText.text = finalRoll.ToString();
    }

    public void rollFlatDie(){
        ClientCalculateRoll(0,0);
    }
    public void hostRollFlatDie(){
        HostCalculateRoll(0,0);
    }

    //SKILL CHECKS
    public void rollAcrobatics(){
        int rankBonus = SkillController.proficiencyRanks[skills["Acrobatics"]];
        ClientCalculateRoll(DEX, rankBonus);
    }
    public void rollArcana(){
        int rankBonus = SkillController.proficiencyRanks[skills["Arcana"]];
        ClientCalculateRoll(INT,rankBonus);
    }
    public void rollAthletics(){
        int rankBonus = SkillController.proficiencyRanks[skills["Athletics"]];
        ClientCalculateRoll(STR, rankBonus);
    }
    public void rollCrafting(){
        int rankBonus = SkillController.proficiencyRanks[skills["Crafting"]];
        ClientCalculateRoll(INT, rankBonus);
    }
    public void rollDeception(){
        int rankBonus = SkillController.proficiencyRanks[skills["Deception"]];
        ClientCalculateRoll(CHAR, rankBonus);
    }
    public void rollDiplomacy(){
        int rankBonus = SkillController.proficiencyRanks[skills["Diplomacy"]];
        ClientCalculateRoll(CHAR, rankBonus);
    }
    public void rollInitimidation(){
        int rankBonus = SkillController.proficiencyRanks[skills["Intimidation"]];
        ClientCalculateRoll(CHAR, rankBonus);
    }
    public void rollMedicine(){
        int rankBonus = SkillController.proficiencyRanks[skills["Medicine"]];
        ClientCalculateRoll(WIS, rankBonus);
        }
    public void rollNature(){
        int rankBonus = SkillController.proficiencyRanks[skills["Nature"]];
        ClientCalculateRoll(WIS, rankBonus);
        }   
    public void rollOccultism(){
        int rankBonus = SkillController.proficiencyRanks[skills["Occultism"]];
        ClientCalculateRoll(INT, rankBonus);
    }
    public void rollPerformance(){
        int rankBonus = SkillController.proficiencyRanks[skills["Performance"]];
        ClientCalculateRoll(CHAR, rankBonus);
    }
    public void rollReligion(){
        int rankBonus = SkillController.proficiencyRanks[skills["Religion"]];
        ClientCalculateRoll(WIS, rankBonus);
        }
    public void rollSociety(){
        int rankBonus = SkillController.proficiencyRanks[skills["Society"]];
        ClientCalculateRoll(INT, rankBonus);
    }
    public void rollStealth(){
        int rankBonus = SkillController.proficiencyRanks[skills["Stealth"]];
        ClientCalculateRoll(DEX, rankBonus);
    }   
    public void rollSurvival(){
        int rankBonus = SkillController.proficiencyRanks[skills["Survival"]];
        ClientCalculateRoll(WIS, rankBonus);
    }
    public void rollThievery(){
        int rankBonus = SkillController.proficiencyRanks[skills["Thievery"]];
        ClientCalculateRoll(DEX, rankBonus);
    }


    public void updateStr(){
        GameObject strInput = GameObject.Find("STR Score");
        STR = int.Parse(strInput.GetComponent<TMP_InputField>().text);
        rawSTR = STR;
        
    }

    public void updateDEX(){
        GameObject dexInput = GameObject.Find("DEX Score");
        DEX = int.Parse(dexInput.GetComponent<TMP_InputField>().text);
        rawDEX = DEX;
        
    }

    public void updateCON(){
        GameObject conInput = GameObject.Find("CON Score");
        CON = int.Parse(conInput.GetComponent<TMP_InputField>().text);
        rawCON = CON;
    }

    public void updateINT(){
        GameObject intInput = GameObject.Find("INT Score");
        INT = int.Parse(intInput.GetComponent<TMP_InputField>().text);
        rawINT = INT;
    }

    public void updateWIS(){
        GameObject wisInput = GameObject.Find("WIS Score");
        WIS = int.Parse(wisInput.GetComponent<TMP_InputField>().text);
        rawWIS = WIS;
    }

    public void updateCHA(){
        GameObject chaInput = GameObject.Find("CHA Score");
        CHAR = int.Parse(chaInput.GetComponent<TMP_InputField>().text);
        rawCHAR = CHAR;
    }
}
