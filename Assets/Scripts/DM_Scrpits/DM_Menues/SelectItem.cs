using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class SelectItem : MonoBehaviour
{

    [SerializeField]
    private Camera freeCam;
    private GameObject objectSelected;

    // Update is called once per frame
    void Update()
    {
        //Test of object selection
        objectSelected = objectSelection();
        Debug.Log(objectSelected);
    }

    private bool gettingGameObject(out RaycastHit hit){
        //Sends out a raycast to the object the mouse is over
        Ray selectBeam = freeCam.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(selectBeam, out hit);
    }

    public GameObject objectSelection(){
        
        if (Input.GetMouseButtonDown(0) && gettingGameObject(out RaycastHit hit))
        {
            //Grabs the object the mouse clicks on
            //Needs to hit a 3D collider
            return hit.collider.gameObject;
        }else if(Input.GetMouseButtonDown(1) && (objectSelected != null)){
            return null;
        }else{
            return objectSelected;
        }
    }
}
