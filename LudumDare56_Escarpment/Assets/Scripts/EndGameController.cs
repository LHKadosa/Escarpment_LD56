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
        // TODO: Subscribe to the action from wherever it is triggered
        //PlayerInputHandler.OnGameEnd += ShowMenu;
    }

    private void OnDisable()
    {
        // TODO: Unsubscribe
        //PlayerInputHandler.OnGameEnd -= ShowMenu;
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
            score.text = $"Score: {gameScore.score}\nTime: {gameScore.timeTaken} seconds";
        }
    }

    public void HideMenu()
    {
        endGameMenu.SetActive(false);
    }
}
