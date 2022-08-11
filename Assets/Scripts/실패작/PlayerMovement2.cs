using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public float moveSpeed = 5f;    // 이동 스피드
    public float dashForce = 30f;   // 대쉬 스피드 혹은 힘
    public float jumpForce = 20f;   // 점프 힘

    public float jumpTimeLimit = 0.2f; // 점프 최대 시간(점프 키를 입력받는 최대, 올라가게하는 시간)
    private float jumpTimer; // 점프 키를 누르고 있는 시간

    public bool jump; // 플레이어가 점프 키를 눌렀는가
    public bool canJump = true; // 점프가 가능한 상태인가
    public bool isJumping = false; // 점프 중인가(위로 힘이 가해지고 있는가)
    public bool isFalling = false; // 떨어지고 있는가


    public float mass = 2f;
    public float drag = 4f;
    public float gravity = 1.5f;


    private PlayerInput2 playerInput; //플레이어에 관한 입력을 담당

    private Rigidbody2D playerRigidbody; // 플레이어의 리지드바디
    private Animator playerAnimator; // 플레이어의 애니메이터(추후 수정)
    private BoxCollider2D playerCollider; // 플레이어의 컬라이더
    private PlayerView playerView;


    private void Start()
    {

        //필요한거 불러옴 ㅇㅇ
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
        Move(); // 좌우 이동
        Jump(); // 점프
        IsFalling(); // 떨어지고 있는지, 점프 가능 여부
        Down(); // 아래로 점프
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

    private void IsFalling() // 처음에는 떨어지는 상태만 구현하려고 했지만, 점프가 가능한 상태를 판별하는 코드도 이와 비슷하기에 canJump와 관련된 코드도 입력 함.
    {
        if (playerRigidbody.velocity.y < 0f) // 플레이어의 y축 이동 속도가 0미만, 아래로 떨어지고 있을 때,
        {
            isFalling = true;
        }
        else if (playerRigidbody.velocity.y > 0f) // 플레이어의 y축 이동 속도가 0초과, 위로 올라가고 있을 때,
        {
            isFalling = false;
        }
        else if (playerRigidbody.velocity.y == 0f) // 플레이어의 y축 이동 속도가 0, 점프 후 꼭대기 이거나, 땅에 다이거나
        {
            isFalling = false; 
            canJump = true;         //*제일 윗 부분일 때 canjump가 참이면 한 번씩 점프가 더 되는 버그가 예상됨 그래서 점프 메소드에 이를 해결할 내용을 추가
        }
    }

    private void Jump() // 3가지의 if문으로 구성 되있음 1. 점프 입력 받기 2. 점프 종료 3. 점프 실행
    {
        //platform effector의 기능을 사용해서 floor를 아래에서 위로 지날 때, 충돌하지 않게 설정 해둠.
        if (playerInput.jump_down && !isFalling && canJump) //점프 키를 눌렀을때, 떨어지는 상태가 아니고, 점프가 가능한 상태
        {
            canJump = false; // 점프 가능 X 
            isJumping = true; // 점프 중(위로 올라가기 시작)
            jumpTimer = 0; // 점프 타이머 초기화

            //3번 if문이 작동 할 수 있게 준비하는 단계
        }

        if (playerInput.jump_up) // 점프키를 땔 때,
        {
            isJumping = false; // 점프 종료(올라가는 거 끝)
            return;

            // 3번 if문에 타이머시간이 넘어가면 점프가 종료 되게 하는 코드가 있지만,
            // 타이머 시간 안에 점프키를 때면, 점프가 종료 되게하는 코드가 없음.
            // 3번 if문 안에 있는 타이머 초과로 인한 점프 종료 if문에 2번 if문을 넣으면,
            // 타이머 시간 안에서 점프 키를 뺐을 때, isJumping이 false가 되지 않는다.
            // 그래서 따로 빼두니, 잘 작동함 ㅇㅇ
        }   

        if ((playerInput.jump && !isFalling && isJumping)) // 점프 키를 누르고 있을 때 + 떨어지는 중이 아닐때, 점프 중일때(올라가기 가능)
        {
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(Vector2.up * jumpForce * ((jumpTimer * 1.2f) + 1f), ForceMode2D.Impulse); // 이건 나도 잘... 근데 수정이 좀 필요함(첫 점프 좀 낮게 만들 수 있어야함)
                                                                                                               // project manager에서 physics2d 수정 + rigidbody2d의 linear drag 수정 +
                                                                                                               // Mess 등등 수정 해가면서 좋은 값을 찾아야함.

            jumpTimer += Time.deltaTime; // 타이머 작동 -> Time.deltaTime == 1/프레임 ex) 1/30, 1/60...
            if(jumpTimer >= jumpTimeLimit) // 점프 제한 시간이 끝나면,
            {
                isJumping = false; // 점프 종료문
                return;
            }
        }
        // canjump를 jump문 안에서 true로 바꿔주면, 계속 점프가 되서 IsFailling 메서드에서 true가 되게 만들어 둠.
    }
    
    private void Down() // 아랫 점프
    {
        GameObject obj = GameObject.FindGameObjectsWithTag("floor")[0]; // tag가 floor(발판)인 게임 오브젝트 저장 (허허 꼼수임 나중에 고쳐야됨 ㅋㅋㅋㅋ 꼭! 고쳐야됨!)
        if (playerInput.down) // 아래 점프 버튼 입력 중 일때,
        {
            playerRigidbody.mass = mass * 2;           
            playerRigidbody.drag = drag / 40;
            playerRigidbody.gravityScale = gravity*(4/3);   //더 빨리 떨어지게 하기 위해서 리지드바디 값 수정
            obj.layer = 9; //void : 9 아까 저장해둔 오브젝트의 레이어 값을 변경
        }
        else if(playerInput.down_up) // 아래 점프 버튼 입력이 아닐 때,
        {
            playerRigidbody.mass = mass;
            playerRigidbody.drag = drag;
            playerRigidbody.gravityScale = gravity;    // 원래 리지드바드 값으로 복원
            obj.layer = 7; // 원래 레이어 값으로 변경
        }
        //플레이어에 platform effector를 사용해서 특정 레이어만 충돌하게 설정 해둠
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

    //여기서 부터...
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
    //...여기 까지는 플레이어 땅에 착지했을 때, 코드가 잘 작동하지 않을 때를 대비한 코드


}
