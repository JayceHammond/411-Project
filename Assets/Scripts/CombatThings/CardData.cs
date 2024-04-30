using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardData : MonoBehaviour
{
    public string title;
    public int actions;
    public string description;
    public int range;
    public string element;
    public string damage;

    public void Initialize(string newTitle, int newActions, string newDescription, int newRange, string newElement, string newDamage)
    {
        title = newTitle;
        actions = newActions;
        description = newDescription;
        range = newRange;
        element = newElement;
        damage = newDamage;
    }

    public void DisplayData()
    {
        Debug.Log("Title: " + title);
        Debug.Log("Action: " + actions);
        Debug.Log("Description: " + description);
        Debug.Log("Range: " + range);
        Debug.Log("Element:" + element);
        Debug.Log("Damage: " + damage);
    }

    void Start()
    {
        
        //myCardData.DisplayData();
    }
}
