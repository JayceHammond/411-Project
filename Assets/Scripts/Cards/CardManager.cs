using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
    private Label Range;
    private Label Element;
    private Label Title;
    private VisualElement Picture;
    private Label Description;


    void Start(){
        //Making a Card and Adding it to all Cards
        AllCards.Add(makeNewCard("FireBall", "Launch a fiery projectile", "Fire", "2d6", 1, 3));
        AllCards.Add(makeNewCard("Master Archer", "A skilled archer renowned for accuracy and deadly precision with a bow.", "Piercing", "1d8+4", 1, 120));

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
        Range = FrontOfCardUI.Q<VisualElement>("CardBase").Q<VisualElement>("InnerCard").Q<VisualElement>("TopInfo").Q<VisualElement>("SecondLayer").Q<VisualElement>("RangeHolder").Q<Label>("Range");
        
        Create2DCard(AllCards);
    }

    public void OnEnable(){
        //Create2DCard(AllCards);
    }

    //Change UDoc for the card. When done changing grab the textrue from the 3D Card, save it as a new texture for the UI Card. Repeat this for every Card in List
    private void Create2DCard(List<Card> Cards){
        //Get each card
        foreach(Card card in Cards){
            Texture savedTexture;
            //Change the Card
            change3DCard(card);
            //Grab the Texture from the 3DCard and copy it to a new texture for the 2DCard
            savedTexture = getCardTexture();

            //Instantiate the 2DCard Template and set it's parent to PlayerHand
            GameObject Card2D = (GameObject)Resources.Load("Cards/Templates/Card-2D");
            //Card2D.transform.name = card.name;
            GameObject SpawnedCard = Instantiate(Card2D);
            SpawnedCard.transform.parent = GameObject.Find("Content").transform;

            //Change the texture of the 2DCard the be the one that is saved
            SpawnedCard.GetComponent<RawImage>().texture = savedTexture;
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
        Description.text = SelectedCard.description;
        
        Element.text = SelectedCard.element;
        if(SelectedCard.element.ToLower() == "fire"){
            Element.style.color = new Color(235, 64, 52);
        }else if(SelectedCard.element.ToLower() == "water"){
            Element.style.color = new Color(9, 138, 237);
        }else if(SelectedCard.element.ToLower() == "wind"){
            Element.style.color = new Color(173, 222, 11);
        }else if(SelectedCard.element.ToLower() == "poison"){
            Element.style.color = new Color(232, 9, 232);
        }else if(SelectedCard.element.ToLower() == "piercing"){
            Element.style.color = new Color(156, 6, 24);
        }

        Damage.text = SelectedCard.damage;
        Range.text = SelectedCard.range.ToString();

        if(SelectedCard.actions == 1){
            Actions.style.backgroundImage = Resources.Load<Texture2D>("/Cards/Templates/SingleAction-removebg-preview");
        }else if(SelectedCard.actions == 2){
            Actions.style.backgroundImage = Resources.Load<Texture2D>("/Cards/Templates/DoubleAction-removebg-preview");
        }else if(SelectedCard.actions >= 3){
            Label ActionCount = new Label
            {
                name = "ActionAmount",
                text = SelectedCard.actions.ToString(),
            };

            Actions.Add(ActionCount);
        }

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
