using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    //Movement
    [Header("Movement")]
    public float startWaitTime;
    public float addForceY;
    public float addForceX;
    public Transform groundDetection;
    private bool movingRight;
    private float waitTime;

    //Ground check
    [Header("Ground check")]
    public Vector2 groundCheckSize;
    public Transform groundCheck;
	public LayerMask whatIsGround;
    private bool isGrounded;

    //Components
    private Animator anim;
    private Rigidbody2D body;


    void Start()
    {
        movingRight = true;
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        waitTime = startWaitTime;
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, whatIsGround);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 6f);
        if(groundInfo.collider == false)
        {
            if(movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
                addForceX = -addForceX;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
                addForceX = -addForceX;
            }
        }
        if (waitTime <= 0 && isGrounded)
        {
            anim.SetTrigger("takeOff");
            body.velocity = new Vector2(addForceX, addForceY);
            waitTime = startWaitTime;
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
        if (isGrounded)
        {
            anim.SetBool("isJumping", false);
        }
        else if (!isGrounded)
        {
            anim.SetBool("isJumping", true);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(groundDetection.position, Vector2.down * 6f);
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
    }
   
}
