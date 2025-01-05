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
    public int facingDirection = 1;
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
        //Checking if enemy can move and set velocity
        CheckCanMove();
        if (isGroundDetected)
        {
            SetVelocity(moveSpeed * facingDirection, rb.velocity.y);
        }
        else
        {
            //Making horizontal moment to Zero and applying velocity downward  
            SetVelocity(0, -2);
            //Changing the current direction and making enemy move
            StartCoroutine(ChangeDirection());
        }
        //method to set movement animation
        SetEnemyAnimations();
    }

    //Setting enemy movement animations
    private void SetEnemyAnimations()
    {
        enemyAnimator.SetFloat("MoveSpeed", MathF.Abs(rb.velocity.x));
    }

    //Checking if ground is dected on detection point or not
    private void CheckCanMove()
    {
        isGroundDetected = Physics2D.Raycast(detectionPoint.transform.position, Vector2.down, detectionLenth, whatIsGround);
    }


    //Flipping the enemy sprite when called
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

    //After waiting 'idleTime' amount of time flips the sprite and start moving
    IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(idleTime);

        Flip();
        isGroundDetected = true;
        StopAllCoroutines();
    }

    //Enemy movement using Rigidbody2D
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
    }


    //Drawing line at detection point to check ground exist and rotate sprite if not
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 target = detectionPoint.transform.position - (Vector3.up * detectionLenth);
        Gizmos.DrawLine(detectionPoint.transform.position, target);
    }


    //collision detection
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            SoundManager.Instance.PlaySFXSound(Sounds.EllenHurt);
            collision.gameObject.GetComponent<PlayerController>().DecreasePlayerHealth();
            collision.gameObject.GetComponent<PlayerController>().PushBack(this);
        }
    }

}
