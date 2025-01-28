using UnityEngine;

public class InteractableObjects : MonoBehaviour
{
    protected SoundManager soundManager;
    protected UIManager uiManager;

    // Initialize singleton references in the Start method
    // This ensures that both UIManager and SoundManager are initialized only once
    // and are accessible to all derived classes (Door, Key, Spike) without the need for re-initialization.
    protected virtual void Start()
    {
        //Initializing soundManager and uiManager
        if (soundManager == null)
        {
            soundManager = SoundManager.Instance;
        }
        if (uiManager == null)
        {
            uiManager = UIManager.Instance;
        }
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
