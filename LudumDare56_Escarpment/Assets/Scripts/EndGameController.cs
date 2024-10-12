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

    private int totalEnemies;

    private void OnEnable()
    {
        PlayerHealthScript.OnGameEnd += ShowMenu;
        Scientist.OnGameVictory += ShowMenu;
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy1").Length;
    }

    private void OnDisable()
    {
        PlayerHealthScript.OnGameEnd -= ShowMenu;
        Scientist.OnGameVictory -= ShowMenu;
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

        GameScore.destroyedEnemies = 0;
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
            
            float percentage = (float)gameScore.score.Value / totalEnemies * 100;
            string rank = GetRankBasedOnPercentage(percentage);

            displayText += $"Criatures burnt: {gameScore.score.Value}/{totalEnemies}\n";
            displayText += $"Rank: {rank}\n";
        }

        if (gameScore.timeTaken.HasValue)
        {
            displayText += $"Time: {gameScore.timeTaken.Value:F1} seconds";
        }

        score.text = displayText.TrimEnd();
    }

    private string GetRankBasedOnPercentage(float percentage)
    {
        if (percentage >= 100) return "Perfect";
        else if (percentage >= 75) return "Great";
        else if (percentage >= 50) return "Good";
        else return "Poor";
    }
}
