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
    private Camera camera;
    
    // Start is called before the first frame update
    void Start()
    {
        mouseInputs = MouseInputs.Instance;
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the camera based on mouse movement while pressing MiddleMouse Button
        if (mouseInputs.scrollWheelClick())
        {
            // Locks the cursor moving the camera
            mouseInputs.isFocused(true);
            transform.position += new Vector3(mouseInputs.MouseMovement().x  * sensitivity, -mouseInputs.MouseMovement().y * sensitivity, 0);
        }else{
            mouseInputs.isFocused(false);
        }

        // Rotate the camera based on the mouse movement while pressing RightClick
        if (mouseInputs.rightClick())
        {
            transform.eulerAngles += new Vector3(mouseInputs.MouseMovement().y, mouseInputs.MouseMovement().x, 0);
            camera.fieldOfView += mouseInputs.scrollWheel().y;
        }else{
            // Scrollwheel moves the camera in the Z direction
            transform.position += new Vector3(0 , 0 , -mouseInputs.scrollWheel().y);
        }

    }
}
