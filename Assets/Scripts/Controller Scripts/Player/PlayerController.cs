using System.Collections;
using UnityEngine;

public class PlayerController : Controller
{
    //public static PlayerController instance;

    private float xInput, yInput;

    [Header("Player Info")]
    [SerializeField]
    private int playerHealth;
    [SerializeField]
    private GameObject playerSpawnPoint;
    private bool gothit = false;

    [Header("Movement Info")]
    [SerializeField]
    private float pushbackForce;
    [SerializeField]
    private float jumpForce;


    [Header("Box Collider Info")]
    public BoxCollider2D playerCollider;
    Vector2 defaultOffset;
    Vector2 defaultSize;
    [SerializeField]
    private Vector2 crouchOffset;
    [SerializeField]
    private Vector2 crouchSize;

    [SerializeField]
    private ParticleSystem claimEffect;

    private bool leftFoot = false;
    private bool canPlayLandedAudio = false;


    private void Update()
    {
        IsPlayerAlive();
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Jump");
        if (!gothit)
        {
            PlayerMovement();
            SetCharacterAnimation();
            Flip();
            Crouch();
        }
        if (!CheckGrounded())
        {
            canPlayLandedAudio = true;
        }

    }

    protected override void InitializeData()
    {
        base.InitializeData();

        soundManager.PlaySFXSound(Sounds.LevelEnter);
        //Setting Default player collider size
        defaultOffset = playerCollider.offset;
        defaultSize = playerCollider.size;
        gothit = false;//this variable is used to play hit animation and temporarily disables player movement
        RespawnPlayer();//Ensuring Player starts from spwan point everytime level starts
    }

    //Checking if Player can Crouch and setting up the player collider size accordingly
    private void Crouch()
    {
        //Making Player Crounch on CTRL key press and hold
        if (CheckGrounded() && Input.GetKeyDown(KeyCode.LeftControl))
        {
            ZeroVelocity();//Allowing player to jump while Crouching
            characterAnimator.SetBool("IsCrouching", true);
            SetCollider(crouchOffset, crouchSize);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))//making player stand if CTRL key is released
        {
            characterAnimator.SetBool("IsCrouching", false);
            SetCollider(defaultOffset, defaultSize);
        }
    }

    //Method to Change the Size and Offset of PlayerCollider
    private void SetCollider(Vector2 _targetOffset, Vector2 _targetSize)
    {
        playerCollider.offset = _targetOffset;
        playerCollider.size = _targetSize;
    }

    //Setting Player Animations (Movement and  Jumping)
    protected override void SetCharacterAnimation()
    {

        //following Condition is used to set Player movement animation
        if (xInput != 0 && CheckGrounded())
        {
            characterAnimator.SetFloat("Speed", Mathf.Abs(moveSpeed * xInput));
        }
        else
        {
            characterAnimator.SetFloat("Speed", 0);
        }

        //Following condition is to set jump animation
        if (yInput > 0 && CheckGrounded())
        {
            characterAnimator.SetBool("IsJumping", true);
        }
        else
        {
            characterAnimator.SetBool("IsJumping", false);
        }

        characterAnimator.SetBool("OnGround", CheckGrounded());
        //Setting the Player Y-Velocity for fall animation
        characterAnimator.SetFloat("yVelocity", rb.velocity.y);
    }

    //Flipping Player Sprite based on Player Horizontal Movement
    protected override void Flip()
    {
        base.Flip();
        //if player is moving left then making scale x to -1(facing left)
        if (xInput < 0)
        {

            localScale.x = -1 * Mathf.Abs(localScale.x);
        }
        //if player is moving right then making scale x to 1(facing right)
        else if (xInput > 0)
        {
            localScale.x = Mathf.Abs(localScale.x);
        }

        transform.localScale = localScale;
    }

    //Applying velocity for Player Movement using SetVelocity() which uses Rigidbody2D
    void PlayerMovement()
    {
        //Horizontal Movement
        if (xInput != 0 && !characterAnimator.GetBool("IsCrouching"))
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
            SetVelocity(rb.velocity.x, jumpForce);
        }
    }

    //Playing Jump Sound
    public void PlayJumpSounds()
    {
        soundManager.PlaySFXSound(Sounds.Jump);
    }

    //Playing Land Sound
    public void PlayLandedSounds()
    {
        if (canPlayLandedAudio && CheckGrounded())
        {
            soundManager.PlaySFXSound(Sounds.Land);
            canPlayLandedAudio = false;
        }
    }
    //Decreasing Player Health and Updating Player Health UI
    public void DecreasePlayerHealth()
    {
        if (playerHealth > 0)
        {
            playerHealth--;
            gothit = true;
            characterAnimator.SetBool("Hit", gothit);
            levelUIManager.RefreshHealth(playerHealth);
            //playerAnimator.SetBool("Hit", false);
        }
        else
        {
            PlayerDead();
        }
    }

    public void PushBack(EnemyController _enemy)
    {
        gothit = true;
        ZeroVelocity();
        Vector2 knockback = (transform.position - _enemy.transform.position).normalized;
        rb.AddForce(knockback * pushbackForce, ForceMode2D.Impulse);
        StartCoroutine(continueMovement());
    }

    //coroutine to make all player movement enabled after knockback 
    IEnumerator continueMovement()
    {
        yield return new WaitForSeconds(.4f);
        gothit = false;
        characterAnimator.SetBool("Hit", gothit);
    }

    //Checking Player Health if Zero Poping GameOver Screen
    private void IsPlayerAlive()
    {
        if (playerHealth <= 0)
        {
            characterAnimator.SetBool("PlayerDie", true);
            this.enabled = false;
        }
    }

    //Respawing Player to spawn position after call
    public void RespawnPlayer()
    {
        if (playerHealth > 0)
        {
            soundManager.PlaySFXSound(Sounds.LevelEnter);
            transform.position = playerSpawnPoint.transform.position;
        }
        gothit = false;
        characterAnimator.SetBool("Hit", gothit);
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

    public void ZeroVelocity()
    {
        SetVelocity(0, 0);
    }

    public void PlayerDead()
    {
        characterAnimator.SetBool("PlayerDie", true);
        soundManager.SetVolume(.4f);
        soundManager.PlaySFXSound(Sounds.GameLoose);
        levelUIManager.OpenGameOverMenu();
    }

    public void PlayFootSound()
    {
        if (leftFoot)
        {
            soundManager.PlaySFXSound(Sounds.playerFootStep_1);
        }
        else
        {
            soundManager.PlaySFXSound(Sounds.playerFootStep_2);
        }
        leftFoot = !leftFoot;
    }
}
