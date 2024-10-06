using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour
{
    [Header("Player health settings")]
    public float health;
    public float maxHealth;
    public float ContactDamageTaken;

    public EnemyDamage EnemyDamageScript;

    public Image healthBar;
    void Start()
    {
        maxHealth = health;
        healthBar = GameObject.FindGameObjectWithTag("HealthUI").GetComponentInChildren<Image>();
    }

    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
    }

    public void LoseHealth()
    {
        health -= ContactDamageTaken;
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
    }

    /*
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy1"))
        {
            Debug.Log("Collision");
            EnemyDamageScript = other.gameObject.GetComponent<EnemyDamage>();
            if (EnemyDamageScript.didDamageYet == false)
            {
                Debug.Log("Bool set");
                health -= ContactDamageTaken;
                healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
                other.gameObject.GetComponent<EnemyDamage>().DidDamage();
            }
        }
    }
    */
}
