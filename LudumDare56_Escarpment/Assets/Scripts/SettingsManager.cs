using UnityEngine;
using System;

public class SettingsManager : MonoBehaviour
{
    private static SettingsManager _instance;

    public static SettingsManager Instance {
        get 
        {
            if (_instance == null) 
            {
                _instance = FindObjectOfType<SettingsManager>();

                // If there is no instance in the scene, create one
                // This is to prevent Null reference errors while developing/testing
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<SettingsManager>();
                    singletonObject.name = typeof(SettingsManager).ToString() + " (Singleton)";
                }
            }

            return _instance;
        }
    }

    public event Action<int> OnSoundsVolumeChanged;

    public event Action<int> OnMusicVolumeChanged;

    public event Action<bool> OnDifficultyChanged;

<<<<<<< HEAD
    private int soundsVolume = -10;
    private int musicVolume = 10;
=======
    private int soundsVolume = 50;
    private int musicVolume = 50;
>>>>>>> parent of af437e3 (Audio can be adjusted from settings)
    private bool hardMode = false;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SetSoundsVolume(int newVolume)
    {
        soundsVolume = newVolume;
        OnSoundsVolumeChanged?.Invoke(soundsVolume);
    }
    
    public void SetMusicVolume(int newVolume)
    {
        musicVolume = newVolume;
        OnMusicVolumeChanged?.Invoke(musicVolume);
    }

    public void SetDifficulty(bool isHardMode)
    {
        hardMode = isHardMode;
        OnDifficultyChanged?.Invoke(hardMode);
    }

    public int GetSoundsVolume()
    {
        return soundsVolume;
    }
    
    public int GetMusicVolume()
    {
        return musicVolume;
    }

    public bool IsHardMode()
    {
        return hardMode;
    }
}
