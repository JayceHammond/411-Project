using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters;
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
        {"Intimidation", "EXPERT"},
        {"Medicine", "EXPERT"},
        {"Nature", "TRAINED"},
        {"Occultism","UNTRAINED"},
        {"Performance", "TRAINED"},
        {"Religion", "UNTRAINED"},
        {"Society", "UNTRAINED"},
        {"Stealth", "EXPERT"},
        {"Survival", "TRAINED"},
        {"Thievery", "TRAINED"}
    };

    

    //STATS
    public int STR;
    public int DEX;
    public int CON;
    public int INT;
    public int WIS;
    public int CHAR;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)){
            rollAcrobatics();
        }
        Debug.Log(finalRoll);
        Debug.Log(SideChecker.sharedSideVal);
    }

    public int calculateStatBonus(int stat){
        return (stat - 10)/ 2;
    }


    public void calculateRoll(int stat, int profBon){
        int statBonus = calculateStatBonus(stat);
        DiceController.rollDie(this.transform);
        StartCoroutine(WaitForRoll(statBonus, profBon));
    }

    IEnumerator WaitForRoll(int statBonus, int profBon){
        yield return new WaitUntil(SideChecker.isDone);
        finalRoll = SideChecker.sharedSideVal + statBonus + profBon;
        DiceController.displayText.text = finalRoll.ToString();
        SideChecker.allowedToCalculate = false;
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
