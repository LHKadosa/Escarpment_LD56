using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAudio : MonoBehaviour
{
    //Audio
    [SerializeField] AudioClip track1;
    [SerializeField] AudioClip track2;
    [SerializeField] AudioClip track3;

    bool track1IsPlaying = false;
    int trackIterator = 0;
    int rnd;

    private void Start()
    {
        rnd = Random.Range(3, 6);
    }

    private void FixedUpdate()
    {
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && !track1IsPlaying)
        {
            TrackAudio();
        }
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            trackIterator = 0;
        }
        
    }

    IEnumerator WaitForAudio()
    {
        yield return new WaitForSeconds(.537f);
        if (trackIterator == rnd)
        {
            int rnd2 = Random.Range(0, 2);
            if (rnd2 == 0)
            {
                AudioManager.instance.PlaySFX(track2, transform, 1f);
            }
            else
            {
                AudioManager.instance.PlaySFX(track3, transform, 1f);
            }
            rnd = Random.Range(3, 6);
            trackIterator = 0;
        }
        yield return new WaitForSeconds(.4f);
        track1IsPlaying = false;
    }

    void TrackAudio()
    {
        AudioManager.instance.PlaySFX(track1, transform, 1f);
        track1IsPlaying = true;
        trackIterator++;
        StartCoroutine(WaitForAudio());
    }
}
