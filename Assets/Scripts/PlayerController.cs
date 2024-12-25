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
        myAnim.SetFloat("Speed", Mathf.Abs(xInput));
        Flip();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myAnim.SetBool("IsJumping", true);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            myAnim.SetBool("IsJumping", false);

        }
    }

    private void FixedUpdate()
    {

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