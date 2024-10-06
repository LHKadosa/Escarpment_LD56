using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;


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
        SceneManager.LoadScene("Game");
#endif
    }

    public void ResumeGame()
    {
        Debug.Log("RESUME");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PauseGame()
    {
        Debug.Log("PAUSE");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ToggleSettingsMenu()
    {
        bool isOpened = mainMenuAnimator.GetBool("openSettings");
        mainMenuAnimator.SetBool("openSettings", !isOpened);

        settingsMenu.SetActive(!isOpened);

        settingsMenuAnimator.SetBool("openSettings", !isOpened);
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
