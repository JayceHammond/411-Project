using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private List<Unit> units = new List<Unit>();
    private int currentUnitIndex = 0;
    private bool isPlayerTurn = true;

    public void AddUnit(Unit unit)
    {
        units.Add(unit);
    }

    public void NextTurn()
    {
        currentUnitIndex = (currentUnitIndex + 1) % units.Count;
        isPlayerTurn = !isPlayerTurn;
        if(!isPlayerTurn)
        {
            //add enemy turn logic
        }
    }

    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }
}
