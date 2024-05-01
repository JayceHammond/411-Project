using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Card : MonoBehaviour{
        public string title;
        public int actions;
        public string description;
        public int range;
        public string element;
        public string damage;

}

/*

public Card makeNewCard(string title, int actions, string description, int range, string element, string damage){
        CardData newCardData = new CardData();

        newCardData.title = title;
        newCardData.actions = actions;
        newCardData.description = description;
        newCardData.range = range;
        newCardData.element = element;
        newCardData.damage = damage;

        Card newCard = new Card();

        return newCard;
    }

    */
