using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    //list of all the game states
    public enum GameState
    {
        MainMenu,
        Combat
    }

    public Camera mainCamera;
    public Camera combatCamera;

    private GameState currentGameState;

    void Start()
    {
        SetGameState(GameState.MainMenu);
    }

    public void SetGameState(GameState newState)
    {
        DisableGameState(currentGameState);

        //endable new state elements
        EnableGameState(newState);
        
        //update current game state
        currentGameState = newState;
    }
    //logic to disable the ui, scripts, ect.
    void DisableGameState(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu:
                break;
            case GameState.Combat:
                break;
            //add more in the future
        }
    }

    void EnableGameState(GameState state)
    {
        //logic to enable UI elements and scripts related to the new state
        switch (state)
        {
            case GameState.MainMenu:
                break;
            case GameState.Combat:
                break;
        }
    }

    public void EnterCombat()
    {
        //switch to combat mode
        mainCamera.enabled = false;
        combatCamera.enabled = true;
        SetGameState(GameState.Combat);
    }


}
