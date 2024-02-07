using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class CameraSwitch : MonoBehaviour
{
    private Button button;

    [SerializeReference]
    private GameObject inputManagerCamera;

    [SerializeReference]
    private GameObject playerCamera;

    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(toggleControls);
    }

    void Update(){
        
    }

    public void toggleControls(){
            bool isActive;

            inputManagerCamera.GetComponent<InputManager>().enabled = !inputManagerCamera.GetComponent<InputManager>().enabled;
            isActive = inputManagerCamera.GetComponent<InputManager>().enabled;
            
            playerCamera.SetActive(isActive);
    }
}
