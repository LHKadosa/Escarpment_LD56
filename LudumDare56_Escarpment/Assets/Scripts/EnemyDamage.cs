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
        Invoke("SetPlayerScript", 0.5f);
    }

    public void DidDamage() 
    {
        didDamageYet = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && didDamageYet == false && playerHealth != null)
        {
            playerHealth.LoseHealth();
            DidDamage();
            Debug.Log("Collision");
        }
    }

    void SetPlayerScript()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthScript>();
    }
}
