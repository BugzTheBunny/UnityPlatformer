using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 15f;
    [SerializeField] Rigidbody2D rb;


    // Jumps
    private bool canDoubleJump;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] float groundCheckDistance;
    [SerializeField] bool isGrounded;

    void Start()
    {
        // Freezez rotation / roll cause of Z axis.
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {   
        CollisionChecks();
        InputChecks();
        HandleMove();
    }

    private void InputChecks() {
        if(Input.GetKeyDown(KeyCode.Space)){
            HandleJump();
        }
    }

    private void HandleMove()
    {
        float directionInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveSpeed * directionInput, rb.velocity.y);
    }

    private void Jump(){
        rb.velocity = new Vector2(rb.velocity.x,jumpForce);
    }


    private void HandleJump(){
        if (isGrounded){
            Jump();
        }else if(canDoubleJump) {
            Jump();
            canDoubleJump = false;
        }
    }

    private void CollisionChecks(){
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down,groundCheckDistance,whatIsGround);
        if (isGrounded){
            canDoubleJump = true;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position,new Vector3(transform.position.x, transform.position.y - groundCheckDistance)); 
    }
}
