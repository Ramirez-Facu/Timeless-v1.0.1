using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;
    public float crouchScale = 0.5f;
    public float normalScale = 1f;
    public float crouchDuration = 0.5f;
    public float moveSpeed = 5f;

    public Sprite jumpingSprite; 
    public Sprite PowerupjumpingSprite;
    public Sprite idleSprite;
    public Sprite RunnigSprite;
    public Sprite PowerupRunnigSprite;
    public Sprite PowerupIdleSprite;
    private bool powerupActive = false;
  
    private SpriteRenderer spriteRender;

    private bool isJumping = false;
    private bool isCrouching = false;
    private float crouchTimer = 0f;

    private Rigidbody2D rb;
    private Vector3 normalColliderSize;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        normalColliderSize = GetComponent<BoxCollider2D>().size;
        spriteRender = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            powerupActive = !powerupActive;
        }
        if (!powerupActive)
        {
            // Movement to the sides
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            if (horizontalInput != 0)
            {
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
                if (!isJumping)
                {
                    spriteRender.sprite = idleSprite;
                }
                else 
                {
                    spriteRender.sprite = jumpingSprite;
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
            if (Input.GetKeyDown(KeyCode.LeftControl) && !isJumping && !isCrouching)
            {
                isCrouching = true;
                GetComponent<BoxCollider2D>().size = new Vector2(GetComponent<BoxCollider2D>().size.x, normalColliderSize.y * crouchScale);
                StartCoroutine(CrouchTimer());
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
        else 
        {
            // Movement to the sides
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            if (horizontalInput != 0)
            {
                transform.position += new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0f, 0f);
                if (horizontalInput > 0)
                {
                    spriteRender.flipX = false;
                    spriteRender.sprite = PowerupRunnigSprite;
                }
                else
                {
                    spriteRender.flipX = true;
                    spriteRender.sprite = PowerupRunnigSprite;
                }

            }
            else
            {
                if (!isJumping)
                {
                    spriteRender.sprite = PowerupIdleSprite;
                }
                else
                {
                    spriteRender.sprite = PowerupjumpingSprite;
                }
                
            }

            // Jumping
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping && !isCrouching)
            {
                isJumping = true;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                spriteRender = GetComponent<SpriteRenderer>();
                spriteRender.sprite = PowerupjumpingSprite;

            }

            // Crouching
            if (Input.GetKeyDown(KeyCode.LeftControl) && !isJumping && !isCrouching)
            {
                isCrouching = true;
                GetComponent<BoxCollider2D>().size = new Vector2(GetComponent<BoxCollider2D>().size.x, normalColliderSize.y * crouchScale);
                StartCoroutine(CrouchTimer());
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
    }

    IEnumerator CrouchTimer()
    {
        yield return new WaitForSeconds(crouchDuration);
        isCrouching = false;
        GetComponent<BoxCollider2D>().size = normalColliderSize;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isJumping = false;
    }
}
