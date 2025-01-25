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
    protected LevelUIManager levelUIManager;
    protected Vector3 localScale;

    private void Start()
    {
        InitializeData();
    }
    protected virtual void InitializeData()
    {
        soundManager = SoundManager.Instance;
        levelUIManager = LevelUIManager.Instance;
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