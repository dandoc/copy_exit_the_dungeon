using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public float moveSpeed = 5f;    // 戚疑 什杷球
    public float dashForce = 30f;   // 企習 什杷球 箸精 毘
    public float jumpForce = 20f;   // 繊覗 毘

    public float jumpTimeLimit = 0.2f; // 繊覗 置企 獣娃(繊覗 徹研 脊径閤澗 置企, 臣虞亜惟馬澗 獣娃)
    private float jumpTimer; // 繊覗 徹研 刊牽壱 赤澗 獣娃

    public bool jump; // 巴傾戚嬢亜 繊覗 徹研 喚袈澗亜
    public bool canJump = true; // 繊覗亜 亜管廃 雌殿昔亜
    public bool isJumping = false; // 繊覗 掻昔亜(是稽 毘戚 亜背走壱 赤澗亜)
    public bool isFalling = false; // 恭嬢走壱 赤澗亜


    public float mass = 2f;
    public float drag = 4f;
    public float gravity = 1.5f;


    private PlayerInput2 playerInput; //巴傾戚嬢拭 淫廃 脊径聖 眼雁

    private Rigidbody2D playerRigidbody; // 巴傾戚嬢税 軒走球郊巨
    private Animator playerAnimator; // 巴傾戚嬢税 蕉艦五戚斗(蓄板 呪舛)
    private BoxCollider2D playerCollider; // 巴傾戚嬢税 鎮虞戚希
    private PlayerView playerView;


    private void Start()
    {

        //琶推廃暗 災君身 しし
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
        Move(); // 疎酔 戚疑
        Jump(); // 繊覗
        IsFalling(); // 恭嬢走壱 赤澗走, 繊覗 亜管 食採
        Down(); // 焼掘稽 繊覗
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

    private void IsFalling() // 坦製拭澗 恭嬢走澗 雌殿幻 姥薄馬形壱 梅走幻, 繊覗亜 亜管廃 雌殿研 毒紺馬澗 坪球亀 戚人 搾汁馬奄拭 canJump人 淫恵吉 坪球亀 脊径 敗.
    {
        if (playerRigidbody.velocity.y < 0f) // 巴傾戚嬢税 y逐 戚疑 紗亀亜 0耕幻, 焼掘稽 恭嬢走壱 赤聖 凶,
        {
            isFalling = true;
        }
        else if (playerRigidbody.velocity.y > 0f) // 巴傾戚嬢税 y逐 戚疑 紗亀亜 0段引, 是稽 臣虞亜壱 赤聖 凶,
        {
            isFalling = false;
        }
        else if (playerRigidbody.velocity.y == 0f) // 巴傾戚嬢税 y逐 戚疑 紗亀亜 0, 繊覗 板 伽企奄 戚暗蟹, 競拭 陥戚暗蟹
        {
            isFalling = false; 
            canJump = true;         //*薦析 性 採歳析 凶 canjump亜 凧戚檎 廃 腰梢 繊覗亜 希 鞠澗 獄益亜 森雌喫 益掘辞 繊覗 五社球拭 戚研 背衣拝 鎧遂聖 蓄亜
        }
    }

    private void Jump() // 3亜走税 if庚生稽 姥失 鞠赤製 1. 繊覗 脊径 閤奄 2. 繊覗 曽戟 3. 繊覗 叔楳
    {
        //platform effector税 奄管聖 紫遂背辞 floor研 焼掘拭辞 是稽 走劾 凶, 中宜馬走 省惟 竺舛 背客.
        if (playerInput.jump_down && !isFalling && canJump) //繊覗 徹研 喚袈聖凶, 恭嬢走澗 雌殿亜 焼艦壱, 繊覗亜 亜管廃 雌殿
        {
            canJump = false; // 繊覗 亜管 X 
            isJumping = true; // 繊覗 掻(是稽 臣虞亜奄 獣拙)
            jumpTimer = 0; // 繊覗 展戚袴 段奄鉢

            //3腰 if庚戚 拙疑 拝 呪 赤惟 層搾馬澗 舘域
        }

        if (playerInput.jump_up) // 繊覗徹研 卿 凶,
        {
            isJumping = false; // 繊覗 曽戟(臣虞亜澗 暗 魁)
            return;

            // 3腰 if庚拭 展戚袴獣娃戚 角嬢亜檎 繊覗亜 曽戟 鞠惟 馬澗 坪球亜 赤走幻,
            // 展戚袴 獣娃 照拭 繊覗徹研 凶檎, 繊覗亜 曽戟 鞠惟馬澗 坪球亜 蒸製.
            // 3腰 if庚 照拭 赤澗 展戚袴 段引稽 昔廃 繊覗 曽戟 if庚拭 2腰 if庚聖 隔生檎,
            // 展戚袴 獣娃 照拭辞 繊覗 徹研 三聖 凶, isJumping戚 false亜 鞠走 省澗陥.
            // 益掘辞 魚稽 皐砧艦, 設 拙疑敗 しし
        }   

        if ((playerInput.jump && !isFalling && isJumping)) // 繊覗 徹研 刊牽壱 赤聖 凶 + 恭嬢走澗 掻戚 焼諌凶, 繊覗 掻析凶(臣虞亜奄 亜管)
        {
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(Vector2.up * jumpForce * ((jumpTimer * 1.2f) + 1f), ForceMode2D.Impulse); // 戚闇 蟹亀 設... 悦汽 呪舛戚 岨 琶推敗(湛 繊覗 岨 碍惟 幻級 呪 赤嬢醤敗)
                                                                                                               // project manager拭辞 physics2d 呪舛 + rigidbody2d税 linear drag 呪舛 +
                                                                                                               // Mess 去去 呪舛 背亜檎辞 疏精 葵聖 達焼醤敗.

            jumpTimer += Time.deltaTime; // 展戚袴 拙疑 -> Time.deltaTime == 1/覗傾績 ex) 1/30, 1/60...
            if(jumpTimer >= jumpTimeLimit) // 繊覗 薦廃 獣娃戚 魁蟹檎,
            {
                isJumping = false; // 繊覗 曽戟庚
                return;
            }
        }
        // canjump研 jump庚 照拭辞 true稽 郊蚊爽檎, 域紗 繊覗亜 鞠辞 IsFailling 五辞球拭辞 true亜 鞠惟 幻級嬢 客.
    }
    
    private void Down() // 焼熊 繊覗
    {
        GameObject obj = GameObject.FindGameObjectsWithTag("floor")[0]; // tag亜 floor(降毒)昔 惟績 神崎詮闘 煽舌 (買買 可呪績 蟹掻拭 壱団醤喫 せせせせ 伽! 壱団醤喫!)
        if (playerInput.down) // 焼掘 繊覗 獄動 脊径 掻 析凶,
        {
            playerRigidbody.mass = mass * 2;           
            playerRigidbody.drag = drag / 40;
            playerRigidbody.gravityScale = gravity*(4/3);   //希 察軒 恭嬢走惟 馬奄 是背辞 軒走球郊巨 葵 呪舛
            obj.layer = 9; //void : 9 焼猿 煽舌背黍 神崎詮闘税 傾戚嬢 葵聖 痕井
        }
        else if(playerInput.down_up) // 焼掘 繊覗 獄動 脊径戚 焼諌 凶,
        {
            playerRigidbody.mass = mass;
            playerRigidbody.drag = drag;
            playerRigidbody.gravityScale = gravity;    // 据掘 軒走球郊球 葵生稽 差据
            obj.layer = 7; // 据掘 傾戚嬢 葵生稽 痕井
        }
        //巴傾戚嬢拭 platform effector研 紫遂背辞 働舛 傾戚嬢幻 中宜馬惟 竺舛 背客
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

    //食奄辞 採斗...
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
    //...食奄 猿走澗 巴傾戚嬢 競拭 鐸走梅聖 凶, 坪球亜 設 拙疑馬走 省聖 凶研 企搾廃 坪球


}
