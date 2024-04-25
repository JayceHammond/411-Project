using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour{


    public List<GameObject> AllCards = new List<GameObject>();

    //Each Deck that we will have
    public List<Card> MovementDeck = new List<Card>();
    public List<Card> AttackDeck = new List<Card>();
    public List<Card> SpellDeck = new List<Card>();

    void Start(){
        AllCards = Resources.LoadAll<GameObject>("Cards").ToList<GameObject>();

    }


}
