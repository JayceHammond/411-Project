using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSwitch : MonoBehaviour
{
    private Button button;

    [SerializeReference]
    private GameObject inputManager;

    [SerializeReference]
    private GameObject playerCamera;

    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(toggleControls);
    }

    void Update(){
        
    }

    void toggleControls(){

        if (inputManager.activeSelf && playerCamera.activeSelf){
            inputManager.SetActive(false);
            playerCamera.SetActive(false);
        }else{
            inputManager.SetActive(true);
            playerCamera.SetActive(true);
        }
    }
}
