﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    public float moveSpeed = 1.0f;
    private bool isOnGround = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("d"))
        {
            anim.SetInteger("State", 1);
            transform.Translate(moveSpeed, 0, 0);
        }
        else if(Input.GetKey("a"))
        {
            anim.SetInteger("State", 2);
            transform.Translate(-moveSpeed, 0, 0);
        }
        else
        {
            anim.SetInteger("State", 0);
        }
    }
}