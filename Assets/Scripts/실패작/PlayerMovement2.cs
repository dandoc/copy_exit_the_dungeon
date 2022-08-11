using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public float moveSpeed = 5f;    // �̵� ���ǵ�
    public float dashForce = 30f;   // �뽬 ���ǵ� Ȥ�� ��
    public float jumpForce = 20f;   // ���� ��

    public float jumpTimeLimit = 0.2f; // ���� �ִ� �ð�(���� Ű�� �Է¹޴� �ִ�, �ö󰡰��ϴ� �ð�)
    private float jumpTimer; // ���� Ű�� ������ �ִ� �ð�

    public bool jump; // �÷��̾ ���� Ű�� �����°�
    public bool canJump = true; // ������ ������ �����ΰ�
    public bool isJumping = false; // ���� ���ΰ�(���� ���� �������� �ִ°�)
    public bool isFalling = false; // �������� �ִ°�


    public float mass = 2f;
    public float drag = 4f;
    public float gravity = 1.5f;


    private PlayerInput2 playerInput; //�÷��̾ ���� �Է��� ���

    private Rigidbody2D playerRigidbody; // �÷��̾��� ������ٵ�
    private Animator playerAnimator; // �÷��̾��� �ִϸ�����(���� ����)
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

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        Move(); // �¿� �̵�
        Jump(); // ����
        IsFalling(); // �������� �ִ���, ���� ���� ����
        Down(); // �Ʒ��� ����
        Dash();
        View();

    }
    
    private void View()
    {
        this.transform.localScale = new Vector3(playerView.playerScaleX, 1, 1);
    }

    private void Move()
    {
        if (playerInput.move != 0)
        {
            Vector2 moveDistance = playerInput.move * Vector2.right * moveSpeed * Time.deltaTime;
            playerRigidbody.position += moveDistance;
        }
        
    }

    private void IsFalling() // ó������ �������� ���¸� �����Ϸ��� ������, ������ ������ ���¸� �Ǻ��ϴ� �ڵ嵵 �̿� ����ϱ⿡ canJump�� ���õ� �ڵ嵵 �Է� ��.
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
            canJump = true;         //*���� �� �κ��� �� canjump�� ���̸� �� ���� ������ �� �Ǵ� ���װ� ����� �׷��� ���� �޼ҵ忡 �̸� �ذ��� ������ �߰�
        }
    }

    private void Jump() // 3������ if������ ���� ������ 1. ���� �Է� �ޱ� 2. ���� ���� 3. ���� ����
    {
        //platform effector�� ����� ����ؼ� floor�� �Ʒ����� ���� ���� ��, �浹���� �ʰ� ���� �ص�.
        if (playerInput.jump_down && !isFalling && canJump) //���� Ű�� ��������, �������� ���°� �ƴϰ�, ������ ������ ����
        {
            canJump = false; // ���� ���� X 
            isJumping = true; // ���� ��(���� �ö󰡱� ����)
            jumpTimer = 0; // ���� Ÿ�̸� �ʱ�ȭ

            //3�� if���� �۵� �� �� �ְ� �غ��ϴ� �ܰ�
        }

        if (playerInput.jump_up) // ����Ű�� �� ��,
        {
            isJumping = false; // ���� ����(�ö󰡴� �� ��)
            return;

            // 3�� if���� Ÿ�̸ӽð��� �Ѿ�� ������ ���� �ǰ� �ϴ� �ڵ尡 ������,
            // Ÿ�̸� �ð� �ȿ� ����Ű�� ����, ������ ���� �ǰ��ϴ� �ڵ尡 ����.
            // 3�� if�� �ȿ� �ִ� Ÿ�̸� �ʰ��� ���� ���� ���� if���� 2�� if���� ������,
            // Ÿ�̸� �ð� �ȿ��� ���� Ű�� ���� ��, isJumping�� false�� ���� �ʴ´�.
            // �׷��� ���� ���δ�, �� �۵��� ����
        }   

        if ((playerInput.jump && !isFalling && isJumping)) // ���� Ű�� ������ ���� �� + �������� ���� �ƴҶ�, ���� ���϶�(�ö󰡱� ����)
        {
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(Vector2.up * jumpForce * ((jumpTimer * 1.2f) + 1f), ForceMode2D.Impulse); // �̰� ���� ��... �ٵ� ������ �� �ʿ���(ù ���� �� ���� ���� �� �־����)
                                                                                                               // project manager���� physics2d ���� + rigidbody2d�� linear drag ���� +
                                                                                                               // Mess ��� ���� �ذ��鼭 ���� ���� ã�ƾ���.

            jumpTimer += Time.deltaTime; // Ÿ�̸� �۵� -> Time.deltaTime == 1/������ ex) 1/30, 1/60...
            if(jumpTimer >= jumpTimeLimit) // ���� ���� �ð��� ������,
            {
                isJumping = false; // ���� ���Ṯ
                return;
            }
        }
        // canjump�� jump�� �ȿ��� true�� �ٲ��ָ�, ��� ������ �Ǽ� IsFailling �޼��忡�� true�� �ǰ� ����� ��.
    }
    
    private void Down() // �Ʒ� ����
    {
        GameObject obj = GameObject.FindGameObjectsWithTag("floor")[0]; // tag�� floor(����)�� ���� ������Ʈ ���� (���� �ļ��� ���߿� ���ľߵ� �������� ��! ���ľߵ�!)
        if (playerInput.down) // �Ʒ� ���� ��ư �Է� �� �϶�,
        {
            playerRigidbody.mass = mass * 2;           
            playerRigidbody.drag = drag / 40;
            playerRigidbody.gravityScale = gravity*(4/3);   //�� ���� �������� �ϱ� ���ؼ� ������ٵ� �� ����
            obj.layer = 9; //void : 9 �Ʊ� �����ص� ������Ʈ�� ���̾� ���� ����
        }
        else if(playerInput.down_up) // �Ʒ� ���� ��ư �Է��� �ƴ� ��,
        {
            playerRigidbody.mass = mass;
            playerRigidbody.drag = drag;
            playerRigidbody.gravityScale = gravity;    // ���� ������ٵ� ������ ����
            obj.layer = 7; // ���� ���̾� ������ ����
        }
        //�÷��̾ platform effector�� ����ؼ� Ư�� ���̾ �浹�ϰ� ���� �ص�
    }

    private void Dash()
    {
        if (playerInput.dash)
        {
            Debug.Log("dash");
            StartCoroutine(Dashing());
        }
    }

    IEnumerator Dashing()
    {
        playerRigidbody.velocity = Vector2.zero;
        playerRigidbody.gravityScale = 0;
        playerRigidbody.AddForce(Vector2.right*dashForce);
        yield return new WaitForSeconds(0.3f);
        playerRigidbody.gravityScale = gravity;
        playerRigidbody.velocity = Vector2.zero;

    }

    //���⼭ ����...
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isFalling)
        {
            isFalling = false;
            canJump = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isFalling)
        {
            isFalling = false;
            canJump = true;
        }
    }
    //...���� ������ �÷��̾� ���� �������� ��, �ڵ尡 �� �۵����� ���� ���� ����� �ڵ�


}
