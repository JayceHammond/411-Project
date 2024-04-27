using UnityEngine;

public class DMCameraController : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 1.0f;
    private const float intialSpeed = 25f;
    private MouseInputs mouseInputs;
    private new Camera camera;
    
    // Start is called before the first frame update
    void Start()
    {
        mouseInputs = MouseInputs.Instance;
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //newCameraMovement();
    }

    public void newCameraMovement(){
        Vector3 inputDir = Vector3.zero;
        Vector3 movementDir;
        if (mouseInputs.rightClick()) {
            mouseInputs.isFocused(true);

            //Camera Rotation
            transform.eulerAngles += new Vector3(mouseInputs.MouseMovement().y, mouseInputs.MouseMovement().x, 0);
            //transform.position += new Vector3(0, 0, -mouseInputs.scrollWheel().y);

            //Camera Movement
            if(Input.GetKey(KeyCode.W)){
                inputDir.z += intialSpeed;
            }
            if(Input.GetKey(KeyCode.S)){
                inputDir.z -= intialSpeed;
            }
            if(Input.GetKey(KeyCode.A)){
                inputDir.x -= intialSpeed;
            }
            if(Input.GetKey(KeyCode.D)){
                inputDir.x += intialSpeed;
            }

            movementDir = (transform.forward * inputDir.z) + (transform.right * inputDir.x);

            if(Input.GetKey(KeyCode.LeftShift)){
                sensitivity = 4f;
            }else{
                sensitivity = 1f;
            }

            transform.position += movementDir * sensitivity * Time.deltaTime;
        }else{
            camera.fieldOfView -= mouseInputs.scrollWheel().y;
            mouseInputs.isFocused(false);
        }
    }

    private void intialCameraMovement(){
        // Move the camera based on mouse movement while pressing MiddleMouse Button
        if (mouseInputs.rightClick())
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
            transform.position += new Vector3(0 , 0 , -mouseInputs.scrollWheel().y);

        }else{
            //Scrollwheel zooms in
            camera.fieldOfView += mouseInputs.scrollWheel().y;
        }
    }
}
