using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;
using Mirror;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.LowLevel;

public class PlayerController : NetworkBehaviour
{
    [SerializeField]
    private float playerSpeed = 1.0f;
    [SerializeField]
    private float speedLimit = 4f;
    private bool isLeft = false;
    private bool isRight = false;

    //private float jumpHeight = 3.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    //private float yVelocity = 0f;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private InputManager inputManager;
    private Transform cameraTransform;
    public GameObject gameplayCam;
    public GameObject uiCam;
    public GameObject cameras;
    private Animator animator;
    // Start is called before the first frame update

    public GameObject PlayerModel;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
        PlayerModel.SetActive(false);
        cameras.SetActive(false);

        //Need this to trigger the animations
        animator = GetComponent<Animator>();
    }

    void toggleCameraLock(){
        if(Input.GetKeyDown(KeyCode.K)){
            uiCam.transform.position = gameplayCam.transform.position;
            uiCam.transform.rotation = gameplayCam.transform.rotation;
            if(gameplayCam.activeSelf == true){
                uiCam.SetActive(true);
                Cursor.lockState = CursorLockMode.Confined;
                gameplayCam.SetActive(false);
            }else{
                gameplayCam.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
                uiCam.SetActive(false);
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Check if we are in Multiplayer Scene
        if(SceneManager.GetActiveScene().name == "MultiplayerTest"){
            if(PlayerModel.activeSelf == false){
                SetPosition();
                PlayerModel.SetActive(true); //Turn on player
                cameras.SetActive(true); //Turn on player camera
                GetComponentInChildren<SpriteBillboard>().GameplayCamera = gameplayCam.GetComponent<Camera>();
                Cursor.lockState = CursorLockMode.Locked;
            }
            if(authority){
                Movement();
            }
            
        }
        toggleCameraLock();
    }

    public void Movement(){
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        
        //Vectors that grab the movement from the player's input to move the char
        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        playerVelocity.y += gravityValue * GameTime.deltaTime;
        controller.Move(playerVelocity * GameTime.deltaTime);

        //Conditonals for movement to try to keep the movement and the animations in the same place

        //Idle
        if(math.abs(movement.x) == 0 && math.abs(movement.y) == 0)
        {
            animator.SetTrigger("Idle");
            animator.ResetTrigger("Walking");
            animator.ResetTrigger("Running");
            
        }
        //Sprinting
        else if ((math.abs(movement.x) > 0 || (math.abs(movement.y) > 0))  && inputManager.PlayerRunning())
        {
            playerSpeed *= 2;
            if(playerSpeed >= speedLimit){
                playerSpeed = speedLimit;
            }
            
            animator.ResetTrigger("Idle");
            animator.ResetTrigger("Walking");
            animator.SetTrigger("Running");
            
            //move.y += gravityValue * Time.deltaTime;

            move = transform.forward * move.z + transform.right * move.x;
            controller.Move(move * GameTime.deltaTime * playerSpeed);

            //To get proper oritation (Keep This)
            if (inputManager.getLeftMovement() && controller.transform.localRotation.eulerAngles.y == 0)
            {
                Debug.Log("I turned left");
                controller.transform.Rotate(0f, 180f, 0f, Space.World);
            }
            else if (!inputManager.getLeftMovement() && controller.transform.localRotation.eulerAngles.y == 180)
            {
                Debug.Log("I turned right");
                controller.transform.Rotate(0f, -180f, 0f, Space.World);
            }

            if(inputManager.PlayerBaseAttack()){
                animator.SetTrigger("RunningAttack");
            }

        }
        //Walking
        else   
        {
            playerSpeed = 1.0f;
            animator.ResetTrigger("Idle");
            animator.ResetTrigger("Running");
            animator.SetTrigger("Walking");

            //move.y += gravityValue * Time.deltaTime;

            move = transform.forward * move.z + transform.right * move.x;
            controller.Move(move * GameTime.deltaTime * playerSpeed);

            //To get proper oritation (Keep This)
            if (inputManager.getLeftMovement() && controller.transform.localRotation.eulerAngles.y == 0)
            {
                controller.transform.Rotate(0f, 180f, 0f, Space.World);
            }
            else if (!inputManager.getLeftMovement() && controller.transform.localRotation.eulerAngles.y == 180)
            {
                controller.transform.Rotate(0f, -180f, 0f, Space.World);
            }

        }
        

        //Conditonals for Jump movement and Animation (Not Working due to plane movement)
        /*
        if(-1 > 0){

            //playerVelocity.y = jumpHeight;

            playerVelocity.y += gravityValue * GameTime.deltaTime;
            controller.Move(playerVelocity * GameTime.deltaTime);

            animator.SetTrigger("Jumping");
        }
        */
        //Conditonals for Defend movement and Animation
        if(inputManager.PlayerDefended()){
            animator.SetTrigger("Defending");
        }

        //Conditonals for Base Attack action and Animation
        if(inputManager.PlayerBaseAttack() && !inputManager.PlayerRunning()){
            animator.SetTrigger("Attacking1");
        }

        //Conditonals for Attack 2 action and Animation
        if(inputManager.PlayerSecondAttack()){
            animator.SetTrigger("Attacking2");
        }

        //Conditonals for Attack 3 action and Animation
        if(inputManager.PlayerThirdAttack()){
            animator.SetTrigger("Attacking3");
        }

        if(inputManager.PlayerForthAttack()){
            animator.SetTrigger("Attacking4");
        }
    }


//WILL CHANGE LATER
    public void SetPosition(){
        transform.position = new Vector3(UnityEngine.Random.Range(-5,5), 0.8f, UnityEngine.Random.Range(-15, 7));
    }
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("D20")) {
            Physics.IgnoreCollision(other.collider, transform.GetComponent<Collider>());
        }
        if(other.gameObject.tag == "Tray"){
            gravityValue = 0;
        }else{
            gravityValue = -9.81f;
        }
    }
    
}



public static class GameTime{
    public static bool isPaused = false;
    public static float deltaTime {get {return isPaused ?0 : Time.deltaTime;}}
}

