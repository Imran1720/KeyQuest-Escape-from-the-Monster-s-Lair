using UnityEngine;

public class KeyController : MonoBehaviour
{
    private int facingDirection = 1;
    private int key = 0;
    [SerializeField]
    private float moveSpeed;
    private Vector3 startpos;
    private float maxYPosition;


    private LevelManager levelManager;
    private LevelUIManager levelUIManager;

    private void Start()
    {
        InitializeData();
    }

    private void InitializeData()
    {
        startpos = transform.position;
        levelManager = LevelManager.Instance;
        levelUIManager = LevelUIManager.Instance;
        maxYPosition = startpos.y + .3f;
        levelUIManager.RefreshScore(key);
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
        if (transform.position.y > maxYPosition)
        {
            facingDirection = -1;
        }
        else if (transform.position.y < startpos.y)
        {
            facingDirection = 1;
        }
    }

    //On triggering with the player Increases the Key count and destroys itself(Gameobject)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.PlayClaimEffect();
            levelManager.CollectKey();
            Destroy(gameObject);
        }
    }

}
