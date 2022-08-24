using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    public float angle;
    public Vector2 playerPosition;
    public Transform gunpivot;

    

    public bool isViewLeft { get; private set; }
    public bool isViewRight { get; private set; }

    public float angle_weight = 0;
    public float ScaleX;
    public float Scale;

    public Quaternion Gun_rotation;


    // Start is called before the first frame update
    void Start()
    {
        Start_Left_Right();
        ScaleX = this.transform.localScale.x;
        gunpivot = transform.Find("gun_Pivot");
    }

    // Update is called once per frame
    void Update()
    {
        GetRotation();
        Left_Right();
        gunpivot.rotation = Gun_rotation;
        gunpivot.localScale = new Vector3(Scale, Scale, 1);
    }

    private void GetRotation()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        angle = Mathf.Atan2(playerPosition.y - this.transform.position.y, playerPosition.x - this.transform.position.x) * Mathf.Rad2Deg;
        Gun_rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Debug.Log(angle);
    }
    private void Start_Left_Right()
    {
        if ((angle > 95f || angle <= -95f))
        {
            //왼쪽 보기
            ScaleX = -ScaleX;
            Scale = -1;

            isViewLeft = true;
            isViewRight = false;
        }

        if ((angle <= 85f && angle > -85f))
        {
            //오른쪽 보기
            ScaleX = -ScaleX;
            Scale = 1;

            isViewLeft = false;
            isViewRight = true;
        }
    }

    private void Left_Right()
    {
        if ((angle > 95f || angle <= -95f) && isViewRight)
        {
            //왼쪽 보기
            ScaleX = -ScaleX;
            Scale = -1;

            isViewLeft = true;
            isViewRight = false;
        }

        if ((angle <= 85f && angle > -85f) && isViewLeft)
        {
            //오른쪽 보기
            ScaleX = -ScaleX;
            Scale = 1;

            isViewLeft = false;
            isViewRight = true;
        }
    }
}