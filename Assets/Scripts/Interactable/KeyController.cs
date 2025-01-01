using UnityEngine;

public class KeyController : MonoBehaviour
{

    public int keyCount;
    int facingDirection = 1;
    public float moveSpeed;

    Vector3 startpos;
    private void Start()
    {
        startpos = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.GetKey(keyCount);

            Destroy(gameObject);
        }
    }
    private void Update()
    {
        transform.Translate(new Vector3(0, facingDirection * moveSpeed * Time.deltaTime, 0));
        FlipDirection();
    }

    void FlipDirection()
    {
        if (transform.position.y > startpos.y + .3f)
        {
            facingDirection = -1;
        }
        else if (transform.position.y < startpos.y)
        {
            facingDirection = 1;
        }
    }

}
