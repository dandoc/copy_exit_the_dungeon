using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string moveAxisName = "Horizontal";//
    public string Jump = "Jump";
    public string Down = "Down";
    public string dashButtonName = "Dash";


    public float move { get; private set; }
    public bool jump { get; private set; }
    public bool down { get; private set; }
    //public bool isJumping { get; private set; }
    public bool dash {get; private set; }

    // Update is called once per frame
    void Update()
    {
        //게임오버 시, 입력 X
        //게임메니저 구현시에 만듭시다
        /*
        move = 0;
        jump = 0;
        dash = false;
        */

        move = Input.GetAxisRaw(moveAxisName);
        jump = Input.GetButtonDown(Jump);
        down = Input.GetButton(Down);
        Debug.Log(jump);
        //isJumping = Input.GetButtonDown(jumpAxisName);
        dash = Input.GetButtonDown(dashButtonName);

    }
}
