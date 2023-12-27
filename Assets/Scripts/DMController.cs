using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMController : MonoBehaviour
{
    private InputManager inputManager;
    private Transform cameraTransform;
    public float flySpeed;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W)){
            transform.Translate(cameraTransform.transform.forward * flySpeed);
        }
        if(Input.GetKey(KeyCode.A)){
            transform.Translate(cameraTransform.transform.right * -flySpeed);
        }
        if(Input.GetKey(KeyCode.D)){
            transform.Translate(cameraTransform.transform.right * flySpeed);
        }
        if(Input.GetKey(KeyCode.S)){
            transform.Translate(cameraTransform.transform.forward * -flySpeed);
        }
        if(Input.GetKey(KeyCode.E)){
            transform.Translate(cameraTransform.up * flySpeed);
        }
        if(Input.GetKey(KeyCode.Q)){
            transform.Translate(cameraTransform.up * -flySpeed);
        }
    }
}
