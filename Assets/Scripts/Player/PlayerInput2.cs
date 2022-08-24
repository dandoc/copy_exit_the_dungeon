using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput2 : MonoBehaviour
{
    public string moveAxisName = "Horizontal";
    public string Jump = "Jump";
    public string Down = "Down";
    public string dashButtonName = "Dash";


    public float move { get; private set; } // �¿� �̵�
    public bool jump { get; private set; } // ���� Ű ������ ��
    public bool down { get; private set; } // �Ʒ� Ű ������ ��
    public bool down_up { get; private set; }
    public bool jump_down { get; private set; } // ���� Ű�� ���� ��
    public bool jump_up { get; private set; } // ���� Ű �� ��
    public bool dash {get; private set; } // �뽬 Ű...�Ƹ� �뽬���� ���� �߰� ���� => ��ǲ ��ũ��Ʈ������ �뽬���� �߰� ����.
    public bool fire { get; private set; }
    public bool reload { get; private set; }

    // Update is called once per frame
    void Update()
    {
        //���ӿ��� ��, �Է� X
        //���Ӹ޴��� �����ÿ� ���� ���� (�ν��Ͻ� ����)
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
