using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 1.0f;
    [SerializeField]
    private float speedLimit = 4f;

    private float jumpHeight = 3.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    private float yVelocity = 0f;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private InputManager inputManager;
    private Transform cameraTransform;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;

        //Need this to trigger the animations
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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

            if (movement.x < 0 && controller.transform.localRotation.eulerAngles.y == 0)
            {
                controller.transform.Rotate(0f, 180f, 0f, Space.World);
            }
            else if (movement.x > 0 && controller.transform.localRotation.eulerAngles.y == 180)
            {
                controller.transform.Rotate(0f, -180f, 0f, Space.World);
            }

            if(inputManager.PlayerBaseAttack()){
                animator.SetTrigger("RunningAttack");
            }

        }
        //Walking
        else if (math.abs(movement.x) > 0 || math.abs(movement.y) > 0)  
        {
            playerSpeed = 1.0f;
            animator.ResetTrigger("Idle");
            animator.ResetTrigger("Running");
            animator.SetTrigger("Walking");

            //move.y += gravityValue * Time.deltaTime;

            move = transform.forward * move.z + transform.right * move.x;
            controller.Move(move * GameTime.deltaTime * playerSpeed);

            //To get proper oritation? of the character based on where they are Walking
            if (movement.x < 0 && controller.transform.localRotation.eulerAngles.y == 0)
            {
                controller.transform.Rotate(0f, 180f, 0f, Space.World);
            }
            else if (movement.x > 0 && controller.transform.localRotation.eulerAngles.y == 180)
            {
                controller.transform.Rotate(0f, -180f, 0f, Space.World);
            }

        }
        

        //Conditonals for Jump movement and Animation (Not Working due to plane movement)
        if(-1 > 0){

            //playerVelocity.y = jumpHeight;

            playerVelocity.y += gravityValue * GameTime.deltaTime;
            controller.Move(playerVelocity * GameTime.deltaTime);

            animator.SetTrigger("Jumping");
        }

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


    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Die") {
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

