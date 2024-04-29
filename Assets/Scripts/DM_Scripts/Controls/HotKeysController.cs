using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HotKeys : MonoBehaviour
{
    private DMInputManager inputManager;

    private InputManager playerInputManagerScript;

    public bool isActive;

    [SerializeReference]
    private GameObject playerInputManager;

    [SerializeReference]
    private GameObject playerCamera;

    [SerializeReference]
    private GameObject buildMenu;
    public static bool canEdit = false;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = DMInputManager.Instance;
        playerInputManagerScript = playerInputManager.GetComponent<InputManager>();
        canEdit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager.isViewPressed())
        {
            playerInputManagerScript.enabled = !playerInputManagerScript.enabled;
            isActive = playerInputManagerScript.enabled;

            canEdit = !canEdit;
            buildMenu.gameObject.SetActive(!isActive);
            buildMenu.GetComponent<SidebarUI>().enabled = !isActive;
            buildMenu.GetComponent<BottomBarUI>().enabled = !isActive;
        }
    }

}
