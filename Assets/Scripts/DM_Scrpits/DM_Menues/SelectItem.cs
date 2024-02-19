using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class SelectItem : MonoBehaviour
{

    [SerializeField]
    private Camera freeCam;
    private GameObject itemSelected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnMouseHover();

        Debug.Log(itemSelected);
    }

    private bool gettingGameObject(out RaycastHit hit){
        Ray selectBeam = freeCam.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(selectBeam, out hit);
    }

    private void OnMouseHover(){
        
        if (Input.GetMouseButtonDown(0) && gettingGameObject(out RaycastHit hit))
        {
            //Grab the object the mouse is currently over via ray cast.
            //Needs to hit a 3D collider
            itemSelected = hit.collider.gameObject; 
        }else if(Input.GetMouseButtonDown(1)){
            itemSelected = null;
        }
    }
}
