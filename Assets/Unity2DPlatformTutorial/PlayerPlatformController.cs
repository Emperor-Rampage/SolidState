using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class PlayerPlatformController : PhysicsObject
{
    public MasterInput controls;

    public float jumpTakeOffSpeed = 7.0f;
    public float jumpCancelDamping = 0.5f;
    //public float crouchSpeed;
    //public float wallSlideDamping = 0.8f;
    //public float dashSpeedUp;
    public float walkSpeed = 7.0f;

    //public bool airControl = true; //Future setting to allow horizontal control in the air.

    private SpriteRenderer spriteRenderer;
    private Animator anim;

    private bool isFacingRight = true; //Boolean to determin if the player is facing right or not.

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");


        if(Input.GetButtonDown("Jump") && isGrounded)
        {

            velocity.y = jumpTakeOffSpeed;
        }
        else if(Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
                velocity.y = velocity.y * jumpCancelDamping;
        }

        if(move.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if(move.x < 0 && isFacingRight)
        {
            Flip();
        }

        /* Old flip sprite code.
        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
        if(flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
        */

        anim.SetBool("IsJumping", !isGrounded);
        anim.SetFloat("Speed", Mathf.Abs(move.x));

        targetVelocity = move * walkSpeed;

    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
}
