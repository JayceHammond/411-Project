using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public static Dictionary<string, int> proficiencyRanks = new Dictionary<string,int>(){
        {"UNTRAINED", 0},
        {"TRAINED", 2},
        {"EXPERT", 4},
        {"MASTER", 6},
        {"LEGENDARY", 8}
    };
    public class Skill{
        public string rank;
        public string statName;
        public string description;
        public int statBonus;
        public int proficiencyBonus;
        public int otherBonuses;

        public Skill(string rankString, string name, string desc, int statNum, int profBon, int other){
            rank = rankString;
            statName = name;
            description = desc;
            statBonus = statNum;
            proficiencyBonus = profBon;
            otherBonuses = other; 
        }
        
    }
}
