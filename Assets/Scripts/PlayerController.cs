using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float xInput, yInput;

    [Header("Movement data")]
    public float moveSpeed;

    [Header("Animator data")]
    public Animator myAnim;

    [Header("Rigidbody data")]
    public Rigidbody2D rb;

    [Header("Box Collider Data")]
    public BoxCollider2D playerCollider;
    Vector2 defaultOffset;
    Vector2 defaultSize;
    public Vector2 crouchOffset;
    public Vector2 crouchSize;

    // Start is called before the first frame update
    void Start()
    {
        //boxcollider
        defaultOffset = playerCollider.offset;
        defaultSize = playerCollider.size;
    }

    //crouch boxcollider offset - .61 sizey - 1.34

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        //move animation
        StartMovementAnimation();

        //set jump animation
        myAnim.SetBool("IsJumping", yInput > 0);

        //Crouching mechanism
        Crouch();

        //flip character
        Flip();
    }

    private void FixedUpdate()
    {
        //move only if not crouching
        if (!myAnim.GetBool("IsCrouching"))
        {

            rb.velocity = (new Vector2(xInput, rb.velocity.y)).normalized * moveSpeed;
        }
        else//stoping movement if crouching
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void Crouch()
    {
        //crouch and shrink collider size if LEFT CTRL button pressed.
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            myAnim.SetBool("IsCrouching", true);
            playerCollider.offset = crouchOffset;
            playerCollider.size = crouchSize;
        }//enter coresponding state and change collider size if LEFT CTRL button released.
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            myAnim.SetBool("IsCrouching", false);
            playerCollider.offset = defaultOffset;
            playerCollider.size = defaultSize;
        }
    }
    public void StartMovementAnimation()
    {
        myAnim.SetFloat("Speed", Mathf.Abs(xInput));
    }
    public void Flip()
    {
        Vector3 scale = transform.localScale;
        Debug.Log(xInput < 0);
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

}