using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput2 : MonoBehaviour
{
    public string moveAxisName = "Horizontal";
    public string Jump = "Jump";
    public string Down = "Down";
    public string dashButtonName = "Dash";


    public float move { get; private set; } // 좌우 이동
    public bool jump { get; private set; } // 점프 키 누르는 중
    public bool down { get; private set; } // 아래 키 누르는 중
    public bool down_up { get; private set; }
    public bool jump_down { get; private set; } // 점프 키를 누를 때
    public bool jump_up { get; private set; } // 점프 키 땔 때
    public bool dash {get; private set; } // 대쉬 키...아마 대쉬관련 변수 추가 예정 => 인풋 스크립트에서는 대쉬관련 추가 없음.
    public bool fire { get; private set; }
    public bool reload { get; private set; }

    // Update is called once per frame
    void Update()
    {
        //게임오버 시, 입력 X
        //게임메니저 구현시에 만들 예정 (인스턴스 포함)
        /*
        move = 0;
        jump = 0;
        dash = false;
        */

        move = Input.GetAxisRaw(moveAxisName);
        jump = Input.GetButton(Jump);
        jump_down = Input.GetButtonDown(Jump);
        down = Input.GetButton(Down);
        down_up = Input.GetButtonUp(Down);
        jump_up = Input.GetButtonUp(Jump);
        dash = Input.GetButtonDown(dashButtonName);
        fire = Input.GetMouseButtonDown(0);
        reload = Input.GetKeyDown(KeyCode.R);
        //if(reload)
        //{
        //    Debug.Log("r");
        //}
    }

}
