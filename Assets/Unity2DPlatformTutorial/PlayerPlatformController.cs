using UnityEngine;

public class PlayerPlatformController : PhysicsObject
{
    public float jumpTakeOffSpeed = 7.0f;
    public float jumpCancelDamping = 0.5f;
    public float walkSpeed = 7.0f;

    //public bool airControl = true; //Future setting to allow horizontal control in the air.

    private SpriteRenderer _spriteRenderer;
    private Animator _anim;

    private bool isFacingRight = true; //Boolean to determin if the player is facing right or not.

    // Start is called before the first frame update
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
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

        _anim.SetBool("IsJumping", !isGrounded);
        _anim.SetFloat("Speed", Mathf.Abs(move.x));

        targetVelocity = move * walkSpeed;

    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
}
