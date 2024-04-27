using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class CardManager : MonoBehaviour{


    public List<GameObject> AllCards = new List<GameObject>();

    //Each Deck that we will have
    public List<Card> MovementDeck = new List<Card>();
    public List<Card> AttackDeck = new List<Card>();
    public List<Card> SpellDeck = new List<Card>();

    private VisualElement CardOne;

    void Start(){
        AllCards = Resources.LoadAll<GameObject>("Cards").ToList<GameObject>();

        CardOne = GameObject.Find("Card 1").GetComponent<UIDocument>().rootVisualElement;

        CardOne.Q<VisualElement>("CardBase").Q<VisualElement>("InnerCard").Q<VisualElement>("TopInfo").Q<VisualElement>("SecondLayer").Q<Label>("Title").text = "FireBall";

    }


}
