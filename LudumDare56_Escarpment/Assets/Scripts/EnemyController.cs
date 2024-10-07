using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int health;
    [SerializeField] float speed;

    [Header("Refernces")]
    public Transform character;
    Rigidbody2D rb;
    private Animator anim;

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

    private Vector3 targetOffset;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        anim = gameObject.transform.GetChild(0).GetComponent<Animator>();
        anim.SetBool("IsMoving", false);
        NewTargetOffset();

        if (SettingsManager.Instance.IsHardMode())
        {
            speed = 3f;
        }
        else
        {
            speed = 1f;
        }
        
    }

    void FixedUpdate()
    {
        if (character != null)
        {
            //Döme, I'm terribly sorry.I had to make some changes to your code in order to avoid some potencial future errors.I take full responsibility fot the errors my actions might cause here.
            float distance = Vector2.Distance(transform.position, character.position);
            anim.SetBool("IsMoving", false);

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
                FacePlayer();
            }
            else if (distance < aggroTriggerRadius + Random.Range(-1f, 1f))
            {
                MoveTowardsTarget();
                swarm.SetActive(true);
                FacePlayer();
            }

            if ((character.position + targetOffset - transform.position).magnitude < 1)
            {
                NewTargetOffset();
            }
        }
    }

    private void NewTargetOffset()
    {
        targetOffset = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), 0);
    }

    private void AttackPlayer()
    {
        isExploding = true;
        Destroy(gameObject, .2f);
        // Play particle here ==========================================
    }

    void MoveTowardsTarget()
    {
        Vector2 direction = (character.position + targetOffset) - transform.position;
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

    void FacePlayer()
    {
        var offset = 90f;
        Vector2 direction = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));

        anim.SetBool("IsMoving", true);
    }

    private void OnDestroy()
    {
        AudioManager.instance.PlaySFX(enemy1Explosion, transform, 1f);
    }
}
