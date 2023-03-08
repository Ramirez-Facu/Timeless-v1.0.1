using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Animator anim;



    //************************ Movement variables  ************************

    private Rigidbody2D rb;
    private Vector2 movement;
    private float dashTimer;
    private float dashCooldownTimer;
    private Vector3 normalColliderSize;
    private bool Immunity = false;

    public float jumpForce = 2f;
    public float crouchScale = 0.5f;
    public float normalScale = 1f;
    public float crouchDuration = 0.5f;
    public float moveSpeed = 5f;
    public float dashDistance = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;


    //*********************************************************************

    //********************* Effects ***************************************
    public ParticleSystem Dust;
    //************************ Boolean Variables **************************
    //private bool powerupActive = false;
    private bool isDashing;
    private bool isJumping = false;
    private bool isCrouching = false;
    //*********************************************************************


    //************************ SPRITES ************************************

    public Sprite jumpingSprite; 
    public Sprite PowerupjumpingSprite;
    public Sprite idleSprite;
    public Sprite RunnigSprite;
    public Sprite PowerupRunnigSprite;
    public Sprite PowerupIdleSprite;
    private SpriteRenderer spriteRender;
    //*********************************************************************

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        normalColliderSize = GetComponent<BoxCollider2D>().size;
        spriteRender = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Movement to the sides
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput != 0)
        {
            CreateDust();
            anim.SetBool("running", true);
            transform.position += new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0f, 0f);
            if (horizontalInput > 0)
            {
                spriteRender.flipX = false;
                spriteRender.sprite = RunnigSprite;
            }
            else
            {
                spriteRender.flipX = true;
                spriteRender.sprite = RunnigSprite;
            }

        }
        else
        {
            anim.SetBool("running", false);
            if (!isJumping)
            {
                StopDust();
                spriteRender.sprite = idleSprite;
            }
            else 
            {
                spriteRender.sprite = jumpingSprite;
            }
                
        }

        ////dashing

        if (Input.GetKeyDown(KeyCode.C) && dashCooldownTimer <= 0)
        {
            CreateDust();
            Dash();
                
        }

        // Check if dash is still in progress
        if (isDashing)
        {
                
            Immunity = true;
            // Subtract from dash timer
            dashTimer -= Time.deltaTime;

            // Check if dash is over
            if (dashTimer <= 0)
            {
                // End dash
                isDashing = false;
                rb.velocity = Vector2.zero;
                dashCooldownTimer = dashCooldown;
            }
        }
        else
        {
            // Start cooldown timer
            if (dashCooldownTimer > 0)
            {
                Immunity = false;
                dashCooldownTimer -= Time.deltaTime;
            }
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && !isCrouching)
        {
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            spriteRender = GetComponent<SpriteRenderer>();
            spriteRender.sprite = jumpingSprite;

        }

        // Crouching
        if (Input.GetKeyDown(KeyCode.X) && !isJumping && !isCrouching)
        {
            isCrouching = true;
            GetComponent<BoxCollider2D>().size = new Vector2(GetComponent<BoxCollider2D>().size.x, normalColliderSize.y * crouchScale);
        }

        if (isCrouching)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        if (isJumping || isCrouching)
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isJumping = false;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Time.timeScale = 0;
        }
    }

    void CreateDust() 
    {
        Dust.Play();
    }
    void StopDust()
    {
        Dust.Stop();
    }
    void Dash()
    {
        // Get movement direction
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        // Set dash timer and flag
        dashTimer = dashDuration;
        isDashing = true;

        // Set velocity to dash distance in movement direction
        rb.velocity = movement * dashDistance;
    }

}
