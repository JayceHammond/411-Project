using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CardManager : MonoBehaviour{

    public CardData myCardData;

    public List<CardData> AllCards = new List<CardData>();

    //Each Deck that we will have
    public List<CardData> MovementDeck = new List<CardData>();
    public List<CardData> AttackDeck = new List<CardData>();
    public List<CardData> SpellDeck = new List<CardData>();

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
        myCardData.Initialize("FireBall ", 1, "Launch a fiery projectile", 3, "Fire", "2d6 damage");


        //Getting the Front of the Card
        FrontOfCard = GameObject.Find("Front");

        //Getting all the cards in resources
        GameObject[] cardObjects = Resources.LoadAll<GameObject>("Cards");

        //convert gameObjects to CardData instances
        foreach(GameObject cardObject in cardObjects)
        {
            CardData cardData = Instantiate(cardObject).GetComponent<CardData>();
            if(cardData != null)
            {
                AllCards.Add(cardData);
            }
        }

        //Vars to Quick Change the Card
        FrontOfCardUI = GameObject.Find("Card-3D").GetComponentInChildren<UIDocument>().rootVisualElement;
        Actions = FrontOfCardUI.Q<VisualElement>("CardBase").Q<VisualElement>("InnerCard").Q<VisualElement>("TopInfo").Q<VisualElement>("FirstLayer").Q<VisualElement>("Actions");
        Damage = FrontOfCardUI.Q<VisualElement>("CardBase").Q<VisualElement>("InnerCard").Q<VisualElement>("TopInfo").Q<VisualElement>("FirstLayer").Q<VisualElement>("Damage").Q<Label>("Dam");
        Element = FrontOfCardUI.Q<VisualElement>("CardBase").Q<VisualElement>("InnerCard").Q<VisualElement>("TopInfo").Q<VisualElement>("FirstLayer").Q<VisualElement>("ElementType").Q<Label>("Element");
        Title = FrontOfCardUI.Q<VisualElement>("CardBase").Q<VisualElement>("InnerCard").Q<VisualElement>("TopInfo").Q<VisualElement>("SecondLayer").Q<Label>("Title");
        Picture = FrontOfCardUI.Q<VisualElement>("CardBase").Q<VisualElement>("InnerCard").Q<VisualElement>("Image");
        Description = FrontOfCardUI.Q<VisualElement>("CardBase").Q<VisualElement>("InnerCard").Q<VisualElement>("Description").Q<Label>("Descrip");
        
    }

    //Change UDoc for the card. When done changing grab the textrue from the 3D Card, save it as a new texture for the UI Card. Repeat this for every Card in List
    private void Create2DCard(GameObject Card3D, List<GameObject> Cards){

        Texture savedTexture;

        //Get each card
        foreach(GameObject card in Cards){
            //Change the Card
            change3DCard(card);
            //Grab the Texture from the 3DCard and copy it to a new texture for the 2DCard
            savedTexture = getCardTexture(Card3D);

            //Instantiate the 2DCard Template and set it's parent to PlayerHand
            GameObject Card2D = (GameObject)Instantiate(Resources.Load("Cards/Templates/Card-2D"));
            Card2D.transform.parent = GameObject.Find("PlayerHand").transform;

            //Change the texture of the 2DCard the be the one that is saved
            Card2D.GetComponent<RawImage>().texture = savedTexture;
        }
    }

    private Texture getCardTexture(GameObject Card3D){
        //Get texture from the 3D card and send it back
        Texture save3DTexture;

        save3DTexture = Card3D.GetComponent<Renderer>().material.mainTexture;

        return save3DTexture;
    }

    private void change3DCard(GameObject SelectedCard){
        //Set the 3DCards to what the selected card is

    }



}
