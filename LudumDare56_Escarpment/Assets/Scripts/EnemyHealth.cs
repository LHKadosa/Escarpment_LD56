using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;
    int hitCount = 0;
    private void OnParticleCollision(GameObject other)
    {
        hitCount++;
        Debug.Log("Hits taken" + hitCount);
        if (hitCount >= maxHealth)
        {
            Destroy(this.gameObject);
        }
    }
}
