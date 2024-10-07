using System.Collections;
using UnityEngine;

public class FlamethrowerController : MonoBehaviour
{
    [SerializeField]
    GameObject flame;

    [SerializeField]
    ParticleSystem flameParticle;

    [SerializeField]
    SpriteRenderer tower;

    [SerializeField]
    float heat = 10f;
    float cooldown = 3f;

    bool isThrowingFlame, isOverheated;
    float maxHeat;

    bool isFlaming = false;
    int flameIterator = 0;

    [SerializeField] AudioClip flame1;
    [SerializeField] AudioClip flame2;
    [SerializeField] AudioClip flame3;

    void Start()
    {
        maxHeat = heat;
        isThrowingFlame = false;
        isOverheated = false;
        flame.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)&& !isOverheated)
        {
            isThrowingFlame = true;
            flame.SetActive(true);
            flameParticle.Play();
            AudioManager.instance.PlaySFX(flame1, transform, .6f);
        }

        if (Input.GetMouseButton(0) && !isOverheated && !isFlaming)
        {
            PlayFireAudio();
            flameIterator++;
        }

        if (Input.GetMouseButtonUp(0) || heat<=0)
        {
            isThrowingFlame = false;
            flame.SetActive(false);
            flameParticle.Stop();
            flameIterator = 0;
        }

        if (heat <= 0)
        {
            StartCoroutine(StartCooling());
        }

        HandleHeat();
       
    }

    void HandleHeat()
    {
        if (isThrowingFlame && heat>0)
        {
            heat -= 2*Time.deltaTime;
        }
        else if (!isThrowingFlame && heat < 10)
        {
            heat += 2*Time.deltaTime;
        }
        tower.color = new Color(1, heat / maxHeat, heat / maxHeat, 1);
    }

    IEnumerator StartCooling()
    {
        Debug.Log("Cooling started");
        isOverheated = true;
        yield return new WaitForSeconds(cooldown);
        isOverheated = false;
        Debug.Log("Cooling finished");
    }

    IEnumerator WaitForAudio()
    {
        AudioManager.instance.PlaySFX(flame2, transform, .5f);
        yield return new WaitForSeconds(.734f);
        isFlaming = false;
    }

    void PlayFireAudio()
    {
        AudioManager.instance.PlaySFX(flame2, transform, .6f);
        isFlaming=true;
        StartCoroutine(WaitForAudio());
        if (flameIterator == 3)
        {
            AudioManager.instance.PlaySFX(flame3, transform, 1f);
        }
    }
}
