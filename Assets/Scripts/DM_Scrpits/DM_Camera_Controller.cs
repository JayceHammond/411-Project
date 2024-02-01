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

    private float newSpeed;
    private float rotateX;
    private float rotateY;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        //Sensitivity is a slider that allows the user to adjust the movement speed of their camera to move faster or slower
        sensitivity += Input.mouseScrollDelta.y;
        if(sensitivity < 0 ){
            //Makes moves slower
            sensitivity = math.abs(sensitivity * 0.25f);
            Debug.Log(sensitivity);
        }else if(sensitivity >= 1) {
            //Changes from fractional movement to whole numbers
            sensitivity = (int)sensitivity;
        }else if(sensitivity >= 10){
            //Limits the max movement speed to 10 since going above seems excessive
            sensitivity = 10;
        }

        // Move the camera forward, backward, left, and right
        transform.position += transform.forward * Input.GetAxis("Vertical") * intialSpeed * sensitivity * Time.deltaTime;
        transform.position += transform.right * Input.GetAxis("Horizontal") * intialSpeed * sensitivity * Time.deltaTime;

        if (Input.GetKey(KeyCode.Z))
        {
            transform.position += transform.up;
        }
        else if(Input.GetKey(KeyCode.X)){
            transform.position -= transform.up;
        }

        // Rotate the camera based on the mouse movement
        if (Input.GetKey(KeyCode.Q))
        {
            rotateX--;
            transform.eulerAngles += new Vector3(0, rotateX, 0);
        }
        else if(Input.GetKey(KeyCode.E)){
            rotateX++;
            transform.eulerAngles += new Vector3(0, rotateX, 0);
        }else{
            rotateX = 0;
        }

        rotateY = Input.GetAxis("Mouse Y");
        

    }
}
