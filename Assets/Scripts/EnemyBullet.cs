using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public BulletData bulletData;

    private Rigidbody2D bulletRigidbody;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    private void Update()
    {
        bulletRigidbody.velocity = transform.right * bulletData.bullet_speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        IDamageable target = other.GetComponent<IDamageable>();
        if (target != null)
        {
            target.PlayerOnDamage();
            Destroy(this.gameObject);
        }

        if (other.tag == "ground")
            Destroy(this.gameObject);
    }
}
