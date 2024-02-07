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

    private DM_HotKeys hotKeys;
    private void Awake(){
        if(_instance != null && _instance != this){
            Destroy(this.gameObject);
        }else{
            _instance = this;
        }
        hotKeys = new DM_HotKeys();
    }

    public bool isViewPressed(){
        return hotKeys.CameraSwitch.CamSwitchHotkey.triggered;
    }

}
