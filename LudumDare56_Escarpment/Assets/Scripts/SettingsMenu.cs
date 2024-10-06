using UnityEngine;
using UnityEngine.UI;  // Required to work with UI elements

public class SettingsMenu : MonoBehaviour
{
    public Slider soundsSlider;
    public Slider musicSlider;
    public Toggle difficultyToggle;

    void Start()
    {
        difficultyToggle.onValueChanged.AddListener(delegate { OnHardModeToggled(); });

        InitializeSettings();
    }

    void InitializeSettings()
    {
        int currentSoundsVolume = SettingsManager.Instance.GetSoundsVolume();
        soundsSlider.value = (float)currentSoundsVolume;
        
        int currentMusicVolume = SettingsManager.Instance.GetMusicVolume();
        musicSlider.value = (float)currentMusicVolume;

        bool isHardMode = SettingsManager.Instance.IsHardMode();
        difficultyToggle.isOn = isHardMode;
    }

    public void OnSoundsVolumeChanged()
    {
        Debug.Log("OnSoundsVolumeChanged " + soundsSlider.value);
        SettingsManager.Instance.SetSoundsVolume((int)soundsSlider.value);
    }
    
    public void OnMusicVolumeChanged()
    {
        Debug.Log("OnMusicVolumeChanged " + musicSlider.value);
        SettingsManager.Instance.SetMusicVolume((int)musicSlider.value);
    }

    void OnHardModeToggled()
    {
        bool isHardMode = difficultyToggle.isOn;
        SettingsManager.Instance.SetDifficulty(isHardMode);
    }
}
