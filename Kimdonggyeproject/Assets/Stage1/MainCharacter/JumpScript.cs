using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VariableJump : MonoBehaviour
{
    public float jumpForce = 5f;
    public float jumpDuration = 0.5f;
    public bool isJumping = false;
    public bool newJump = false;
    private float jumpStartTime;
    private Rigidbody rb;
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.1f;
    public bool isGrounded;
    public float extraGravity = 20f;
    private Animator animator;
    private PlayerMovementRB moveSC;

    void Start()
    {
        moveSC = GetComponent<PlayerMovementRB>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    public void HandleCollision(Collision collision)
    {
        isGrounded = true;
    }
    private void Update()
    {
        if (moveSC.canMove == false)
            return;
        if (isGrounded && Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            newJump = true;
            isGrounded = false;
            Jump();
        }else
            newJump = false;

        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }
    private void FixedUpdate(){
        if (isJumping && Input.GetKey(KeyCode.Space) && Time.time - jumpStartTime < jumpDuration)
        {
            JumpHigher();
        }
        if(rb.useGravity)
            rb.AddForce(Vector3.down * extraGravity, ForceMode.Acceleration);
    }
    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isJumping = true;
        jumpStartTime = Time.time;
    }

    private void JumpHigher()
    {
        rb.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse);
    }
}

