using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// IS �ø��� ���� ��Ű�� �� ����
/// </summary>

public class PlayerMovement3 : MonoBehaviour
{
    public float moveSpeed = 5f;    // �̵� ���ǵ�
    public float dashForce = 30f;   // �뽬 ���ǵ� Ȥ�� ��
    public float jumpForce = 20f;   // ���� ��

    public float jumpTimeLimit = 0.2f; // ���� �ִ� �ð�(���� Ű�� �Է¹޴� �ִ�, �ö󰡰��ϴ� �ð�)
    private float jumpTimer; // ���� Ű�� ������ �ִ� �ð�

    public bool canMove = true;
    public bool canJump = true; // ������ ������ �����ΰ�
    public bool canDown = true;
    public bool canDash = true;
    public bool isMoving = false;
    public bool isJumping = false;
    public bool isUp = false; // ���� ���ΰ�(���� ���� �������� �ִ°�)
    public bool isDashing = false;
    public bool isFalling = false; //(==isDowning) �������� �ִ°�

    public float mass = 2f;
    public float drag = 4f;
    public float gravity = 1.5f;

    GameObject floor;

    private PlayerInput2 playerInput; //�÷��̾ ���� �Է��� ���

    private Rigidbody2D playerRigidbody; // �÷��̾��� ������ٵ�
    private Animator playerAnimator; // �÷��̾��� �ִϸ���F��(���� ����)
    private BoxCollider2D playerCollider; // �÷��̾��� �ö��̴�
    private PlayerView playerView;

    private void Start()
    {
        //�ʿ��Ѱ� �ҷ��� ����
        playerInput = GetComponent<PlayerInput2>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        playerView = GetComponent<PlayerView>();
        //playerAnimator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        Move(); // �¿� �̵�
        Jump(); // ����
        IsFalling(); // �������� �ִ���, ���� ���� ����
        //Down(); // �Ʒ��� ����
        Dash();
        View();

    }

