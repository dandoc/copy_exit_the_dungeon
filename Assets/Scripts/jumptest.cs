using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumptest : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;
    public float jumpPower = 10;
    public float jumpTimeLimit = 0.1f;
    public float jumpTimer = 0f;
    public bool isJumping = false;
    bool jump_down;
    bool jump;
    bool jump_up;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        jump_down = Input.GetButtonDown("Jump");
        jump = Input.GetButton("Jump");
        jump_up = Input.GetButtonUp("Jump");
        Jump();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //jump();
    }

    void Jump()
    {
        if(jump_down && !isJumping)
        {
            isJumping = true;
            jumpTimer = 0;
        }

        if(!jump || jumpTimer >= jumpTimeLimit)
        {
            isJumping = false;

            return;
        }

        playerRigidbody.velocity = Vector2.zero;
        playerRigidbody.AddForce(Vector2.up * jumpPower * ((jumpTimer * 20)), ForceMode2D.Impulse);

        jumpTimer += Time.deltaTime;
    }


}
    