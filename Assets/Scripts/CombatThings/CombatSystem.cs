using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum CombatState {START, PAYLERTURN, ENEMYTURN, WON, LOST }

public class CombatSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemmyPrefab;

    public Transform playerCombatPosition;
    public Transform enemyCombatPosition;

    Unit playerUnit;
    Unit enemyUnit;

    public CombatState state;
    // Start is called before the first frame update
    void Start()
    {
        state = CombatState.START;
        SetupCombat();
    }

    void SetupCombat()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerCombatPosition);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemmyPrefab, enemyCombatPosition);
        enemyUnit = enemyGO.GetComponent<Unit>();

        Debug.Log("you are in combat");
    }


}
