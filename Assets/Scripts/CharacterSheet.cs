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

    //STATS
    public int STR;
    public int DEX;
    public int CON;
    public int INT;
    public int WIS;
    public int CHAR;

    //SKILLS
    public Skill acrobatics;
    public Skill arcana;
    public Skill athletics;
    public Skill crafting;
    public Skill deception;
    public Skill diplomacy;
    public Skill intimidation;
    public Skill medicine;
    public Skill nature;
    public Skill occultism;
    public Skill performance;
    public Skill religion;
    public Skill society;
    public Skill stealth;
    public Skill survival;
    public Skill thievery;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)){
            rollAthletics();
        }
        Debug.Log(finalRoll);
        Debug.Log(SideChecker.sharedSideVal);
    }

    public int calculateStatBonus(int stat){
        return (stat - 10)/ 2;
    }


    public void calculateRoll(int stat){
        int statBonus = calculateStatBonus(stat);
        DiceController.rollDie(this.transform);
        StartCoroutine(WaitForRoll(statBonus));
    }

    IEnumerator WaitForRoll(int statBonus){
        yield return new WaitUntil(SideChecker.isDone);
        finalRoll = SideChecker.sharedSideVal + statBonus;
        DiceController.displayText.text = finalRoll.ToString();
        SideChecker.allowedToCalculate = false;
    }

    //SKILL CHECKS
    public void rollAcrobatics(){
        calculateRoll(DEX);
    }
    public void rollArcana(){
        calculateRoll(INT);
    }
    public void rollAthletics(){
        calculateRoll(STR);
    }
    public void rollCrafting(){
        calculateRoll(INT);
    }
    public void rollDeception(){
        calculateRoll(CHAR);
    }
    public void rollDiplomacy(){
        calculateRoll(CHAR);
    }
    public void rollInitimidation(){
        calculateRoll(CHAR);
    }
    public void rollMedicine(){
        calculateRoll(WIS);
    }
    public void rollNature(){
        calculateRoll(WIS);
    }   
    public void rollOccultism(){
        calculateRoll(INT);
    }
    public void rollPerformance(){
        calculateRoll(CHAR);
    }
    public void rollReligion(){
        calculateRoll(WIS);
    }
    public void rollSociety(){
        calculateRoll(INT);
    }
    public void rollStealth(){
        calculateRoll(DEX);
    }   
    public void rollSurvival(){
        calculateRoll(WIS);
    }
    public void rollThievery(){
        calculateRoll(DEX);
    }   
}
