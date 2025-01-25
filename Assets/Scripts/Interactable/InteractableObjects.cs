using UnityEngine;

public class InteractableObjects : MonoBehaviour
{
    protected SoundManager soundManager;
    protected LevelManager levelManager;

    private void Start()
    {
        soundManager = SoundManager.Instance;
        levelManager = LevelManager.Instance;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            PerformTriggerAction(collision.GetComponent<PlayerController>());
        }
    }

    protected virtual void PerformTriggerAction(PlayerController playerController) { }
}
