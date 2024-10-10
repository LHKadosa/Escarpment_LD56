using System;
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

    public static event Action<bool, GameScore> OnGameEnd;

    public Image healthBar;

    [SerializeField] AudioClip die;
    void Start()
    {
        maxHealth = health;
    }

    void FixedUpdate()
    {
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
        if (health <= 0)
        {

            GameScore score = new GameScore
            {
                score = null, // TODO: implement score system ?
                timeTaken = Time.time,
            };
            
            OnGameEnd?.Invoke(false, score);

            AudioManager.instance.PlaySFX(die, transform, 1f);
            Time.timeScale = 0f;
            Destroy(this.gameObject);
        }
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
