using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float xInput, yInput;
    [Header("Components")]
    public Rigidbody2D rb;
    public Animator playerAnimator;

    [Header("Player Info")]
    public int playerHealth;
    public GameObject playerSpawnPoint;

    [Header("Movement Info")]
    public float moveSpeed;

    public KeyTracker tracker;

    public float jumpForce;



    [Header("Box Collider Info")]
    public BoxCollider2D playerCollider;
    Vector2 defaultOffset;
    Vector2 defaultSize;
    public Vector2 crouchOffset;
    public Vector2 crouchSize;

    [Header("Ground Check Info")]
    public float groundCheckDistance;
    public LayerMask whatIsGround;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        defaultOffset = playerCollider.offset;
        defaultSize = playerCollider.size;
        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Jump");
        PlayerMovement();
        Flip();
        Crouch();
        SetPlayerAnimation();

        if (playerHealth <= 0)
        {
            LevelUIManager.Instance.OpenGameOverMenu();
        }
    }

    private void Crouch()
    {
        if (CheckGrounded() && Input.GetKeyDown(KeyCode.LeftControl))
        {
            SetVelocity(0, yInput);
            playerAnimator.SetBool("IsCrouching", true);
            playerCollider.offset = crouchOffset;
            playerCollider.size = crouchSize;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            playerAnimator.SetBool("IsCrouching", false);
            playerCollider.offset = defaultOffset;
            playerCollider.size = defaultSize;
        }
    }

    private void SetPlayerAnimation()
    {
        if (xInput != 0 && CheckGrounded())
        {
            playerAnimator.SetFloat("Speed", Mathf.Abs(rb.velocity.x * xInput));
        }
        else
        {
            playerAnimator.SetFloat("Speed", 0);
        }

        if (yInput > 0 && CheckGrounded())
        {
            playerAnimator.SetBool("IsJumping", true);
        }
        else
        {
            playerAnimator.SetBool("IsJumping", false);
        }
    }

    bool CheckGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;

        if (xInput < 0)
        {
            scale.x = -1 * Mathf.Abs(scale.x);
        }
        else if (xInput > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }

        transform.localScale = scale;
    }

    void PlayerMovement()
    {

        if (xInput != 0 && !playerAnimator.GetBool("IsCrouching"))
        {
            SetVelocity(xInput * moveSpeed, rb.velocity.y);
        }
        else
        {
            SetVelocity(0, rb.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.Space) && CheckGrounded())
        {

            SetVelocity(rb.velocity.x, jumpForce);
        }
    }

    void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
    }

    public void GetKey(int value)
    {
        tracker.IncreaseKeyCount(value);
    }

    public void DecreasePlayerHealth()
    {
        if (playerHealth > 0)
        {
            playerHealth--;

            LevelUIManager.Instance.RefreshHealth(playerHealth);
        }
    }

    public void RespawnPLayer()
    {
        transform.position = playerSpawnPoint.transform.position;
    }
}