using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] LayerMask jumpableGround;

    private float dirX = 0f;
    private float moveSpeed = 7f;
    private float jumpforce = 14f;

    private enum MovementState { idle, running, jumping, falling }

    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource walkSoundEffect;

    private bool isWalking;

    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        rb.velocity = new UnityEngine.Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded()) //maybe add the isGrounded?
        {
            jumpSoundEffect.Play();
            rb.velocity = new UnityEngine.Vector2(rb.velocity.x, jumpforce);
        }

        UpdateAnimationState();
        UpdateWalkSound();

    }


    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            
            sprite.flipX = false;

        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -0.1f) 
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state" , (int)state);

    }

    private void UpdateWalkSound()
    {

        if (IsGrounded() && dirX != 0)
        {
            // Check if the walk sound effect is not playing
            if (!walkSoundEffect.isPlaying)
            {
                // Play the walk sound effect
                walkSoundEffect.Play();
                isWalking = true;
            }
        }
        else
        {
            // Check if the player was walking and the walk sound effect is playing
            if (isWalking && walkSoundEffect.isPlaying)
            {
                // Stop the walk sound effect
                walkSoundEffect.Stop();
                isWalking = false;
            }
        }

    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, UnityEngine.Vector2.down, .1f, jumpableGround);
    }
}

