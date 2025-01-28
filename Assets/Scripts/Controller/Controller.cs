using System;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Animator characterAnimator;

    [Header("Movement Info")]
    [SerializeField] protected float moveSpeed;

    [Header("Ground Detection info")]
    [SerializeField] protected float detectionLenth;
    [SerializeField] protected Transform detectionPoint;
    [SerializeField] protected LayerMask whatIsGround;
    protected bool isGroundDetected;

    protected SoundManager soundManager;
    protected UIManager uiManager;
    protected Vector3 localScale;

    private void Start()
    {
        InitializeData();
    }

    //This method is responsible for initialization and resetting the data
    protected virtual void InitializeData()
    {
        if (soundManager == null)
        {
            soundManager = SoundManager.Instance;
        }
        if (uiManager == null)
        {
            uiManager = UIManager.Instance;
        }
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        if (characterAnimator == null)
        {
            characterAnimator = GetComponent<Animator>();
        }

        Time.timeScale = 1.0f;//Ensuring time flow of game is normal
    }

    protected virtual void SetCharacterAnimation()
    {
        characterAnimator.SetFloat("MoveSpeed", MathF.Abs(rb.velocity.x));
    }

    //Checking if player is on Ground or not
    protected bool CheckGrounded()
    {
        return Physics2D.Raycast(detectionPoint.position, Vector2.down, detectionLenth, whatIsGround);
    }

    //Enemy movement using Rigidbody2D
    protected void SetVelocity(float xVelocity, float yVelocity)
    {
        //Debug.Log(rb == null);
        rb.velocity = new Vector2(xVelocity, yVelocity);

    }

    protected virtual void Flip()
    {
        localScale = transform.localScale;
    }

    //Drawing line at detection point to check ground exist and rotate sprite if not
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 target = detectionPoint.transform.position - (Vector3.up * detectionLenth);
        Gizmos.DrawLine(detectionPoint.transform.position, target);
    }
}