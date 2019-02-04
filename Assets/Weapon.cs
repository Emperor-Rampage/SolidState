using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;
    public float idleReturnVal = 0.5f;
    private float countdown = 0.0f;
    public AudioClip fireEffect;

    private Animator anim;
    private AudioSource audioS;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioS = GetComponent<AudioSource>();
    }

    //Calls the shot, and checks for the countdown timer to return to non-firing position.
    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            Shoot();
            countdown = idleReturnVal;
        }
        Countdown();
    }

    void Shoot()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
        if(fireEffect)
        {
            audioS.PlayOneShot(fireEffect);
        }

        anim.SetLayerWeight(1, 1);
    }

    public void Countdown()
    {
        if (countdown > 0.0f)
        {
            countdown -= Time.deltaTime;
        }
        else if (countdown <= 0.0f && anim.GetLayerWeight(1) > 0)
        {
            countdown = 0.0f;
            anim.SetLayerWeight(1, 0);
        }
    }
}
