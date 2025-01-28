using UnityEngine;

public class Key : InteractableObjects
{
    private int facingDirection = 1;
    [SerializeField]
    private float moveSpeed;
    private float minYPosition;
    private float maxYPosition;

    protected override void Start()
    {
        base.Start();
        InitializeData();
    }

    //This method is responsible to assign initial data
    // start pos 
    private void InitializeData()
    {
        minYPosition = transform.position.y;
        maxYPosition = minYPosition + .3f;
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
        else if (transform.position.y < minYPosition)
        {
            facingDirection = 1;
        }
    }

    //This method gets called when iteracted with player
    //This method is responsible to  perform certain actions when colliding with player
    protected override void PerformTriggerAction(PlayerController playerController)
    {
        soundManager.PlaySFXSound(Sounds.Collect);
        playerController.PlayClaimEffect();
        uiManager.CollectKey();
        Destroy(gameObject);
    }
}
