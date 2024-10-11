using UnityEngine;
using System;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    private static SettingsManager _instance;
    [SerializeField] AudioMixer audioMixer;

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

    private int soundsVolume = 0;
    private int musicVolume = 0;
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

        if (audioMixer == null)
        {
            audioMixer = Resources.Load<AudioMixer>("Assets/Audio/MainMixer.mixer");
        }
    }

    public void SetSoundsVolume(int newVolume)
    {
        soundsVolume = newVolume;
        OnSoundsVolumeChanged?.Invoke(soundsVolume);
        audioMixer.SetFloat("SFXVolume", soundsVolume);
    }
    
    public void SetMusicVolume(int newVolume)
    {
        musicVolume = newVolume;
        OnMusicVolumeChanged?.Invoke(musicVolume);
        audioMixer.SetFloat("MusicVolume", musicVolume);
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
