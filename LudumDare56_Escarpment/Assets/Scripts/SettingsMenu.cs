using UnityEngine;
using UnityEngine.UI;  // Required to work with UI elements

public class SettingsMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public Toggle difficultyToggle;

    void Start()
    {
        difficultyToggle.onValueChanged.AddListener(delegate { OnHardModeToggled(); });

        InitializeVolumeSlider();
    }

    void InitializeVolumeSlider()
    {
        int currentVolume = SettingsManager.Instance.GetVolume();
        volumeSlider.value = (float)currentVolume;

        bool isHardMode = SettingsManager.Instance.IsHardMode();
        difficultyToggle.isOn = isHardMode;
    }

    public void OnVolumeChanged()
    {
        SettingsManager.Instance.SetVolume((int)volumeSlider.value);
    }

    void OnHardModeToggled()
    {
        bool isHardMode = difficultyToggle.isOn;
        SettingsManager.Instance.SetDifficulty(isHardMode);
    }
}
