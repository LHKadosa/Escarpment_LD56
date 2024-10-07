using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scientist : MonoBehaviour
{
    [SerializeField] AudioClip victory;

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
        }
    }
}
