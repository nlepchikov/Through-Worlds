using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private CapsuleCollider2D coll;

    [SerializeField] private LayerMask jumpableGround;

    private enum MovementState { idle, running, jumping, falling};

    [SerializeField] private AudioSource jumpSound;

    private float dirX;
    public float jumpForce = 5f;
    public float velocityForce = 7f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * velocityForce  , rb.velocity.y);
        
        if(Input.GetButtonDown("Jump") && IsGrounded() == true)
        {
            jumpSound.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimState();
    }

    private void UpdateAnimState()
    {
        MovementState State;

        if (dirX > 0f)
        {
            State = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            State = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            State = MovementState.idle;

        }

        if(rb.velocity.y > .1f)
        {
            State = MovementState.jumping;
        }
        else if(rb.velocity.y < -.1f)
        {
            State = MovementState.falling;
        }

        anim.SetInteger("State", (int)State);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
