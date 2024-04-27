using Unity.Mathematics;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using System;
using TMPro;
using Mirror.Examples.Basic;

public class PlayerController : NetworkBehaviour
{
    [SerializeField]
    private float playerSpeed;
    private float oldSpeed;
    [SerializeField]
    private float sprintSpeed = 5f;
    [SerializeField]
    private float speedLimit = 4f;
    private bool isLeft = false;
    private bool isRight = false;

    //private float yVelocity = 0f;
    private CharacterController controller;
    private Rigidbody rb;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private InputManager inputManager;
    private Transform cameraTransform;
    public GameObject gameplayCam;
    public GameObject uiCam;
    public GameObject cameras;
    //public Animator animator;
    public NetworkAnimator n_animator;
    // Start is called before the first frame update

    public GameObject PlayerModel;
    public HotKeys hotKeyController; 
    public DMCameraController dmControls;
    public bool buildMode = false;
    private GameObject buildMenu;
    public bool isActive = false;

    public override void OnStartAuthority()
    {
        cameras.SetActive(true);
    }
    void Start()
    {
        controller = GetComponent<CharacterController>(); //DEPRECATED

        rb = GetComponent<Rigidbody>(); //Grab rigidbody component to control velocity
        rb.useGravity = false; //Make gravity false until we enter game
        oldSpeed = playerSpeed; //Set speed value to switch to when not sprinting

        n_animator = GetComponent<NetworkAnimator>();

        inputManager = InputManager.Instance; //DEPRECATED

        cameraTransform = Camera.main.transform;

        //The player loads in during lobby, hide model and cameras until all players enter game
        PlayerModel.SetActive(false);

        hotKeyController = GameObject.Find("Building Hotkeys").GetComponent<HotKeys>();
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
                
                gameplayCam.GetComponent<DiceController>().displayTextOBJ = GameObject.Find("RollText").GetComponent<TextMeshProUGUI>();
                rb.useGravity = true;
                //GetComponentInChildren<SpriteBillboard>().GameplayCamera = gameplayCam.GetComponent<Camera>();
                Cursor.lockState = CursorLockMode.Locked;
            }
            if(Input.GetKeyDown(KeyCode.V)){
                buildMode = true;
            }
            if(buildMode == true){ //SWITCH TO DM_CAM CONTROLS HERE
                buildMenu.gameObject.SetActive(!isActive);
                buildMenu.GetComponent<SidebarUI>().enabled = !isActive;
                buildMenu.GetComponent<BottomBarUI>().enabled = !isActive;
            }else{
                UpdatedMovement();
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
        controller.Move(playerVelocity * GameTime.deltaTime);

        //Conditonals for movement to try to keep the movement and the animations in the same place

        //Idle
        if(math.abs(movement.x) == 0 && math.abs(movement.y) == 0)
        {
            IdleAnim();
            
        }
        //Sprinting
        else if ((math.abs(movement.x) > 0 || (math.abs(movement.y) > 0))  && inputManager.PlayerRunning())
        {
            playerSpeed *= 2;
            if(playerSpeed >= speedLimit){
                playerSpeed = speedLimit;
            }
            
            SprintAnim();
            
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
                n_animator.SetTrigger("RunningAttack");
            }

        }
        //Walking
        else   
        {
            playerSpeed = 1.0f;
            WalkAnim();

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

            n_animator.SetTrigger("Jumping");
        }
        */
        //Conditonals for Defend movement and Animation
        if(inputManager.PlayerDefended()){
            n_animator.SetTrigger("Defending");
        }

        //Conditonals for Base Attack action and Animation
        if(inputManager.PlayerBaseAttack() && !inputManager.PlayerRunning()){
            n_animator.SetTrigger("Attacking1");
        }

        //Conditonals for Attack 2 action and Animation
        if(inputManager.PlayerSecondAttack()){
            n_animator.SetTrigger("Attacking2");
        }

        //Conditonals for Attack 3 action and Animation
        if(inputManager.PlayerThirdAttack()){
            n_animator.SetTrigger("Attacking3");
        }

        if(inputManager.PlayerForthAttack()){
            n_animator.SetTrigger("Attacking4");
        }
    }

    public void IdleAnim(){
        if(!isLocalPlayer){
            return;
        }
        n_animator.SetTrigger("Idle");
        n_animator.ResetTrigger("Walking");
        n_animator.ResetTrigger("Running");
    }

    public void WalkAnim(){
        if(!isLocalPlayer){
            return;
        }
        n_animator.ResetTrigger("Idle");
        n_animator.ResetTrigger("Running");
        n_animator.SetTrigger("Walking");
    }

    public void SprintAnim(){
        if(!isLocalPlayer){
            return;
        }
        n_animator.ResetTrigger("Idle");
        n_animator.ResetTrigger("Walking");
        n_animator.SetTrigger("Running");
    }

    public void UpdatedMovement(){
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 forward = gameplayCam.transform.forward;
        Vector3 right = gameplayCam.transform.right;
        //forward.y = 0f;
        //right.y = 0f;
        forward.Normalize();
        right.Normalize();
        bool sprinting;
        bool walking;
        bool idle;

        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        //moveDirection.y = 0f;
        moveDirection.Normalize();


        if((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && (Math.Abs(horizontalInput) > 0 || Math.Abs(verticalInput) > 0)){
        // Manage animation state
        // ****************
            sprinting = true;
            walking = false;
            idle = false;
        // ****************
            

        }else if(Math.Abs(horizontalInput) > 0 || Math.Abs(verticalInput) > 0){
        // Manage animation state
        // ****************
            sprinting = false;
            walking = true;
            idle = false;
        // ****************
            

        }else{
        // Manage animation state
        // ****************
            sprinting = false;
            walking = false;
            idle = true;
        // ****************
        }

        //ANIMATE SPRITE BEFORE MOVEMENT FRAME
        if(walking){ //WALKING
            n_animator.ResetTrigger("Idle");
            n_animator.ResetTrigger("Running");
            n_animator.SetTrigger("Walking");
            playerSpeed = oldSpeed; //Apply speed change
        }
        if(sprinting){ //SPRINTING
            n_animator.ResetTrigger("Idle");
            n_animator.ResetTrigger("Walking");
            n_animator.SetTrigger("Running");
            playerSpeed = sprintSpeed; //Apply speed change
        }
        if(idle){ //IDLE
            n_animator.SetTrigger("Idle");
            n_animator.ResetTrigger("Walking");
            n_animator.ResetTrigger("Running");
        }

        rb.velocity = new Vector3(moveDirection.x * playerSpeed, rb.velocity.y, moveDirection.z * playerSpeed); //Move


        
    }

//WILL CHANGE LATER
    public void SetPosition(){
        if(GetComponent<PlayerObjectController>().ConnectionID == 0){ //If this player is the host spawn here
            transform.position = new Vector3(50, 30f, 30);
        }else{ //If client spawn here
            transform.position = new Vector3(50, 30f, 30);
        }
        
    }
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("D20")) {
            Physics.IgnoreCollision(other.collider, transform.GetComponent<Collider>());
        }
    }
    
}



public static class GameTime{
    public static bool isPaused = false;
    public static float deltaTime {get {return isPaused ?0 : Time.deltaTime;}}
}

