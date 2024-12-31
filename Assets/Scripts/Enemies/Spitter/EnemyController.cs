using System;
using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator enemyAnimator;

    [Header("Movement Info")]
    public float moveSpeed;
    int facingDirection = 1;
    bool isGroundDetected;
    public float idleTime;


    [Header("Ground Detection info")]
    public float detectionLenth;
    public Transform detectionPoint;
    public LayerMask whatIsGround;


    // Update is called once per frame
    private void Start()
    {
        isGroundDetected = true;
    }
    void Update()
    {
        enemyAnimator.SetFloat("MoveSpeed", rb.velocity.x);

        CheckCanMove();
        if (!isGroundDetected)
        {
            SetVelocity(0, 0);
            StartCoroutine(ChangeDirection());
        }

        SetEnemyAnimations();
    }

    private void SetEnemyAnimations()
    {
        enemyAnimator.SetFloat("MoveSpeed", MathF.Abs(rb.velocity.x));
    }

    private void CheckCanMove()
    {

        isGroundDetected = Physics2D.Raycast(detectionPoint.transform.position, Vector2.down, detectionLenth, whatIsGround);
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        if (facingDirection == 1)
        {
            facingDirection = -1;
            scale.x = -1 * Mathf.Abs(facingDirection);
        }
        else if (facingDirection == -1)
        {
            facingDirection = 1;
            scale.x = Mathf.Abs(facingDirection);
        }
        transform.localScale = scale;
    }

    private void FixedUpdate()
    {
        if (isGroundDetected)
        {
            SetVelocity(moveSpeed * facingDirection, 0);
        }

    }

    IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(idleTime);

        Flip();
        isGroundDetected = true;
        StopAllCoroutines();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 target = detectionPoint.transform.position - (Vector3.up * detectionLenth);
        Gizmos.DrawLine(detectionPoint.transform.position, target);
    }


    //collision detection
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            collision.gameObject.GetComponent<PlayerController>().DecreasePlayerHealth();
        }
    }
}
