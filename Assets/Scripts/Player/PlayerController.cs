using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public static PlayerController instance;

    float xInput, yInput;
    [Header("Components")]
    public Rigidbody2D rb;
    public Animator playerAnimator;

    [Header("Player Info")]
    public int playerHealth;
    public GameObject playerSpawnPoint;
    bool gothit = false;

    [Header("Movement Info")]
    public float moveSpeed;
    public float pushbackForce;
    public KeyTracker tracker;

    public float jumpForce;
    bool canPlayJumpAudio = false, canPlayLandedAudio = false;


    [Header("Box Collider Info")]
    public BoxCollider2D playerCollider;
    Vector2 defaultOffset;
    Vector2 defaultSize;
    public Vector2 crouchOffset;
    public Vector2 crouchSize;

    [Header("GroundCheck Info")]
    public float groundCheckDistance;
    public LayerMask whatIsGround;


    public ParticleSystem claimEffect;

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}
    private void Start()
    {

        RespawnPlayer();//Ensuring Player starts from spwan point everytime level starts
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        SoundManager.Instance.PlaySFXSound(Sounds.LevelEnter);
        //Setting Default player collider size
        defaultOffset = playerCollider.offset;
        defaultSize = playerCollider.size;
        gothit = false;
        Time.timeScale = 1.0f;//Ensuring time flow of game is normal
    }

    private void Update()
    {
        IsPlayerAlive();
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Jump");
        if (!gothit)
        {
            PlayerMovement();
            SetPlayerAnimation();
            CheckToPlayJumpAudio();
            Flip();
            Crouch();
        }
        Debug.Log("Before hit" + playerAnimator.GetBool("Hit"));

    }



    //Conditions to play Jump and Land Sounds
    private void CheckToPlayJumpAudio()
    {
        if (!CheckGrounded())
        {
            canPlayLandedAudio = true;
        }
        if (canPlayLandedAudio && CheckGrounded())
        {
            PlayLandedSounds();
        }
    }

    //Checking if Player can Crouch and setting up the player collider size accordingly
    private void Crouch()
    {
        //Making Player Crounch on CTRL key press and hold
        if (CheckGrounded() && Input.GetKeyDown(KeyCode.LeftControl))
        {
            SetVelocity(0, yInput);//Allowing player to jump while Crouching
            playerAnimator.SetBool("IsCrouching", true);
            playerCollider.offset = crouchOffset;
            playerCollider.size = crouchSize;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))//making player stand if CTRL key is released
        {
            playerAnimator.SetBool("IsCrouching", false);
            playerCollider.offset = defaultOffset;
            playerCollider.size = defaultSize;
        }
    }

    //Setting Player Animations (Movement and  Jumping)
    private void SetPlayerAnimation()
    {
        //following Condition is used to set Player movement animation
        if (xInput != 0 && CheckGrounded())
        {
            playerAnimator.SetFloat("Speed", Mathf.Abs(rb.velocity.x * xInput));
        }
        else
        {
            playerAnimator.SetFloat("Speed", 0);
        }

        //Following condition is to set jump animation
        if (yInput > 0 && CheckGrounded())
        {
            playerAnimator.SetBool("IsJumping", true);
        }
        else
        {
            playerAnimator.SetBool("IsJumping", false);
        }

        //Setting the Player Y-Velocity for fall animation
        playerAnimator.SetFloat("yVelocity", rb.velocity.y);
    }



    //Checking if player is on Ground or not
    bool CheckGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }


    //Flipping Player Sprite based on Player Horizontal Movement
    void Flip()
    {
        Vector3 scale = transform.localScale;
        //if player is moving left then making scale x to -1(facing left)
        if (xInput < 0)
        {

            scale.x = -1 * Mathf.Abs(scale.x);
        }
        //if player is moving right then making scale x to 1(facing right)
        else if (xInput > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }

        transform.localScale = scale;
    }

    //Applying velocity for Player Movement using SetVelocity() which uses Rigidbody2D
    void PlayerMovement()
    {
        //Horizontal Movement
        if (xInput != 0 && !playerAnimator.GetBool("IsCrouching"))
        {
            SetVelocity(xInput * moveSpeed, rb.velocity.y);
        }
        else
        {
            SetVelocity(0, rb.velocity.y);
        }
        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && CheckGrounded())
        {
            canPlayJumpAudio = true;

            SetVelocity(rb.velocity.x, jumpForce);
            PlayJumpSounds();
        }
    }


    //Playing Jump Sound
    public void PlayJumpSounds()
    {
        if (canPlayJumpAudio && CheckGrounded())
        {
            canPlayJumpAudio = false;
            SoundManager.Instance.PlaySFXSound(Sounds.Jump);
        }
    }

    //Playing Land Sound
    public void PlayLandedSounds()
    {
        if (canPlayLandedAudio && CheckGrounded())
        {
            canPlayLandedAudio = false;
            SoundManager.Instance.PlaySFXSound(Sounds.Land);
        }
    }

    //Setting Player Velocity using Rigidbody2D
    void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
    }

    //Increasing KeyCount
    public void GetKey(int value)
    {
        tracker.IncreaseKeyCount(value);
    }


    //Decreasing Player Health and Updating Player Health UI
    public void DecreasePlayerHealth()
    {
        if (playerHealth > 0)
        {
            playerHealth--;
            gothit = true;
            playerAnimator.SetBool("Hit", gothit);
            LevelUIManager.Instance.RefreshHealth(playerHealth);
            //playerAnimator.SetBool("Hit", false);
        }
    }

    public void PushBack(EnemyController _enemy)
    {
        Vector2 knockback = (transform.position - _enemy.transform.position).normalized * pushbackForce;
        SetVelocity(knockback.x, knockback.y);

        StartCoroutine(continueMovement());
    }

    //coroutine to make all player movement enabled after knockback 
    IEnumerator continueMovement()
    {
        yield return new WaitForSeconds(.4f);
        gothit = false;
        playerAnimator.SetBool("Hit", gothit);
        Debug.Log("After hit" + playerAnimator.GetBool("Hit"));


    }

    //Checking Player Health if Zero Poping GameOver Screen
    private void IsPlayerAlive()
    {
        if (playerHealth <= 0)
        {
            playerAnimator.SetBool("PlayerDie", true);

            //StartCoroutine(PlayDieAnimation());
        }
    }

    /*Display GameoverScreen after PlayerDeath animation
    IEnumerator PlayDieAnimation()
    {
        yield return new WaitForSeconds(2f);
        playerAnimator.SetBool("PlayerDie", false);
        SoundManager.Instance.SetVolume(.05f);
        SoundManager.Instance.PlaySFXSound(Sounds.GameLoose);
        LevelUIManager.Instance.OpenGameOverMenu();
    }*/

    //Respawing Player to spawn position after call
    public void RespawnPlayer()
    {
        if (playerHealth > 0)
        {
            SoundManager.Instance.PlaySFXSound(Sounds.LevelEnter);
            transform.position = playerSpawnPoint.transform.position;
        }
        gothit = false;
        playerAnimator.SetBool("Hit", gothit);
    }

    //Playing the particle effect on collecting Key
    public void PlayClaimEffect()
    {
        StartCoroutine(ClaimEffect());
    }
    IEnumerator ClaimEffect()
    {
        claimEffect.Play();
        yield return new WaitForSeconds(.5f);
        claimEffect.Stop();
    }
}