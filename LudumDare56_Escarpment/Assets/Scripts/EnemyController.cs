using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int health;
    [SerializeField] float speed;
    [SerializeField] float aggroTriggerRadius;
    [SerializeField] float swarmRadius;

    [Header("Refernces")]
    public Transform character;
    Rigidbody2D rb;

    [Header("Swarming")]
    Transform nearestObject = null;
    float nearestDistance = Mathf.Infinity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float distance = Vector2.Distance(transform.position, character.position);

        if (distance <= aggroTriggerRadius)
        {
            MoveTowardsTarget();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void MoveTowardsTarget()
    {
        Vector2 direction = character.position - transform.position;
        direction.Normalize();
        Vector2 velocity = direction * speed;
        rb.velocity = velocity;
    }

    // visualization of aggro and swarm raduis 
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroTriggerRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, swarmRadius);
    }

}
