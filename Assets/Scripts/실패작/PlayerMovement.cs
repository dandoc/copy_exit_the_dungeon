using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 10f;
    public float jumpForce = 20f;

    public bool jump;
    public bool down;
    public bool isJumping = false;
    public bool isDowning = false;
    public bool isGround;
    public bool isFloor;

    private PlayerInput playerInput;
    public RayCastunder r;

    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;
    private BoxCollider2D playerCollider;


    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        r = GetComponent<RayCastunder>();
        //playerAnimator = GetComponent<Animator>();

    }

    private void Update()
    {
        jump = playerInput.jump;
        down = playerInput.down;
        Jump();
        Down();
        Move();
        Debug.Log("d_f"+r.down_floor);
        Debug.Log("d_g"+r.down_ground);
        Debug.Log("u_f"+r.up_floor);
        Debug.Log("u_g"+r.up_ground);
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
        isJumping = false;
        isDowning = false;
    }

    private void Jump()
    {
        if (jump)
        {
            playerCollider.enabled = false;
            if (!isJumping)
            {
                playerCollider.enabled = false;
                playerRigidbody.velocity = Vector2.zero;
                playerRigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);
                isJumping = true;
            }
        }
        if (isJumping)
            Landing();
        
    }

    private void Landing()
    {
        //for (int i = 0; i < r.undertags.Length; i++)
        //{
        //    if ((r.undertags[i] == "floor" || r.undertags[i] == "ground"))
        //    {
        //        playerCollider.enabled = true;
        //    }
        //}

        if (r.down_floor || r.down_ground)
            playerCollider.enabled = true;
    }

    private void Down()
    {
        if (down)
        {
            if (!isDowning)
            {
                //for (int j = 0; j < r.undertags.Length; j++)
                //{
                //    if (r.undertags[j] == "floor")
                //    {
                //        playerCollider.enabled = false;
                //        isDowning = true;
                //    }
                //}
                if (r.down_floor)
                {
                    playerCollider.enabled = false;
                    isDowning = true;
                }
            }
            else
            {
                //for (int j = 0; j < r.undertags.Length; j++)
                //{
                //    if (r.undertags[j] == "ground")
                //    {
                //        if (playerCollider.enabled == false && isDowning)
                //        {
                //            playerCollider.enabled = true;
                //            isDowning = false;
                //        }
                //    }
                //}
                if(r.down_ground)
                {
                    if(playerCollider.enabled == false && isDowning)
                    {
                        playerCollider.enabled = true;
                        isDowning = false;
                    }
                }
            }
        }
        else
        {
            //for (int j = 0; j < r.undertags.Length; j++)
            //{
            //    if (r.undertags[j] == "ground" || r.undertags[j] == "floor")
            //    {
            //        if (playerCollider.enabled == false && isDowning)
            //        {
            //            playerCollider.enabled = true;
            //            isDowning = false;
            //        }
            //    }
            //}

            if(r.down_ground || r.down_floor)
            {
                if(playerCollider.enabled == false && isDowning)
                {
                    playerCollider.enabled=true;
                    isDowning = false;
                }
            }

        }


    }
}
