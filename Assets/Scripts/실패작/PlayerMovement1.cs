using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 10f;
    public float jumpForce = 20f;

    public bool jump;
    public bool down;
    public bool isJumping = false;
    public bool isDowning = false;
    public bool onair = false;

    private PlayerInput1 playerInput;
    public RayCastunder1 r;

    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;
    private BoxCollider2D playerCollider;


    private void Start()
    {
        playerInput = GetComponent<PlayerInput1>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        r = GetComponent<RayCastunder1>();
        //playerAnimator = GetComponent<Animator>();

    }

    private void Update()
    {
        Move();
        Jump();
        Down();

    }

    private void FixedUpdate()
    {
        
    }
    
    private void Move()
    {
        if (playerInput.move != 0)
        {
            Vector2 moveDistance = playerInput.move * Vector2.right * moveSpeed * Time.deltaTime;
            playerRigidbody.position += moveDistance;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(r.down_floor || r.down_ground)
            isJumping = false;
    }

    private void Jump()
    {
        if(playerInput.jump)
        {
            if(!isJumping)
            {
                playerRigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                isJumping = true;
            }
        }
        if(isJumping)
            Landing_j();
    }

    private void Landing_j()
    {
        
        if(r.up_floor)
        {
            playerCollider.enabled = false;
        }

        if (!r.down_ground && !r.down_floor)
        {
            playerCollider.enabled = false;
        }

        else if ((r.down_ground || r.down_floor) && !isDowning)
        {
            playerCollider.enabled = true;
        }
    }

    private void Down()
    {
        
        if(isDowning)
        {
            if(r.down_ground)
            {
                playerCollider.enabled = true;
                isDowning = false;
            }
            else if(r.down_floor && !playerInput.down)
            {
                playerCollider.enabled = true;
                isDowning = false;
            }
        }
        else
        {
            if (playerInput.down)
            {
                if (!isDowning && r.down_ground)
                    return;
                isDowning = true;
                playerCollider.enabled = false;
            }
            else if (!playerInput.down)
            {
                isDowning = false;
            }
        }
        
    }
}
