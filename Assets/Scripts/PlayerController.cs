using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 3.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private InputManager inputManager;
    private Transform cameraTransform;
    private GameObject sprite;
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
       
       //Conditonals for walking to try to keep the movement and the animations in the same place
        if(math.abs(movement.x) > 0 || math.abs(movement.y) > 0){

            animator.ResetTrigger("Idle");
            animator.SetTrigger("Walk");

            move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
            controller.Move(move * GameTime.deltaTime * playerSpeed);
            
            move.y = 0f;
            
            //To get proper oritation? of the character based on where they are walking
            if(movement.x < 0 && controller.transform.localRotation.eulerAngles.y == 0){
                controller.transform.Rotate(0f, 180f, 0f, Space.Self);
            }else if(movement.x > 0 && controller.transform.localRotation.eulerAngles.y == 180){
                controller.transform.Rotate(0f, -180f, 0f, Space.Self);
            }

        }else{
            animator.ResetTrigger("Walk");
            animator.SetTrigger("Idle");
        }

        //Conditonals for Jump movement and Animation (Not Working due to plane movement)
        if(-1 > 0){

            //playerVelocity.y = jumpHeight;

            playerVelocity.y += gravityValue * GameTime.deltaTime;
            controller.Move(playerVelocity * GameTime.deltaTime);

            animator.SetTrigger("Jumping");
        }
    }


    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Die") {
            Physics.IgnoreCollision(other.collider, transform.GetComponent<Collider>());
        }
    }
}



public static class GameTime{
    public static bool isPaused = false;
    public static float deltaTime {get {return isPaused ?0 : Time.deltaTime;}}
}

