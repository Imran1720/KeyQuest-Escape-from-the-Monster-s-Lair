using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
using UnityEngine.UI;
#endif
[RequireComponent(typeof(Button))]
public class ButtonClick_Action : MonoBehaviour
{

    [SerializeField]
    public ButtonType buttonType;
    private Button currentButton;

    [SerializeField]
    private GameObject screenToShow;
    [SerializeField]
    private GameObject screenToHide;

    private SoundManager soundManager;
    public Levels levelToLoad;

    private void Start()
    {
        soundManager = SoundManager.Instance;
        currentButton = GetComponent<Button>();
        if (LevelManager.Instance.GetLevelStatus((int)levelToLoad) == LevelStatus.locked && buttonType == ButtonType.Button_LevelLoader)
        {
            currentButton.interactable = false;
        }
        currentButton.onClick.AddListener(Perform_ButtonAction);
    }

    public void Perform_ButtonAction()
    {
        switch (buttonType)
        {
            case ButtonType.Button_UILoader:
                soundManager.PlaySFXSound(Sounds.ButtonClick);
                screenToHide.SetActive(false);
                screenToShow.SetActive(true);
                break;
            case ButtonType.Button_LevelLoader:
                if (LevelManager.Instance.GetLevelStatus((int)levelToLoad) != LevelStatus.locked)
                {
                    soundManager.PlaySFXSound(Sounds.LevelStart);
                    currentButton.interactable = true;
                    LevelManager.Instance.LoadLevel((int)levelToLoad);
                }
                break;
            case ButtonType.Quit:
                soundManager.PlaySFXSound(Sounds.LevelStart);
                Application.Quit();
                break;
        }
    }

}

public enum ButtonType
{
    None,
    Button_UILoader,
    Button_LevelLoader,
    Quit
}

#if UNITY_EDITOR
[CustomEditor(typeof(ButtonClick_Action))]
public class CustomEditor_Button : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        ButtonClick_Action buttonClick_Action = (ButtonClick_Action)target;

        EditorGUILayout.PropertyField(serializedObject.FindProperty("buttonType"));

        switch (buttonClick_Action.buttonType)
        {
            case ButtonType.Button_UILoader:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("screenToShow"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("screenToHide"));

                break;
            case ButtonType.Button_LevelLoader:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("levelToLoad"));
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}

#endif