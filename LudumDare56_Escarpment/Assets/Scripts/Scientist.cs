using System;
using UnityEngine;

public class Scientist : MonoBehaviour
{
    [SerializeField] AudioClip victory;

    public static event Action<bool, GameScore> OnGameVictory;

    void FixedUpdate()
    {
        float rotationSpeed = 60f;
        transform.Rotate(0, 0, rotationSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.PlaySFX(victory, transform, .4f);

            GameScore score = new GameScore
            {
                score = null, 
                timeTaken = Time.time,
            };

            OnGameVictory?.Invoke(false, score);
        }
    }
}
