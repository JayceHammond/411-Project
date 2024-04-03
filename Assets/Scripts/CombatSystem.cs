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
    public CombatState state;
    // Start is called before the first frame update
    void Start()
    {
        state = CombatState.START;
        SetupCombat();
    }

    void SetupCombat()
    {
        
    }


}
