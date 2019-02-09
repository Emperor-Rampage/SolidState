using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 10;
    public float speed = 20.0f;
    public Rigidbody2D rb;

    public GameObject BlastSprite;

    void Start()
    {
        if (!rb)
            rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.right * speed;
        Destroy(gameObject, 2.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStats player = collision.GetComponent<PlayerStats>();

        if (player != null)
        {
            player.TakeDamage(damage);
        }
        Instantiate(BlastSprite, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
