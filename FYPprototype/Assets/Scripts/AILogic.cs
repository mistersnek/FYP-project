//Code adapted from :
//TheKiwiCoder - https://www.youtube.com/watch?v=znZXmmyBF-o
//Brackeys - https://www.youtube.com/watch?v=znZXmmyBF-o
//DaveGameDevelopment - https://www.youtube.com/watch?v=UjkSFoLxesw
//Jayanam - https://www.youtube.com/watch?v=Zjlg9F3FRJs

using UnityEngine;
using UnityEngine.AI;

public class AILogic : MonoBehaviour
{
    private NavMeshAgent agent;
    public float speed = 6f;
    public Transform player;
    public LayerMask whatIsPlayer, whatIsGround;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public GameObject bulletPoint;

    //States
    public float sightRange, attackRange;
    public bool playerInRange, playerAttackRange;

    [Header("Projectile Speed and Upwards force")]
    public float bulletSpeed = 90f;
    public float upwardForce = 2f;

    public GameObject aiAgent;
    private FieldOfView aiFOV;
    private AIHealth agentHealth;

    private GameObject[] healthPoints;

    public float EnemyDistanceRun = 6f;


    private Animator anim;


    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkRange, walkRange);
        float randomX = Random.Range(-walkRange, walkRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            anim.SetBool("walking", true);

        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
            anim.SetBool("walking", false);
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        anim.SetBool("walking", true);
    }

    private void AttackPlayer()
    {
        //make sure enemy doesnt move
        agent.SetDestination(transform.position);
        anim.SetBool("walking", false);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, bulletPoint.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            
            rb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
            rb.AddForce(transform.up * upwardForce, ForceMode.Impulse);

            alreadyAttacked = true;

            anim.SetTrigger("attacking");

            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void RunFromPlayer()
    {
        anim.SetBool("walking", true);

        if (GameObject.FindWithTag("HealthPoint") != null)
        {
            agent.SetDestination(GameObject.FindWithTag("HealthPoint").transform.position);
            Debug.Log("running for health");
        } else if (GameObject.FindWithTag("HealthPoint") == null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if(distance < EnemyDistanceRun)
            {
                

                Vector3 dirToPlayer = transform.position - player.transform.position;
                Vector3 newPos = transform.position + dirToPlayer;

                agent.SetDestination(newPos);
                Debug.Log("running from player");
            }
        }
    }   

    // Start is called before the first frame update
    void Start()
    {
        aiFOV = GetComponent<FieldOfView>();
        agentHealth = GetComponent<AIHealth>();
        anim = GetComponent<Animator>();
        agent.speed = speed;

        healthPoints = GameObject.FindGameObjectsWithTag("HealthPoint");

    }

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {            
        //creates a sphere where if the player is inside it, do something
        playerInRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (agentHealth.currentHealth <= agentHealth.maxHealth / 2) RunFromPlayer();

        else if (aiFOV.canSeePlayer == false) Patrolling();

        else if (aiFOV.canSeePlayer == true && playerInRange) ChasePlayer();

        if (aiFOV.canSeePlayer == true && playerAttackRange && agentHealth.currentHealth > agentHealth.maxHealth / 2)  AttackPlayer();

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
