using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    public static InputManager Instance{
        get{
            return _instance;
        }
    }
    private PlayerControls playerControls;

    private void Awake(){
        if(_instance != null && _instance != this){
            Destroy(this.gameObject);
        }else{
            _instance = this;
        }
        playerControls = new PlayerControls();
        
    }

    private void OnEnable(){
        playerControls.Enable();
    }

    private void OnDisable() {
        playerControls.Disable();
    }

    public Vector2 GetPlayerMovement(){
        return playerControls.Player.Movement.ReadValue<Vector2>();
    }
    
    public Vector2 GetMouseDelta(){
        return playerControls.Player.Look.ReadValue<Vector2>();
    }

    public bool PlayerDefended(){
        return playerControls.Player.Defending.IsPressed();
    }

    public bool PlayerBaseAttack(){
        return playerControls.Player.BaseAttack.triggered;
    }

    public bool PlayerSecondAttack(){
        return playerControls.Player.Attack2.triggered;
    }

    public bool PlayerThirdAttack(){
        return playerControls.Player.Attack3.triggered;
    }

    public bool PlayerJumpedThisFrame(){
        return playerControls.Player.Jump.triggered;
    }

    public bool PlayerRunning(){
        return Input.GetKey(KeyCode.LeftShift);
    }
}
