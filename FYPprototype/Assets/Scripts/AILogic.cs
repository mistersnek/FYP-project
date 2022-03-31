//Code adapted from :
//TheKiwiCoder - https://www.youtube.com/watch?v=znZXmmyBF-o
//Brackeys - https://www.youtube.com/watch?v=znZXmmyBF-o
//DaveGameDevelopment - https://www.youtube.com/watch?v=UjkSFoLxesw
//Jayanam - https://www.youtube.com/watch?v=Zjlg9F3FRJs
//KeySmash Studios - https://www.youtube.com/watch?v=22PZJlpDkPE

using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AILogic : MonoBehaviour
{
    //reference to the nav mesh agent, animator
    private NavMeshAgent agent;
    private Animator anim;

    //speed at which agent can walk
    public float speed = 6f;

    //reference to the player and a layer mask to identify what is the player
    public Transform player;
    public LayerMask whatIsPlayer;


    //Attacking cooldown
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //the projectile to be used and the point it is instantiated from
    public GameObject projectile;
    public GameObject bulletPoint;

    //States
    public float sightRange, attackRange;
    public bool playerInRange, playerAttackRange;

    //force and speed of the projectile instantiated at the bullet point position
    [Header("Projectile Speed and Upwards force")]
    public float bulletSpeed = 90f;
    public float upwardForce = 2f;

    //references to the ai agent itself, the script FOV and aiHealth
    public GameObject aiAgent;
    private FieldOfView aiFOV;
    private AIHealth agentHealth;

    //array of healthpoints that saves their positions 3D
    private GameObject[] healthPoints;

    //distance the enemy runs from player when too low on health
    public float EnemyDistanceRun = 12f;

    [Header ("Set Patrol Points Here")]
    public Transform[] patrolPoints;

    //Which patrol point the AI is currently at
    public int patrolPointsIndex = 0;

    //IEnumerator allows for cooldowns but is inneficient (apparently)
    IEnumerator ActionCooldown(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
    }
    private void Patrolling()
    {
        agent.SetDestination(patrolPoints[patrolPointsIndex].position);
        anim.SetBool("walking", true);

        Vector3 distanceToPatrolPoint = transform.position - patrolPoints[patrolPointsIndex].position;
        
        //Patrol Point reached
        if (distanceToPatrolPoint.magnitude < 1.5f)
        {
            anim.SetBool("walking", false);
            IncreaseIndex();
        }

        if(agentHealth.gotAttacked == true)
        {
            transform.LookAt(new Vector3(player.position.x, 0, player.position.z));
        }
    }
    private void IncreaseIndex()
    {
        patrolPointsIndex++;
        if(patrolPointsIndex >= patrolPoints.Length)
        {
            patrolPointsIndex = 0;
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

        //make AI only look at player in X and Z axis
        Vector3 lookAtPosition = player.position;
        lookAtPosition.y = transform.position.y;
        transform.LookAt(lookAtPosition);

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
            //calculates distance to player from the ai agent
            float distance = Vector3.Distance(transform.position, player.transform.position);

            Vector3 dirToPlayer = transform.position - player.transform.position;
            Vector3 newPos = transform.position + dirToPlayer;

            if (distance < EnemyDistanceRun)
            {
                agent.SetDestination(newPos);
                Debug.Log("running from player");
                Debug.Log(newPos);

            }

            if(aiAgent.transform.position == newPos)
            {
                anim.SetBool("walking", false);
            }
        }
        StartCoroutine(ActionCooldown(5f));
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

        if (aiFOV.canSeePlayer == true && playerAttackRange && agentHealth.currentHealth > agentHealth.maxHealth / 2) AttackPlayer();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}