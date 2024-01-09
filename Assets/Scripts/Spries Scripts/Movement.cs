using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Vector2 movement;
    private Rigidbody2D rb;

    private void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

   private void onMovement(InputAction.CallbackContext value){
        movement = value.ReadValue<Vector2>();

   }

   private void FixedUpdate(){
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
   }
}
