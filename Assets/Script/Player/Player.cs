using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float runSpeed = 10f;
    [SerializeField] public float jumpSpeed = 10f;
    [SerializeField] Vector2 deathkick = new Vector2(0f, 10f);
    float xAxis;

    [Header("Component")]
    public Rigidbody2D rb2d;
    CapsuleCollider2D capsuleCollider;

    [Header("Ground Check")]
    [SerializeField] LayerMask groundMask;
    [SerializeField] Transform groundCheck;
    bool isGrounded;
    bool isDoubleJump;
    public bool isAlive;

    [Header("Obstacle Check")]
    [SerializeField] LayerMask ObstacleMask;
    bool isObstacle;

    // Animation state
    Animator animator;
    string currentState;
    const string PLAYER_IDLE = "Player_Idle";
    const string PLAYER_RUN = "Player_Run";
    const string PLAYER_JUMP = "Player_Jump";
    const string PLAYER_DOUBLEJUMP = "Player_DoubleJump";
    const string PLAYER_FALL = "Player_Fall";
    const string PLAYER_DIE = "Hit";

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        isAlive = true;
    }

    void Update()
    {
        Run();
        Jump();
        Hit();
        Buff();
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }

    void Run()
    {
        xAxis = Input.GetAxisRaw("Horizontal");

        rb2d.velocity = new Vector2(xAxis * runSpeed, rb2d.velocity.y);

        if (xAxis > 0.001)
        {
            transform.localScale = new Vector2(1f, 1f);
        }
        else if (xAxis < -0.001)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }

        if (isGrounded && rb2d.velocity.y <= 0.001f || isObstacle && rb2d.velocity.y <= 0.001f)
        {
            if (xAxis != 0)
            {
                ChangeAnimationState(PLAYER_RUN);
            }
            else
            {
                ChangeAnimationState(PLAYER_IDLE);
            }
        }
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.3f, 0.04f), CapsuleDirection2D.Horizontal, 0, groundMask);
        isObstacle = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.3f, 0.04f), CapsuleDirection2D.Horizontal, 0, ObstacleMask);

        if (Input.GetKeyDown(KeyCode.Space) && isAlive)
        {
            if (isGrounded || isObstacle)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
                ChangeAnimationState(PLAYER_JUMP);
                isDoubleJump = true;
            }
            else
            {
                if (isDoubleJump || isObstacle)
                {
                    rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed * 0.8f);
                    ChangeAnimationState(PLAYER_DOUBLEJUMP);
                    isDoubleJump = false;
                }
            }
        }
        else
        {
            return;
        }

        if (!isGrounded && rb2d.velocity.y < -0.1f || !isObstacle && rb2d.velocity.y < -0.1f)
        {
            ChangeAnimationState(PLAYER_FALL);
        }
    }

    void Hit()
    {
        if (capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Trap")) && isAlive)
        {
            rb2d.velocity = deathkick;
            capsuleCollider.enabled = false;
            rb2d.constraints = RigidbodyConstraints2D.FreezePositionX;
            ChangeAnimationState(PLAYER_DIE);
            isAlive = false;
            FindObjectOfType<CameraController>().virtualCamera.Follow = null;
            StartCoroutine(Respawn());
            return;
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSecondsRealtime(2f);
        FindObjectOfType<CameraController>().virtualCamera.Follow = FindObjectOfType<PlayerController>().SpawnPlayer().transform;
        Destroy(this.gameObject);
        isAlive = true;
    }

    void Buff()
    {
        if (capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Buff")) && isAlive)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed + 2);
        }
    }
}