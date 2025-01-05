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

    //On triggering with the player Increases the Key count and destroys itself(Gameobject)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            SoundManager.Instance.PlaySFXSound(Sounds.Collect);
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.PlayClaimEffect();
            player.GetKey(keyCount);

            Destroy(gameObject);
        }
    }
    private void Update()
    {
        MoveKey();
        FlipDirection();
    }

    //Method to Move Key Up and down between two values
    private void MoveKey()
    {
        transform.Translate(new Vector3(0, facingDirection * moveSpeed * Time.deltaTime, 0));
    }

    //Changing the Direction of key movement if one extremee point is reached
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
