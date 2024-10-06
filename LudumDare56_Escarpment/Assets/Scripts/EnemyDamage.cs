using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public bool didDamageYet;
    private PlayerHealthScript playerHealth;

    void Start()
    {
        didDamageYet = false;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthScript>();
    }

    public void DidDamage() 
    {
        didDamageYet = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && didDamageYet == false)
        {
            playerHealth.LoseHealth();
            DidDamage();
            Debug.Log("Collision");
        }
    }
}
