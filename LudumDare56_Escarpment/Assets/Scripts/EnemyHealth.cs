using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;
    public GameObject explodingParticle;

    int hitCount = 0;
    private void OnParticleCollision(GameObject other)
    {
        hitCount++;
        Debug.Log("Hits taken" + hitCount);
        if (hitCount >= maxHealth)
        {
            Instantiate(explodingParticle, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            GameScore.destroyedEnemies++;
            Destroy(this.gameObject);
        }
    }
}
