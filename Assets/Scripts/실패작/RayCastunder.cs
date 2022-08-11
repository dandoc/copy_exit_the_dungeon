using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastunder : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D col;
    public Vector3 down_ray_pos;
    public Vector3 up_ray_pos;
    public RaycastHit2D[] underhits { get; private set; }
    public RaycastHit2D[] abovehits { get; private set; }
    public string[] undertags { get; private set; }
    public string[] abovetags { get; private set; }
    public bool up_floor;
    public bool down_floor;
    public bool up_ground;
    public bool down_ground;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        
    }

    // Update is called once  frame
    private void Update()
    {
        down_ray_pos = transform.position - new Vector3(col.size.x / 2, col.size.y / 2 + 0.015f, 0);
        up_ray_pos = transform.position + new Vector3(col.size.x / 2, col.size.y / 2 + 0.2f, 0);

        Debug.DrawRay(down_ray_pos, Vector2.right * col.size.y, Color.red);
        Debug.DrawRay(up_ray_pos, Vector2.right * -col.size.y, Color.red);
        RaycastHit2D[] underhits = Physics2D.RaycastAll(down_ray_pos, Vector2.right, col.size.x);
        RaycastHit2D[] abovehits = Physics2D.RaycastAll(up_ray_pos, Vector2.right, -col.size.x);
        int d_length = underhits.Length;
        int u_length = abovehits.Length;
        undertags = new string[d_length];
        abovetags = new string[u_length];
        up_floor = false;
        down_floor = false;
        up_ground = false;
        down_ground = false;
        for (int i = 0; i < underhits.Length; i++)
        {
            undertags[i] += underhits[i].transform.tag;
            if (undertags[i] == "floor")
                down_floor = true;
            else if (undertags[i] == "ground")
                down_ground = true;
            //Debug.Log("under: "+undertags[i]);
        }

        for (int i = 0; i < abovehits.Length; i++)
        {
            abovetags[i] += abovehits[i].transform.tag;
            if (abovetags[i] == "floor")
                up_floor = true;
            else if (abovetags[i] == "ground")
                up_ground = true;
            // Debug.Log("above: "+abovetags[i]);
        }

    }
}
