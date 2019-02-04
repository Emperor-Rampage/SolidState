using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int PlayerHealth = 40;
    public AudioClip HealSound;
    public GameObject deathEffect;

    public AudioSource audioS;
    //public int Score;

    private void Start()
    {
        if(!audioS)
            audioS = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health heal = collision.gameObject.GetComponent<Health>();
        if (heal)
        {
            if (PlayerHealth < 100)
            {
                PlayerHealth += heal.HealAmount;
                if (PlayerHealth > 100)
                    PlayerHealth = 100;

                audioS.PlayOneShot(HealSound);
                Destroy(collision.gameObject);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        PlayerHealth -= damage;

        if (PlayerHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
