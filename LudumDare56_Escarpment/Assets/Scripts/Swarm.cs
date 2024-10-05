using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarm : MonoBehaviour
{
    EnemyController enemyController;

    private void Start()
    {
        enemyController = GetComponentInParent<EnemyController>();
        CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.radius = enemyController.swarmRadius;
    }
}
