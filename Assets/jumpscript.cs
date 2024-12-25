using UnityEngine;

public class jumpscript : MonoBehaviour
{

    public float jumpforce;
    public Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
        }
    }
}
