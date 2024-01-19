using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSheet : MonoBehaviour
{
    

    public string playerName;
    public int level;
    public int expPoints;
    public int hitPoints;
    public int tempHP;
    public List<string> conditions;
    public int classDC;
    public int heroPoints;
    public int speed;
    public int perceptionBonus;

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
            rollAthletics(calculateStatBonus(STR), DiceController.rollDie(this.transform));
        }
    }

    public void rollAthletics(int bonus, int dieVal){
        int finalRoll = dieVal + bonus;
        DiceController.displayText.text = finalRoll.ToString();
    }

    public int calculateStatBonus(int stat){
        return (stat - 10)/ 2;
    }


    
}
