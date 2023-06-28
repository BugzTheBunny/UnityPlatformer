using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
public class PlayerController : MonoBehaviour
{   
    // Components
    private Animator anim;
    private Rigidbody2D rb;

    [Header("- Move Info")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpForce = 15f;
    private bool canDoubleJump;
    private bool canMove = true;
    [SerializeField] Vector2 jumpDirection = new Vector2(5,15);

    [Header("- Collision Info")]
    // Wall
    [SerializeField] LayerMask whatIsWall;
    [SerializeField] float wallCheckDistance;
    private bool isOnWall;
    private bool canWallSlide;
    private bool isWallSliding;

    // Ground
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] float groundCheckDistance;
    private bool isGrounded;

    
    [Header("- Facing Direction")]
    private bool facingRight = true;
    private int facingDirection = 1;

    void Start()
    {
        SetupInitialComponents();
        SetupInitialSettings();
    }

    private void SetupInitialComponents()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void SetupInitialSettings()
    {
        // Freezez rotation / roll cause of Z axis.
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
         
    }
    // Update is called once per frame
    void Update()
    {
        AnimationController();
        FlipController();
        CollisionChecks();
        InputChecks();
        HandleMove();
    }

    private void AnimationController()
    {
        anim.SetBool("isMoving", GetXVelocityAxis() != 0);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isWallSliding", isWallSliding);

    }

    private float GetXVelocityAxis(){
        return Input.GetAxisRaw("Horizontal");
    }

    private void InputChecks() {

        if (Input.GetKeyDown(KeyCode.R)) {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.Space)){
            OnJump();
        }
    }

    private void WallJump()
    {
        canMove = false;
        rb.velocity = new Vector2(jumpDirection.x * -facingDirection, jumpDirection.y);
    }

    private void HandleMove()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(moveSpeed * GetXVelocityAxis(), rb.velocity.y);
        }
    }

    private void Jump(){
        rb.velocity = new Vector2(rb.velocity.x,jumpForce);
    }

    private void FlipController()
    {
        if (facingRight && rb.velocity.x < 0) {
            Flip();
        }else if (!facingRight && rb.velocity.x > 0) {
            Flip();
        }

    }

    private void Flip()
    {
        facingDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0,180,0);
    }


    private void OnJump(){
        if (isWallSliding)
        {
            WallJump();
        }else if (isGrounded)
        {
            Jump();
        }
        else if(canDoubleJump) {
            Jump();
            canDoubleJump = false;
        }

        canWallSlide = false;
    }

    private void CollisionChecks(){
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down,groundCheckDistance,whatIsGround);
        if (isGrounded)
        {
            canMove = true;
            canWallSlide = true;
            canDoubleJump = true;
        }
        CheckWallCollision();
    }

    private void CheckWallCollision()
    {
        // Checks if wall detected depending on the facing direction
        isOnWall = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, wallCheckDistance, whatIsWall);
        canWallSlide = CheckIfCanWallSlide();
        isWallSliding = canWallSlide;
        if (canWallSlide)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.1f);
        }
    }

    private bool CheckIfCanWallSlide()
    {
        if (Input.GetAxis("Vertical") < 0)
        {
            return false;
        }
        return isOnWall && rb.velocity.y < 0 ;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position,new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + wallCheckDistance * facingDirection, transform.position.y));
    }
}
