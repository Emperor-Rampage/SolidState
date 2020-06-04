using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public float speed = 20.0f;
    public int damage = 20;
    public Rigidbody2D rb;
    public GameObject BlastSprite;

    // Start is called before the first frame update
    void Start()
    {
        if (!rb)
            rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.right * speed;
        Destroy(gameObject, 2.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>(); 
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Instantiate(BlastSprite,transform.position,transform.rotation);
        Destroy(gameObject);
    }
}
