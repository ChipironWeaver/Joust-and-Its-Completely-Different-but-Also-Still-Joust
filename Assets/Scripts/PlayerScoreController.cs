using System;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class PlayerScoreController : MonoBehaviour
{
    public int playerScore;
    [SerializeField,Required] private TextMeshProUGUI _scoreText;

    public void OnEnable()
    {
        Actions.Lose += () => {LevelManager.Instance.playerScores.Add(playerScore); };
        Actions.Win += () => {LevelManager.Instance.playerScores.Add(playerScore); };
    }

    public void OnDisable()
    {
        Actions.Lose -= () => {LevelManager.Instance.playerScores.Add(playerScore); };
        Actions.Win -= () => {LevelManager.Instance.playerScores.Add(playerScore); };
    }

    public void OnDestroy()
    {
        if (_scoreText != null)
        {
            Destroy(_scoreText.gameObject);
        }
        LevelManager.Instance?.playerScores.Add(playerScore);
    }

    public void AddScore(int score, Vector2 position = default)
    {
        playerScore += score;
        if (position != default)
        {
            print(position.ToString());
        }
        RenderScore(playerScore);
    }

    private void RenderScore(int score)
    {
        _scoreText.text = score.ToString();
    }
}
