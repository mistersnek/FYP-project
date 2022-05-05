using UnityEngine;
using UnityEngine.AI;

public class AIHealth : MonoBehaviour
{
    [HideInInspector]
    public float currentHealth;
    public float maxHealth;
    public NavMeshAgent agent;
    private Animator anim;

    [HideInInspector]
    public bool gotAttacked;

    public GameObject agentRig;
    Collider[] ragdollColliders;
    Rigidbody[] rigidBodies;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        GetComponents();
        RagdollOff();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log("Current health:" + currentHealth);
        gotAttacked = true;

        if (currentHealth <= 0.0f) Ragdoll();

    }
    public void IncreaseHealth(float amount)
    {
        currentHealth += amount;
    }

    private void Ragdoll()
    {
        GetComponent<Animator>().enabled = false;
        MonoBehaviour[] scripts = gameObject.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts) 
            script.enabled = false;

        foreach (Collider coll in ragdollColliders) 
            coll.enabled = true;

        Collider col = gameObject.GetComponent<CapsuleCollider>();
        col.enabled = false;

        foreach (Rigidbody rigd in rigidBodies)
            rigd.isKinematic = false;

        GetComponent<NavMeshAgent>().enabled = false;
    }

    public void RagdollOff()
    {
        foreach(Collider coll in ragdollColliders)
            coll.enabled = true;

        foreach(Rigidbody rigd in rigidBodies)
            rigd.isKinematic = true;
    }

    public void GetComponents()
    {
        agent = GetComponent<NavMeshAgent>();
        ragdollColliders = agentRig.GetComponentsInChildren<Collider>();
        rigidBodies = agentRig.GetComponentsInChildren<Rigidbody>();
        anim = agent.GetComponent<Animator>();
    }
}
