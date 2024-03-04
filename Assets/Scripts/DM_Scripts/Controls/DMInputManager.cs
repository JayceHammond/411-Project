using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMInputManager : MonoBehaviour
{
    private static DMInputManager _instance;
    public static DMInputManager Instance{
        get{
            return _instance;
        }
    }

    private HotKeyControls hotKeys;
    private void Awake(){
        if(_instance != null && _instance != this){
            Destroy(this.gameObject);
        }else{
            _instance = this;
        }
        hotKeys = new HotKeyControls();
    }

    private void OnEnable(){
        hotKeys.Enable();
    }

    private void OnDisable() {
        hotKeys.Disable();
    }

    public bool isViewPressed(){
        //Debug.Log("Hotkey Manager Value: " + hotKeys.CameraSwitch.SwitchHotkey.triggered);
        return hotKeys.CameraSwitch.SwitchHotkey.triggered;
    }

}
