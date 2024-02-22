using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MouseInputs : MonoBehaviour
{
    
    private static MouseInputs _instance;
    public static MouseInputs Instance{
        get{
            return _instance;
        }
    }
    private void Awake(){
        if(_instance != null && _instance != this){
            Destroy(this.gameObject);
        }else{
            _instance = this;
        }
    }

    private Vector3 mouseMovement;
    private Vector2 mouseScroll;

    public Vector3 MouseMovement(){
        mouseMovement.x = Input.GetAxis("Mouse X");
        mouseMovement.y = -Input.GetAxis("Mouse Y");
        return mouseMovement;
    }

    public Vector2 scrollWheel(){
        mouseScroll.y = Input.mouseScrollDelta.y;
        return mouseScroll;
    }

    public bool rightClick(){
        return Input.GetKey(KeyCode.Mouse1);
    }

    public bool scrollWheelClick(){
        return Input.GetKey(KeyCode.Mouse2);
    }

    //Locks the cusrsor in place
    public void isFocused(bool setter){
        if(setter)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }
}
