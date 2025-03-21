using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    // Component References
    private BoxCollider2D playerBox;
    private Rigidbody2D playerRb;
    private Animator anim;
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float jumpHeight = 5;
    private bool doubleJump;

    [Header("Class References")]
    public CoinManage cm;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask bossLayer;

    [Header("Attack")]
    public float damage = 5f;
    public GameObject attackArea;
    public float radius;

    [Header("Running")]
    private float runningSpeed = 1.5f;

    [SerializeField] private float height;
    [SerializeField] private float width;
    [SerializeField] private float depth;

    AudioSource jumpAudio;

    [Header("Dash")]
    [SerializeField] private float dashPower = 5f;
    [SerializeField] private float dashDuration = 0.1f;
    [SerializeField] private float dashCooldown = 1.5f;
    [SerializeField] private float IFramesDuration;
    private bool isDashing;
    private bool canDash = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerBox = GetComponent<BoxCollider2D>();
        playerRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerBox = GetComponent<BoxCollider2D>();
        jumpAudio = GetComponent<AudioSource>();
        doubleJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing) { return; }

        float movement = Input.GetAxis("Horizontal");
        playerRb.linearVelocity = new Vector2(movement * moveSpeed, playerRb.linearVelocityY);

        // Walking Function
        if (movement > 0.01f)
        {
            transform.localScale = new Vector2(Math.Abs(transform.localScale.x), transform.localScale.y);
        }
        else if (movement < -0.01f)
        {
            transform.localScale = new Vector2(-Math.Abs(transform.localScale.x), transform.localScale.y);
        }

        if (Input.GetKey(KeyCode.LeftShift) && isGrounded())
        {
            playerRb.linearVelocity = new Vector2(movement * moveSpeed * runningSpeed, playerRb.linearVelocity.y);
        }
        else
        {
            playerRb.linearVelocity = new Vector2(movement * moveSpeed, playerRb.linearVelocity.y);
        }


        // Jumping Function
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            jumpAudio.Play();
            doubleJump = true;
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isGrounded() && doubleJump)
        {
            jumpAudio.Play();
            doubleJump = false;
            anim.SetTrigger("useDoubleJump");
            Jump();
        }
        if (isGrounded())
        {
            doubleJump = true;
        }

        // Dash Function
        if (Input.GetKeyDown(KeyCode.Q) && canDash)
        {
            Debug.Log("Dash");
            StartCoroutine(DoDash());
        }


        // Animations
        anim.SetBool("onGround", isGrounded());

        // Checks the Movement
        if (movement == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }
    }

    // Dashing Functions
    private IEnumerator DoDash()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        Physics2D.IgnoreLayerCollision(6, 9, true);
        Physics2D.IgnoreLayerCollision(6, 10, true);
        Physics2D.IgnoreLayerCollision(6, 11, true);

        canDash = false;
        isDashing = true;
        float originalGrav = playerRb.gravityScale;
        playerRb.gravityScale = 0f;
        playerRb.linearVelocity = new Vector2(transform.localScale.x * dashPower, 0f);
        yield return new WaitForSeconds(dashDuration);

        playerRb.gravityScale = originalGrav;
        isDashing = false;
        yield return new WaitForSeconds(IFramesDuration);
        Physics2D.IgnoreLayerCollision(6, 7, false);
        Physics2D.IgnoreLayerCollision(6, 9, false);
        Physics2D.IgnoreLayerCollision(6, 10, false);
        Physics2D.IgnoreLayerCollision(6, 11, false);
        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }

    // Checks for Collisions
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Door") && cm.fragCount == 1)
        {
            SceneManager.LoadScene("Video2");
        }
        else if (other.gameObject.CompareTag("Door_2") && cm.fragCount == 1)
        {
            SceneManager.LoadScene("Video3");
        }
        else if (other.gameObject.CompareTag("Door_3") && cm.fragCount == 1)
        {
            SceneManager.LoadScene("Video4");
        }
        else if (other.gameObject.CompareTag("Door_4") && cm.fragCount == 1)
        {
            SceneManager.LoadScene("Video5");
        }
        else if (other.gameObject.CompareTag("Door_5") && cm.fragCount == 1)
        {
            SceneManager.LoadScene("Video6");
        }
    }

    // Checks for Ground
    private bool isGrounded()
    {
        RaycastHit2D onGround = Physics2D.BoxCast(playerBox.bounds.center, playerBox.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return onGround.collider != null;
    }

    // Jumps
    private void Jump()
    {
        playerRb.linearVelocity = new Vector2(playerRb.linearVelocityX, jumpHeight);
    }

    // Checks for Trigger Events
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Fragment"))
        {
            Destroy(other.gameObject);
            cm.fragCount++;
        }
    }
}
