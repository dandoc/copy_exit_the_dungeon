using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D enemyRigidbody;
    private CapsuleCollider2D enemyColloder;
    private EnemyView enemyView;


    private void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyColloder = GetComponent<CapsuleCollider2D>();
        enemyView = GetComponent<EnemyView>();
    }


    void Update()
    {
        View();
    }


    private void View()
    {
        this.transform.localScale = new Vector3(enemyView.ScaleX, this.transform.localScale.y, this.transform.localScale.z  );
    }
}
