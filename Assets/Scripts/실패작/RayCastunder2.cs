using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastunder2 : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D col;
    public Vector3 LD_ray_pos;
    public Vector3 LU_ray_pos;
    public Vector3 RD_ray_pos;
    public Vector3 RU_ray_pos;
    public RaycastHit2D LD_hit { get; private set; }
    public RaycastHit2D LU_hit { get; private set; }
    public RaycastHit2D RD_hit { get; private set; }
    public RaycastHit2D RU_hit { get; private set; }

   private bool LD_F;
   private bool LU_F;
   private bool RD_F;
   private bool RU_F;
   private bool LD_G;
   private bool RU_G;
   private bool LU_G;
   private bool RD_G;

    public bool down_floor { get; private set; }
    public bool down_ground { get; private set; }
    public bool up_floor { get; private set; }
    public bool up_ground { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();

    }

    // Update is called once  frame
    private void Update()
    {
        LD_ray_pos = transform.position + new Vector3(col.size.x / -2, col.size.y / -2 - 0.02f, 0);
        LU_ray_pos = transform.position + new Vector3(col.size.x / -2, col.size.y / +2 + 0.05f, 0);

        RD_ray_pos = transform.position + new Vector3(col.size.x / 2, col.size.y / -2 - 0.02f, 0);
        RU_ray_pos = transform.position + new Vector3(col.size.x / 2, col.size.y / +2 + 0.05f, 0);

        Debug.DrawRay(LD_ray_pos, Vector2.up * -0.015f, Color.red);
        Debug.DrawRay(LU_ray_pos, Vector2.up * 0.1f, Color.red);

        LD_hit = Physics2D.Raycast(LD_ray_pos, Vector2.down, 0f);
        LU_hit = Physics2D.Raycast(LU_ray_pos, Vector2.up, 0.1f);


        Debug.DrawRay(RD_ray_pos, Vector2.up * -0.015f, Color.red);
        Debug.DrawRay(RU_ray_pos, Vector2.up * 0.1f, Color.red);

        RD_hit = Physics2D.Raycast(RD_ray_pos, Vector2.down, 0f);
        RU_hit = Physics2D.Raycast(RU_ray_pos, Vector2.up, 0.1f);



        if (LD_hit.collider)
        {
            if(LD_hit.transform.tag != "floor")
                LD_F = false;
            else if (LD_hit.transform.tag == "floor")
                LD_F = true;
            if(LD_hit.transform.tag != "ground")
                LD_G = false;
            else if (LD_hit.transform.tag == "ground")
                LD_G = true;
        }
        else if(!LD_hit.collider)
        {
            LD_F = false;
            LD_G = false;
        }

        if (RD_hit.collider)
        {
            if (RD_hit.transform.tag != "floor")
                RD_F = false;
            else if (RD_hit.transform.tag == "floor")
                RD_F = true;
            if (RD_hit.transform.tag != "ground")
                RD_G = false;
            else if (RD_hit.transform.tag == "ground")
                RD_G = true;
        }
        
        else if (!RD_hit.collider)
        {
            RD_F = false;
            RD_G = false;
        }

        down_floor = LD_F == true || RD_F == true;
        down_ground = LD_G == true || RD_G == true;


        if (LU_hit.collider)
        {
            if (LU_hit.transform.tag != "floor")
                LU_F = false;
            else if (LU_hit.transform.tag == "floor")
                LU_F = true;
            if (LU_hit.transform.tag != "ground")
                LU_G = false;
            else if (LU_hit.transform.tag == "ground")
                LU_G = true;
        }
        else if(!LU_hit.collider)
        {
            LU_F = false;
            LU_G = false;
        }

        if (RU_hit.collider)
        {
            if (RU_hit.transform.tag != "floor")
                RU_F = false;
            else if (RU_hit.transform.tag == "floor")
                RU_F = true;
            if (RU_hit.transform.tag != "ground")
                RU_G = false;
            else if (RU_hit.transform.tag == "ground")
                RU_G = true;
        }
        else if (!RU_hit.collider)
        {
            RU_F = false;
            RU_G = false;
        }
        up_floor = LU_F == true || RU_F == true;
        up_ground = LU_G == true || RU_G == true;

        //Debug.Log("down_floor: " + down_floor);
        //Debug.Log("down_ground: " + down_ground);
        //Debug.Log("up_floor: " + up_floor);
        //Debug.Log("up_ground: " + up_ground);

    }

}
