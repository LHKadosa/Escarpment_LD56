using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameController : MonoBehaviour
{
    [SerializeField] GameObject endGameMenu;
    [SerializeField] DialogController dialogController;
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
        if (hasWon)
        {
            title.text = winMessage;
            ShowWinDialog();
        }
        else
        {
            title.text = loseMessage;
        }

        if (score != null)
        {
            ShowScore(gameScore);
        }

        endGameMenu.SetActive(true);
    }

    public void HideMenu()
    {
        endGameMenu.SetActive(false);
    }
    
    private void ShowWinDialog()
    {
        List<string> startDialog = new List<string>
        {
            "Well, look at that! You actually did it!",
            "Those tiny creatures didn’t stand a chance, huh?",
            "Well done finding the scientist! Or what's left of it..."
        };

        dialogController.StartDialog(startDialog);
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
            displayText += $"Time: {gameScore.timeTaken.Value:F1} seconds";
        }

        score.text = displayText.TrimEnd();
    }
}
