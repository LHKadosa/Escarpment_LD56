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
    [SerializeField] AudioClip enemy1Swarm;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
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

    private void AttackPlayer()
    {
        isExploding = true;
        Destroy(gameObject, .2f);
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

    void PlaySwarmSFX()
    {
        AudioManager.instance.PlaySFX(enemy1Swarm, transform, 1f);
    }

    // Visualization of aggro and swarm radius 
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, aggroTriggerRadius);

        Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position, swarmRadius);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Swarm"))
        {
            isSwarmed = true;
            int reduceSwarm = Random.Range(1, 5);
            if (reduceSwarm == 1)
            {
                float crowdVoice = Random.Range(0f, .8f);
                Invoke("PlaySwarmSFX", crowdVoice);
            }
        }
    }

    private void OnDestroy()
    {
        AudioManager.instance.PlaySFX(enemy1Explosion, transform, 1f);
    }
}
