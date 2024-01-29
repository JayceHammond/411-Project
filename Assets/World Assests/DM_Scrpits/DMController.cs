using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DMController : MonoBehaviour
{
    private InputManager inputManager;
    private Transform cameraTransform;
    public float flySpeed;
    public List<string> dmAssets = new();
    // Start is called before the first frame update
    void Start()
    {
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        moveDM();

    }

    public void moveDM(){
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

    public void getAssets(){
        DirectoryInfo dmFolder = new("Assets/DM_Assets/Foliage");
        FileInfo[] info = dmFolder.GetFiles("*.prefab");
        info.Select(f => f.FullName).ToArray();
        foreach(FileInfo f in info){
            dmAssets.Add(f.ToString());
            Debug.Log(f.ToString());
        }
    }

}
