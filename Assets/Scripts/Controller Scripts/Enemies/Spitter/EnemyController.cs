using System;
using System.Collections;
using UnityEngine;

public class EnemyController : Controller
{
    [Header("Movement Info")]
    private int facingDirection = 1;
    [SerializeField]
    private float idleTime;

    // Update is called once per frame

    void Update()
    {
        //Checking if enemy can move and set velocity
        //CheckCanMove();
        if (CheckGrounded())
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
        characterAnimator.SetFloat("MoveSpeed", MathF.Abs(rb.velocity.x));
    }

    //Flipping the enemy sprite when called
    protected override void Flip()
    {
        base.Flip();
        if (facingDirection == 1)
        {
            facingDirection = -1;
            localScale.x = -1 * Mathf.Abs(facingDirection);
        }
        else if (facingDirection == -1)
        {
            facingDirection = 1;
            localScale.x = Mathf.Abs(facingDirection);
        }
        transform.localScale = localScale;
    }

    //After waiting 'idleTime' amount of time flips the sprite and start moving
    IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(idleTime);
        Flip();
        StopAllCoroutines();
    }

    //collision detection
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

            soundManager.PlaySFXSound(Sounds.EllenHurt);
            playerController.DecreasePlayerHealth();
            playerController.PushBack(this);
        }
    }
}
