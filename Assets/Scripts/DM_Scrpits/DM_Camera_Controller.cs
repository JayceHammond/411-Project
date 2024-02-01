using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class DMCameraController : MonoBehaviour
{

    [SerializeField]
    private const float intialSpeed = 10f;

    [SerializeField]
    private float sensitivity = 1.0f;

    private MouseInputs mouseInputs;
    
    // Start is called before the first frame update
    void Start()
    {
        mouseInputs = MouseInputs.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        //Sensitivity is a slider that allows the user to adjust the movement speed of their camera to move faster or slower
        sensitivity += mouseInputs.scrollWheel().y;
        if(sensitivity < 0 ){
            //Makes moves slower
            sensitivity = math.abs(sensitivity * 0.25f);
        }else if(sensitivity >= 1) {
            //Changes from fractional movement to whole numbers
            sensitivity = (int)sensitivity;
        }else if(sensitivity >= 10){
            //Limits the max movement speed to 10 since going above seems excessive
            sensitivity = 10;
        }

        //Move the camera based on mouse movement while pressing MiddleMouse Button
        if (mouseInputs.scrollWheelClick())
        {
            //Locks the cursor moving the camera
            mouseInputs.isFocused(true);
            transform.position += new Vector3(mouseInputs.MouseMovement().x  * sensitivity, -mouseInputs.MouseMovement().y * sensitivity, 0);
        }else{
            mouseInputs.isFocused(false);
        }
        

        // Rotate the camera based on the mouse movement while pressing RightClick
        if (mouseInputs.rightClick())
        {
            transform.eulerAngles += new Vector3(mouseInputs.MouseMovement().y, mouseInputs.MouseMovement().x, 0);
        }

    }
}
