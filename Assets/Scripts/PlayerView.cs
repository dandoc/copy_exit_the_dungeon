using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public float angle;
    public Vector2 mousePosition;

    public bool isViewLeft { get; private set; }
    public bool isViewRight { get; private set; }

    public float angle_weight = 0;
    public float playerScaleX = 1;

    public Quaternion Gun_rotation;

    void Start()
    {
        Start_Left_Right();
    }

    // Update is called once per frame
    void Update()
    {
        GetRotation();
        Left_Right();
    }

    private void GetRotation()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mousePosition.y - this.transform.position.y, mousePosition.x - this.transform.position.x) * Mathf.Rad2Deg;
        Gun_rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Start_Left_Right()
    {
        if ((angle > 95f || angle <= -95f))
        {
            //왼쪽 보기
            playerScaleX = -1;

            isViewLeft = true;
            isViewRight = false;
        }

        if ((angle <= 85f && angle > -85f))
        {
            //오른쪽 보기
            playerScaleX = 1;

            isViewLeft = false;
            isViewRight = true;
        }
    }

    private void Left_Right()
    {
        if ((angle > 95f || angle <= -95f) && isViewRight)
        {
            //왼쪽 보기
            playerScaleX = -1;
            
            isViewLeft = true;
            isViewRight = false;
        }

        if ((angle <= 85f && angle > -85f) && isViewLeft)
        {
            //오른쪽 보기
            playerScaleX = 1;
            
            isViewLeft = false;
            isViewRight = true;
        }
    }
}
