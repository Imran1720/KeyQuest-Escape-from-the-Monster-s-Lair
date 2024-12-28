using TMPro;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    int score = 0;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        RefreshScore(score);
    }

    public void IncreaseKeyCount(int value)
    {
        score += value;

        RefreshScore(score);
    }

    private void RefreshScore(int score)
    {
        scoreText.text = score + "X";
    }
}