    private void Move() //canMove, isMoving (isFalling)
        // Down�� ��ħ.
    {
        if (playerInput.move != 0 && canMove)
        {
            Vector2 moveDistance = playerInput.move * Vector2.right * moveSpeed * Time.deltaTime;
            playerRigidbody.position += moveDistance;
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
       // Down ����
        if (playerInput.down && canDown && !isDashing) // �Ʒ� ���� ��ư �Է� �� �϶�,
        {
            floor = GameObject.FindGameObjectsWithTag("floor")[0]; // tag�� floor(����)�� ���� ������Ʈ ���� (���� �ļ��� ���߿� ���ľߵ� �������� ��! ���ľߵ�!)
            playerRigidbody.mass = mass * 2;
            playerRigidbody.drag = drag / 40;
            playerRigidbody.gravityScale = gravity * (4 / 3);   //�� ���� �������� �ϱ� ���ؼ� ������ٵ� �� ����
            floor.layer = 9; //void : 9 �Ʊ� �����ص� ������Ʈ�� ���̾� ���� ����
        }
        if (playerInput.down_up) // �Ʒ� ���� ��ư �Է��� �ƴ� ��,
        {
            floor = GameObject.FindGameObjectsWithTag("floor")[0];
            init_rigid();    // ���� ������ٵ� ������ ����
            floor.layer = 7; // ���� ���̾� ������ ����
        }
        //�÷��̾ platform effector�� ����ؼ� Ư�� ���̾ �浹�ϰ� ���� �ص�

    }

    private void Jump() //isUp, canJump, isFalling
        // 3������ if������ ���� ������ 1. ���� �Է� �ޱ� 2. ���� ���� 3. ���� ����
    {
        //platform effector�� ����� ����ؼ� floor�� �Ʒ����� ���� ���� ��, �浹���� �ʰ� ���� �ص�.
        if (playerInput.jump_down && !isFalling && canJump) //���� Ű�� ��������, �������� ���°� �ƴϰ�, ������ ������ ����
        {
            canJump = false; // ���� ���� X 
            isUp = true; // ���� ��(���� �ö󰡱� ����)
            jumpTimer = 0; // ���� Ÿ�̸� �ʱ�ȭ

            //3�� if���� �۵� �� �� �ְ� �غ��ϴ� �ܰ�
        }

        if (playerInput.jump_up) // ����Ű�� �� ��,
        {
            isUp = false; // ���� ����(�ö󰡴� �� ��)
            return;

            // 3�� if���� Ÿ�̸ӽð��� �Ѿ�� ������ ���� �ǰ� �ϴ� �ڵ尡 ������,
            // Ÿ�̸� �ð� �ȿ� ����Ű�� ����, ������ ���� �ǰ��ϴ� �ڵ尡 ����.
            // 3�� if�� �ȿ� �ִ� Ÿ�̸� �ʰ��� ���� ���� ���� if���� 2�� if���� ������,
            // Ÿ�̸� �ð� �ȿ��� ���� Ű�� ���� ��, isUp�� false�� ���� �ʴ´�.
            // �׷��� ���� ���δ�, �� �۵��� ����
        }   

        if ((playerInput.jump && !isFalling && isUp)) // ���� Ű�� ������ ���� �� + �������� ���� �ƴҶ�, ���� ���϶�(�ö󰡱� ����)
        {
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(Vector2.up * jumpForce * ((jumpTimer * 1.2f) + 1f), ForceMode2D.Impulse); // �̰� ���� ��... �ٵ� ������ �� �ʿ���(ù ���� �� ���� ���� �� �־����)
                                                                                                               // project manager���� physics2d ���� + rigidbody2d�� linear drag ���� +
                                                                                                               // Mess ��� ���� �ذ��鼭 ���� ���� ã�ƾ���.

            jumpTimer += Time.deltaTime; // Ÿ�̸� �۵� -> Time.deltaTime == 1/������ ex) 1/30, 1/60...
            if(jumpTimer >= jumpTimeLimit) // ���� ���� �ð��� ������,
            {
                isUp = false; // ���� ����
                return;
            }
        }
        // canjump�� jump�� �ȿ��� true�� �ٲ��ָ�, ��� ������ �Ǽ� IsFailling �޼��忡�� true�� �ǰ� ����� ��.
    }

    /// <summary>
    /// �뽬 ����:
    /// 1. ��Ŭ�� ��, �ٶ󺸴� �������� �뽬
    /// 2. �̵�Ű + ��Ŭ�� ��, �̵��ϴ� �������� �뽬 (�ٶ󺸴� ���⵵ ��ȯ �ϰԴ� �ƴ�, �ϸ� Ŀ�ø� ��������)
    /// 3. �����ÿ��� 1,2 ���� ����
    /// 4. �뽬 ���߿� �ƹ��� ��ȣ�ۿ� X (���Ŀ� �߰��� �� �߻絵 ������)
    /// </summary>
    private void Dash()
    {
        if ((playerInput.dash && canDash))
        {

            init_rigid();

            isDashing = true;
            float dash_dir = 0;
            Debug.Log("dash");
            if(playerInput.move != 0)
                dash_dir = playerInput.move;
            else
                dash_dir = playerView.playerScaleX;
            StartCoroutine(Dashing(dash_dir));
        }
        
    }

    IEnumerator Dashing(float dash_dir)
    {
        canMove = false;
        canJump = false;
        canDash = false;
        canDown = false;
        playerRigidbody.velocity = Vector2.zero;
        playerRigidbody.gravityScale = 0;
        playerRigidbody.AddForce(dash_dir * Vector2.right * dashForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.3f);
        
        playerRigidbody.gravityScale = gravity;
        playerRigidbody.velocity = Vector2.zero;
        isDashing = false;
        
        canMove = true;
        canDown = true;
    }

    private void IsFalling() // isFalling, canJump
    // ó������ �������� ���¸� �����Ϸ��� ������, ������ ������ ���¸� �Ǻ��ϴ� �ڵ嵵 �̿� ����ϱ⿡ canJump�� ���õ� �ڵ嵵 �Է� ��.
    {
        if (playerRigidbody.velocity.y < 0f) // �÷��̾��� y�� �̵� �ӵ��� 0�̸�, �Ʒ��� �������� ���� ��,
        {
            isFalling = true;
        }
        else if (playerRigidbody.velocity.y > 0f) // �÷��̾��� y�� �̵� �ӵ��� 0�ʰ�, ���� �ö󰡰� ���� ��,
        {
            isFalling = false;
        }
        else if (playerRigidbody.velocity.y == 0f) // �÷��̾��� y�� �̵� �ӵ��� 0, ���� �� ����� �̰ų�, ���� ���̰ų�
        {
            isFalling = false;
            //canJump = true;         //*���� �� �κ��� �� canjump�� ���̸� �� ���� ������ �� �Ǵ� ���װ� ����� �׷��� ���� �޼ҵ忡 �̸� �ذ��� ������ �߰� ��~~ �׵��� ����~~ �̰� ������ �� ����
        }
    }

    

    private void View()
    {
        this.transform.localScale = new Vector3(playerView.playerScaleX, 1, 1);
    }

    private void init_rigid()
    {
        playerRigidbody.mass = mass;
        playerRigidbody.drag = drag;
        playerRigidbody.gravityScale = gravity;
    }


    //���⼭ ����...
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isFalling)
        {
            isFalling = false; //�̰� ��� �Ǵµ� �̰� �����ϱ� �� ���� ������.
            canJump = true;
        }
        if(!isDashing)
        {
            canDash = true;
            canJump = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isFalling)
        {
            isFalling = false; //�̰� ��� �Ǵµ� �̰� �����ϱ� �� ���� ������.
            canJump = true;
        }
        if (!isDashing)
        {
            canDash = true;
            canJump = true;
        }
    }
    //...���� ������ �÷��̾� ���� �������� ��, �ڵ尡 �� �۵����� ���� ���� ����� �ڵ� => ��� �ƴ� ��� �־�� �� ���ư�
}
