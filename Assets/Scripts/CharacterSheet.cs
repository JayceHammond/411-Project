using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters;
using Cinemachine;
using TMPro;
using UnityEngine;
using Skill = SkillController.Skill;

public class CharacterSheet : MonoBehaviour
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


    public TextMeshProUGUI fpsTMP;
    public string fpsText;
	public float deltaTime;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(DiceController.displayText.text);
        if(Input.GetKeyDown(KeyCode.J)){
            rollFlatDie();

        }
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
		float fps = 1.0f / deltaTime;
		fpsText = Mathf.Ceil (fps).ToString ();
        fpsTMP.text = fpsText;
    }

    public int calculateStatBonus(int stat){
        if(stat == 0){
            return 0;
        }
        return (stat - 10)/ 2;
    }


    public void calculateRoll(int stat, int profBon){
        int statBonus = calculateStatBonus(stat);
        DiceController.rollDie(dice[selectedDie],this.transform);
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
        calculateRoll(0,0);
    }

    //SKILL CHECKS
    public void rollAcrobatics(){
        int rankBonus = SkillController.proficiencyRanks[skills["Acrobatics"]];
        calculateRoll(DEX, rankBonus);
    }
    public void rollArcana(){
        int rankBonus = SkillController.proficiencyRanks[skills["Arcana"]];
        calculateRoll(INT,rankBonus);
    }
    public void rollAthletics(){
        int rankBonus = SkillController.proficiencyRanks[skills["Athletics"]];
        calculateRoll(STR, rankBonus);
    }
    public void rollCrafting(){
        int rankBonus = SkillController.proficiencyRanks[skills["Crafting"]];
        calculateRoll(INT, rankBonus);
    }
    public void rollDeception(){
        int rankBonus = SkillController.proficiencyRanks[skills["Deception"]];
        calculateRoll(CHAR, rankBonus);
    }
    public void rollDiplomacy(){
        int rankBonus = SkillController.proficiencyRanks[skills["Diplomacy"]];
        calculateRoll(CHAR, rankBonus);
    }
    public void rollInitimidation(){
        int rankBonus = SkillController.proficiencyRanks[skills["Intimidation"]];
        calculateRoll(CHAR, rankBonus);
    }
    public void rollMedicine(){
        int rankBonus = SkillController.proficiencyRanks[skills["Medicine"]];
        calculateRoll(WIS, rankBonus);
    }
    public void rollNature(){
        int rankBonus = SkillController.proficiencyRanks[skills["Nature"]];
        calculateRoll(WIS, rankBonus);
    }   
    public void rollOccultism(){
        int rankBonus = SkillController.proficiencyRanks[skills["Occultism"]];
        calculateRoll(INT, rankBonus);
    }
    public void rollPerformance(){
        int rankBonus = SkillController.proficiencyRanks[skills["Performance"]];
        calculateRoll(CHAR, rankBonus);
    }
    public void rollReligion(){
        int rankBonus = SkillController.proficiencyRanks[skills["Religion"]];
        calculateRoll(WIS, rankBonus);
    }
    public void rollSociety(){
        int rankBonus = SkillController.proficiencyRanks[skills["Society"]];
        calculateRoll(INT, rankBonus);
    }
    public void rollStealth(){
        int rankBonus = SkillController.proficiencyRanks[skills["Stealth"]];
        calculateRoll(DEX, rankBonus);
    }   
    public void rollSurvival(){
        int rankBonus = SkillController.proficiencyRanks[skills["Survival"]];
        calculateRoll(WIS, rankBonus);
    }
    public void rollThievery(){
        int rankBonus = SkillController.proficiencyRanks[skills["Thievery"]];
        calculateRoll(DEX, rankBonus);
    }   
}
