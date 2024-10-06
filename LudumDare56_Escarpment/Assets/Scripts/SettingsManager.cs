using UnityEngine;
using System;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    public event Action<int> OnVolumeChanged;

    public event Action<bool> OnDifficultyChanged;

    private int volume = 50;
    private bool hardMode = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetVolume(int newVolume)
    {
        volume = newVolume;
        OnVolumeChanged?.Invoke(volume);
    }

    public void SetDifficulty(bool isHardMode)
    {
        hardMode = isHardMode;
        OnDifficultyChanged?.Invoke(hardMode);
    }

    public int GetVolume()
    {
        return volume;
    }

    public bool IsHardMode()
    {
        return hardMode;
    }
}
