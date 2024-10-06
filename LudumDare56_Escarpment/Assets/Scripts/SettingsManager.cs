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

    public event Action<int> OnVolumeChanged;

    public event Action<bool> OnDifficultyChanged;

    private int volume = 50;
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
