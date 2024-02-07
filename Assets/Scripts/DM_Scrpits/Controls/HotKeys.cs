using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotKeys : MonoBehaviour
{

    private CameraSwitch buttonHandler;
    private DMInputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = DMInputManager.Instance;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager.isViewPressed())
        {
            Debug.Log("Hotkey's is working");
            buttonHandler.toggleControls();
        }
    }

}
