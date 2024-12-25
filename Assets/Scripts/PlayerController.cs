using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float xInput, yInput;

    public Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {

    }

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
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            myAnim.SetBool("IsCrouching", false);
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