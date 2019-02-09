using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController2D))]

public class PlayerMovement : MonoBehaviour
{
    private CharacterController2D control;
    private Animator anim;

    float horizontalMove = 0.0f;

    public float runSpeed = 40.0f;

    bool jump = false;
    bool crouch = false;

    void Start() //Happens after Awake()!
    {
        control = GetComponent<CharacterController2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; //Grabbing any horizontal axis to move the playe

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            
        }

        if(Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if(Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

        anim.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if(control.m_Grounded)
        {
            anim.SetBool("IsJumping", false);
        }
    }

    void FixedUpdate() //Use FixedUpdate for all physics calculations, like player or enemy movement.
    {
        control.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump); //Time.fixedDeltaTime is important, as it takes the exact time that has elapsed since the last call of this method, and keeps all character motion consistent across any platforms.
        jump = false;
        if (!control.m_Grounded)
        {
            anim.SetBool("IsJumping", true);
        }
    }

    public void OnLanding()
    {
        Debug.Log("OnLanding Event");
        //anim.SetBool("IsJumping", false);
    }
}
