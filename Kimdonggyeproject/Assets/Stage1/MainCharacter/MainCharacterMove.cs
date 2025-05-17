using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Threading;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementRB : MonoBehaviour
{
    public float moveSpeed = 5f;
    public List<GameObject> saveTargets;
    public Camera cam;
    public CameraFollow camerasc;
    private Rigidbody rb;
    Animator animator;
    public GameObject target;
    private Vector3 movement;
    public bool canMove = true;
    public int newMove = 0;
    public Vector3 savepoint;
    public GameObject hand;
    VariableJump jumpsc;
    int dir = 0;
    private bool checking = true;
    void Start()
    {
        savepoint = transform.position;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        animator = target.GetComponent<Animator>();
        jumpsc = GetComponent<VariableJump>();
        // Y축 회전만 고정 (필요에 따라 조절 가능)
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        camerasc = cam.GetComponent<CameraFollow>();
    }
    public void HandleCollision(Collision collision)
    {
        if (collision.gameObject.layer == 8 && checking == true)
        { //wrong tile
            if (collision.gameObject.CompareTag("Alpha"))
            {
                return;
            }
            canMove = false;
            checking = false;
            rb.AddForce(Vector3.up * 12, ForceMode.Impulse);
            animator.SetInteger("Event", 1);
            //Thread.Sleep(1000);
            GetComponent<Collider>().enabled = false;
            Invoke("Dead", 1.6f);
        }
        else if (collision.gameObject.layer == 9 && checking == true) //background layer
        {
            canMove = false;
            checking = false;
            Debug.Log("추락 판정");
            rb.AddForce(Vector3.up * 22, ForceMode.Impulse);
            animator.SetInteger("Event", 1);
            GetComponent<Collider>().enabled = false;
            Invoke("Dead", 2.5f);
        }
        else if (collision.gameObject.layer == 10 && savepoint.x != collision.gameObject.transform.position.x && savepoint.z != collision.gameObject.transform.position.z) //savepoint layer
        {
            Debug.Log("세이브 포인트 설정");
            savepoint = collision.gameObject.transform.position;
            savepoint.y = 3;
        }
        else if (collision.gameObject.CompareTag("END") && checking == true) //종료 조건
        {
            checking = false;
            canMove = false;
            Debug.Log("종료 판정");
            GetComponent<Collider>().enabled = false;
            rb.useGravity = false;
            Invoke("END", 2.5f);
        }
    }
    void Update()
    {
        float moveX = 0f;
        float moveZ = 0f;
        if (canMove)
        {
            if (Input.GetKey(KeyCode.W))
            {
                moveZ = 1f;
                dir = 0;
                if (newMove == 0)
                    newMove = 1;
                else
                    newMove = 2;
            }
            if (Input.GetKey(KeyCode.S))
            {
                moveZ = -1f;
                dir = 1;
                if (newMove == 0)
                    newMove = 1;
                else
                    newMove = 2;
            }
            if (Input.GetKey(KeyCode.A))
            {
                moveX = -1f;
                dir = 2;
                if (newMove == 0)
                    newMove = 1;
                else
                    newMove = 2;
            }
            if (Input.GetKey(KeyCode.D))
            {
                moveX = 1f;
                dir = 3;
                if (newMove == 0)
                    newMove = 1;
                else
                    newMove = 2;
            }
            if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S))
                animator.SetBool("Moving", false);
            else
                animator.SetBool("Moving", true);
        }
        // ResetAllTriggers();
        // if (jumpsc.isJumping && jumpsc.isGrounded == false){
        //     if (dir == 0)
        //         animator.SetTrigger("Walk-U");
        //     else if (dir == 1)
        //         animator.SetTrigger("Walk-D");
        //     else if (dir == 2)
        //         animator.SetTrigger("Jump-L");
        //     else if (dir == 3)
        //         animator.SetTrigger("Jump-R");
        //     else
        //         animator.SetTrigger("IDLE");
        // }
        // else {
        //     if (dir == 0)
        //         animator.SetTrigger("Walk-U");
        //     else if (dir == 1)
        //         animator.SetTrigger("Walk-D");
        //     else if (dir == 2)
        //         animator.SetTrigger("Walk-L");
        //     else if (dir == 3)
        //         animator.SetTrigger("Walk-R");
        //     else
        //         animator.SetTrigger("IDLE");
        // }

        if (!jumpsc.isGrounded)
            animator.SetBool("Jumping", true);
        else
            animator.SetBool("Jumping", false);

        animator.SetInteger("Dir", dir);
        movement = new Vector3(moveX, 0f, moveZ).normalized;
        // 기존 y축 속도 보존 (중력 영향)
        Vector3 currentVelocity = rb.linearVelocity;
        rb.linearVelocity = new Vector3(movement.x * moveSpeed, currentVelocity.y, movement.z * moveSpeed);
    }
    void Dead() //사망 판정
    {
        Respawn();
    }
    void Respawn()
    {
        VariableJump Jump = GetComponent<VariableJump>();
        Jump.isJumping = false;
        
        animator.SetInteger("Event", 0);
        
        GetComponent<Collider>().enabled = true;
        rb.useGravity = true;
        CallRestoreOnAllPlatforms();
        transform.position = savepoint;
        checking = true;
        canMove = true;
        return;
    }
    void CallRestoreOnAllPlatforms()
    {
        DisappearingPlatform[] platforms = Resources.FindObjectsOfTypeAll<DisappearingPlatform>();
        foreach (DisappearingPlatform p in platforms)
        {
            p.RestorePlatform();
        }
    }
    void END()
    {
        //종료 후 실행시킬 코드드
    }
}

