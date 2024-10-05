using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour
{
#if UNITY_EDITOR
    public SceneAsset gameScene;
#endif
    
    public void StartGame()
    {
#if UNITY_EDITOR
        if (gameScene != null) {
            SceneManager.LoadScene(gameScene.name);
        } else {
            Debug.LogError("No scene is assigned!");
        }
#else
        //SceneManager.LoadScene("Game");
#endif
    }

    public void OpenSettings()
    {
        Debug.Log("Settings Button Clicked");
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
