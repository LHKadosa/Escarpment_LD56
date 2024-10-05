using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int health;
    [SerializeField] float speed;

    [Header("Refernces")]
    public Transform character;
    Rigidbody2D rb;

    [Header("Aggro")]
    [SerializeField] float aggroTriggerRadius;
    public float swarmRadius;
    [SerializeField] GameObject swarm;
    bool isAggroed = false;
    bool isSwarmed = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float distance = Vector2.Distance(transform.position, character.position);

        if (distance <= aggroTriggerRadius + Random.Range(-1f, 1f))
        {
            MoveTowardsTarget();
            swarm.SetActive(true);
        } 
        else if (isAggroed || isSwarmed)
        {
            MoveTowardsTarget();
        }
        else
        {
            rb.velocity = Vector2.zero;
            swarm.SetActive(false);
        }
    }

    void MoveTowardsTarget()
    {
        Vector2 direction = character.position - transform.position;
        direction.Normalize();
        Vector2 velocity = direction * speed;
        rb.velocity = velocity;
        isAggroed = true;
    }

    // visualization of aggro and swarm raduis 
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroTriggerRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, swarmRadius);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Swarm"))
        {
            isSwarmed = true;
        }
    }
}
