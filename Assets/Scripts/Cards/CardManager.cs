using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CardManager : MonoBehaviour{

    public Card Card;

    //List of All Cards
    public List<Card> AllCards = new List<Card>();

    //Each Deck that we will have
    public List<Card> MovementDeck = new List<Card>();
    public List<Card> AttackDeck = new List<Card>();
    public List<Card> SpellDeck = new List<Card>();

    //The Front of the Card
    private GameObject FrontOfCard;


    private VisualElement FrontOfCardUI;
    private VisualElement Actions;
    private Label Damage;
    private Label Element;
    private Label Title;
    private VisualElement Picture;
    private Label Description;


    void Start(){
        //Making a Card
        AllCards.Add(makeNewCard("FireBall ", "Launch a fiery projectile", "Fire", "2d6 damage", 1, 3));

        //Getting the Front of the Card
        FrontOfCard = GameObject.Find("Front");

        //Getting all the cards in resources
        GameObject[] cardObjects = Resources.LoadAll<GameObject>("Cards");

        //Vars to Quick Change the Card
        FrontOfCardUI = GameObject.Find("Card-3D").GetComponentInChildren<UIDocument>().rootVisualElement;
        Actions = FrontOfCardUI.Q<VisualElement>("CardBase").Q<VisualElement>("InnerCard").Q<VisualElement>("TopInfo").Q<VisualElement>("FirstLayer").Q<VisualElement>("Actions");
        Damage = FrontOfCardUI.Q<VisualElement>("CardBase").Q<VisualElement>("InnerCard").Q<VisualElement>("TopInfo").Q<VisualElement>("FirstLayer").Q<VisualElement>("Damage").Q<Label>("Dam");
        Element = FrontOfCardUI.Q<VisualElement>("CardBase").Q<VisualElement>("InnerCard").Q<VisualElement>("TopInfo").Q<VisualElement>("FirstLayer").Q<VisualElement>("ElementType").Q<Label>("Element");
        Title = FrontOfCardUI.Q<VisualElement>("CardBase").Q<VisualElement>("InnerCard").Q<VisualElement>("TopInfo").Q<VisualElement>("SecondLayer").Q<Label>("Title");
        Picture = FrontOfCardUI.Q<VisualElement>("CardBase").Q<VisualElement>("InnerCard").Q<VisualElement>("Image");
        Description = FrontOfCardUI.Q<VisualElement>("CardBase").Q<VisualElement>("InnerCard").Q<VisualElement>("Description").Q<Label>("Descrip");
        
        Create2DCard(AllCards);
    }

    public void OnEnable(){
        //Create2DCard(AllCards);
    }

    //Change UDoc for the card. When done changing grab the textrue from the 3D Card, save it as a new texture for the UI Card. Repeat this for every Card in List
    private void Create2DCard(List<Card> Cards){

        Texture savedTexture;

        //Get each card
        foreach(Card card in Cards){
            //Change the Card
            change3DCard(card);
            //Grab the Texture from the 3DCard and copy it to a new texture for the 2DCard
            savedTexture = getCardTexture();

            //Instantiate the 2DCard Template and set it's parent to PlayerHand
            GameObject Card2D = (GameObject)Instantiate(Resources.Load("Cards/Templates/Card-2D"));
            Card2D.transform.parent = GameObject.Find("PlayerHand").transform;

            //Change the texture of the 2DCard the be the one that is saved
            Card2D.GetComponent<RawImage>().texture = savedTexture;
        }
    }

    private Texture getCardTexture(){
        //Get texture from the 3D card and send it back
        Texture save3DTexture;

        save3DTexture = FrontOfCard.GetComponent<Renderer>().material.mainTexture;

        return save3DTexture;
    }

    private void change3DCard(Card SelectedCard){
        //Set the 3DCards to what the selected card is
        Title.text = SelectedCard.title;

    }

    private Card makeNewCard(String Title, String Description, String Element, String Damage, int ActionCount, int Range){
        Card newCard = new Card
        {
            title = Title,
            description = Description,
            element = Element,
            damage = Damage,
            actions = ActionCount,
            range = Range
        };

        return newCard;
    }

}
