using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
using UnityEngine.UI;
#endif
[RequireComponent(typeof(Button))]
public class ButtonHandler : MonoBehaviour
{
    [Header("BUTTON")]
    [SerializeField]
    public ButtonType buttonType;
    private Button currentButton;

    [Header("PANELS")]
    [SerializeField]
    private GameObject screenToShow;
    [SerializeField]
    private GameObject screenToHide;

    [Header("LEVEL")]
    [SerializeField]
    private Levels levelToLoad;

    private SoundManager soundManager;
    private LevelManager levelManager;

    private void Start()
    {
        InitializeData();
    }

    //This method is responsible for initialization and resetting the data
    private void InitializeData()
    {
        currentButton = GetComponent<Button>();

        if (soundManager == null)
        {
            soundManager = SoundManager.Instance;
        }
        if (levelManager == null)
        {
            levelManager = LevelManager.Instance;
        }

        bool isButtonLevelLoader = (buttonType == ButtonType.LevelLoader);
        //Checking if Button is of type LevelLoader, to set it's Interaction.
        if (isButtonLevelLoader)
        {
            SetButtonInteraction();
        }
        currentButton.onClick.AddListener(PerformButtonAction);
    }

    /**
     * Performs actions Based on the Button Type "UILoader", "LevelLoader" and "Quit".
     * 
     * If Button is of type UILoader then it is responsible to Show Panel(ScreenToShow) and Hide Panel(ScreenToHide).
     * If Button is of Type LevelLoader then it is responsible to Load Levels Based on the index value(levelToLoad).
     * If Button is of Type Quit then it is responsible for closing the application or Game.
    **/
    public void PerformButtonAction()
    {
        switch (buttonType)
        {
            case ButtonType.Lobby:
                soundManager.PlaySFXSound(Sounds.ButtonClick);
                levelManager.LoadLobby();
                break;
            case ButtonType.UILoader:
                soundManager.PlaySFXSound(Sounds.ButtonClick);
                screenToHide.SetActive(false);
                screenToShow.SetActive(true);
                break;
            case ButtonType.LevelLoader:
                bool isLevelUnlocked = levelManager.GetLevelStatus((int)levelToLoad) != LevelStatus.locked;
                if (isLevelUnlocked)
                {
                    soundManager.PlaySFXSound(Sounds.LevelStart);
                    currentButton.interactable = true;
                    levelManager.LoadLevel((int)levelToLoad);
                }
                break;
            case ButtonType.Quit:
                soundManager.PlaySFXSound(Sounds.LevelStart);
                Application.Quit();
                break;
        }
    }

    /**
     * Makes the Button Interactable if the level it needs to load is Unlocked
     * isTargetLevelLocked - Checks if the level at index(levelToLoad) is locked or not.
     **/
    private void SetButtonInteraction()
    {
        bool isTargetLevelLocked = (levelManager.GetLevelStatus((int)levelToLoad) == LevelStatus.locked);
        if (isTargetLevelLocked)
        {
            currentButton.interactable = false;
        }
        else
        {
            currentButton.interactable = true;
        }
    }

}

public enum ButtonType
{
    Lobby,
    UILoader,
    LevelLoader,
    Quit
}

#if UNITY_EDITOR
[CustomEditor(typeof(ButtonHandler))]
public class CustomEditorButton : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        ButtonHandler buttonClick_Action = (ButtonHandler)target;

        EditorGUILayout.PropertyField(serializedObject.FindProperty("buttonType"));

        switch (buttonClick_Action.buttonType)
        {
            case ButtonType.UILoader:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("screenToShow"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("screenToHide"));

                break;
            case ButtonType.LevelLoader:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("levelToLoad"));
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}

#endif