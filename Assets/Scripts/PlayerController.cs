using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;

    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;

    private bool isGrounded;

    public bool PlayerCanMove
    {
        get;
        set;
    }
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool IsGrounded;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        PlayerCanMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerCanMove)
        {
            MovePlayer();
        }
        else
        {
            rb2d.velocity = Vector2.zero;
            return;
        }
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);
    }

    private void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(horizontalInput * moveSpeed, rb2d.velocity.y);
        if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        }

        if (isGrounded && Input.GetKeyDown(jump))
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }
    }
}