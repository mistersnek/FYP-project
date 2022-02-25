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
    private NavMeshAgent agent;
    private Animator anim;
    public float speed = 6f;
    public Transform player;
    public LayerMask whatIsPlayer, whatIsGround;

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

    public float EnemyDistanceRun = 12f;

    [Header ("Set Patrol Points Here")]
    public Transform[] patrolPoints;
    //Which patrol point the AI is currently at
    private int patrolPointsIndex;


    private void Patrolling()
    {
        agent.SetDestination(patrolPoints[patrolPointsIndex].position);
        anim.SetBool("walking", true);

        Vector3 distanceToPatrolPoint = transform.position - patrolPoints[patrolPointsIndex].position;
        
        /*Debug.Log(patrolPointsIndex);
        Debug.Log(distanceToPatrolPoint.magnitude);*/

        //Patrol Point reached
        if (distanceToPatrolPoint.magnitude < 1.5f)
        {
            anim.SetBool("walking", false);
            IncreaseIndex();
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

        //make AI only look at player in Y axis
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
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if(distance < EnemyDistanceRun)
            {
                

                Vector3 dirToPlayer = transform.position - player.transform.position;
                Vector3 newPos = transform.position + dirToPlayer;

                agent.SetDestination(newPos);
                Debug.Log("running from player");
            }
        }
        StartCoroutine(RunFromPlayerCooldown());
    }

    IEnumerator RunFromPlayerCooldown()
    {
        yield return new WaitForSeconds(5);
        //if(aiAgent != null) ChasePlayer();
    }

    // Start is called before the first frame update
    void Start()
    {
        aiFOV = GetComponent<FieldOfView>();
        agentHealth = GetComponent<AIHealth>();
        anim = GetComponent<Animator>();
        agent.speed = speed;
        healthPoints = GameObject.FindGameObjectsWithTag("HealthPoint");


        patrolPointsIndex = 0;
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
