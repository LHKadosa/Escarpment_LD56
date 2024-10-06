using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Hey you! Why don't you come here and exterminate these tiny creatures, would ya? Trial by fire, hahahaaaaa. 
    // Oh and there's a scientist trapped somewhere around here, what an idiot... 
    // Do me a favor and save his arse. Good luck, you little machinist!

    public static AudioManager instance;

    [SerializeField] AudioSource soundFXSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySFX(AudioClip audioClip, Transform targetTransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXSource, targetTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }

}
