using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;



#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour
{
#if UNITY_EDITOR
    public SceneAsset gameScene;
#endif
    public Animator mainMenuAnimator;
    public Animator settingsMenuAnimator;
    public Animator pauseMenuAnimator;
    public GameObject settingsMenu;
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    private void OnEnable()
    {
        PlayerInputHandler.OnPausePressed += HandlePausePressed;
    }

    private void OnDisable()
    {
        PlayerInputHandler.OnPausePressed -= HandlePausePressed;
    }

    private void HandlePausePressed()
    {
        if (isPaused) {
            ResumeGame();
        } else {
            PauseGame();
        }
    }

    public void StartGame()
    {
#if UNITY_EDITOR
        if (gameScene != null) {
            SceneManager.LoadScene(gameScene.name);
        } else {
            Debug.LogError("No scene is assigned!");
        }
#else
        SceneManager.LoadScene("S_Kadosa_LevelDesign");
#endif
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        pauseMenuAnimator.SetBool("openPauseMenu", false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        pauseMenuAnimator.SetBool("openPauseMenu", true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f;
        StartCoroutine(RestartGameAsync());
    }

    IEnumerator RestartGameAsync()
    {
        yield return Resources.UnloadUnusedAssets();
        SceneManager.LoadScene("S_Kadosa_LevelDesign");
    }

    public void ToggleSettingsMenu()
    {
        bool isOpened = mainMenuAnimator.GetBool("openSettings");
        mainMenuAnimator.SetBool("openSettings", !isOpened);

        settingsMenu.SetActive(!isOpened);

        settingsMenuAnimator.SetBool("openSettings", !isOpened);
    }

    public void ShowCredits()
    {
        // TODO: implement
        Debug.Log("Show credits");
    }
    
    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Make sure the game is not paused before changing scenes
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
