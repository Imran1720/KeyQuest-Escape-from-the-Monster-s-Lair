using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float xInput, yInput;

    public Animator myAnim;

    public BoxCollider2D playerCollider;
    [Header("Box Collider Data")]
    Vector2 defaultOffset;
    Vector2 defaultSize;
    public Vector2 crouchOffset;
    public Vector2 crouchSize;

    // Start is called before the first frame update
    void Start()
    {
        //boxcollider
        defaultOffset = new Vector2(playerCollider.offset.x, playerCollider.offset.y);
        defaultSize = new Vector2(playerCollider.size.x, playerCollider.size.y);
    }

    //crouch boxcollider offset - .61 sizey - 1.34

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        Move();
        //jump check
        myAnim.SetBool("IsJumping", yInput > 0);

        //Crouching mechanism
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            myAnim.SetBool("IsCrouching", true);
            playerCollider.offset = crouchOffset;
            playerCollider.size = crouchSize;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            myAnim.SetBool("IsCrouching", false);
            playerCollider.offset = defaultOffset;
            playerCollider.size = defaultSize;
        }

        //flip character
        Flip();
    }

    private void FixedUpdate()
    {

    }

    public void Move()
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