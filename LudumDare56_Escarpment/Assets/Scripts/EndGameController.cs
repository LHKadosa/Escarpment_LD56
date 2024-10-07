using System;
using UnityEngine;
using TMPro;

public class EndGameController : MonoBehaviour
{
    [SerializeField] GameObject endGameMenu;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] string winMessage = "Mission completed!";
    [SerializeField] string loseMessage = "Game Over...";

    private void OnEnable()
    {
        PlayerHealthScript.OnGameEnd += ShowMenu;
    }

    private void OnDisable()
    {
        PlayerHealthScript.OnGameEnd -= ShowMenu;
    }

    public void ShowMenu(bool hasWon, GameScore gameScore)
    {
        endGameMenu.SetActive(true);

        if (hasWon)
        {
            title.text = winMessage;
        }
        else
        {
            title.text = loseMessage;
        }

        if (score != null)
        {
            ShowScore(gameScore);
        }
    }

    public void HideMenu()
    {
        endGameMenu.SetActive(false);
    }
    
    private void ShowScore(GameScore gameScore)
    {
        string displayText = "";

        if (gameScore.score.HasValue)
        {
            displayText += $"Score: {gameScore.score.Value}\n";
        }

        if (gameScore.timeTaken.HasValue)
        {
            displayText += $"Time: {gameScore.timeTaken.Value} seconds";
        }

        score.text = displayText.TrimEnd();
    }
}
