using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
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
        
        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;
        controller.Move(move * GameTime.deltaTime * playerSpeed);
        playerVelocity.y += gravityValue * GameTime.deltaTime;
        controller.Move(playerVelocity * GameTime.deltaTime);

        if(math.abs(movement.x) > 0 || math.abs(movement.y) > 0){
            animator.SetTrigger("Walk");
        }else{
            animator.ResetTrigger("Walk");
            animator.SetTrigger("Idle");
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

