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
    [SerializeField] float attackRange;
    public float swarmRadius;
    [SerializeField] GameObject swarm;
    bool isAggroed = false;
    bool isSwarmed = false;
    bool isExploding = false;

    [Header("Audio")]
    [SerializeField] AudioClip enemy1Explosion;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        if (character != null)
        {
            //D�me, I'm terribly sorry. I had to make some changes to your code in order to avoid some potencial future errors. I take full responsibility fot the errors my actions might cause here.
            float distance = Vector2.Distance(transform.position, character.position);

            if (isExploding)
            {
                return;
            }
            else if (distance < attackRange)
            {
                AttackPlayer();
            }
            else if (isAggroed || isSwarmed)
            {
                MoveTowardsTarget();
            }
            else if (distance < aggroTriggerRadius + Random.Range(-1f, 1f))
            {
                MoveTowardsTarget();
                swarm.SetActive(true);
            }
        }
    }

    private void AttackPlayer()
    {
        isExploding = true;
        Destroy(gameObject, .5f);
        // Play particle here ==========================================
    }

    void MoveTowardsTarget()
    {
        Vector2 direction = character.position - transform.position;
        direction.Normalize();
        Vector2 velocity = direction * speed;
        rb.velocity = velocity;
        isAggroed = true;
    }

    // Visualization of aggro and swarm radius 
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

    private void OnDestroy()
    {
        AudioManager.instance.PlaySFX(enemy1Explosion, transform, 1f);
    }
}
