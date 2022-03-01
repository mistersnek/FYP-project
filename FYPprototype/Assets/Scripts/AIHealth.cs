using UnityEngine;
using UnityEngine.AI;

public class AIHealth : MonoBehaviour
{
    [HideInInspector]
    public float currentHealth;

    public float maxHealth;
    public NavMeshAgent agent;

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
        Debug.Log("Current health:" +currentHealth);

        if (currentHealth <= 0.0f)   Ragdoll();
    }

    private void Ragdoll()
    {
        GetComponent<Animator>().enabled = false;
        MonoBehaviour[] scripts = gameObject.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts) 
            script.enabled = false;

        foreach (Collider coll in ragdollColliders) 
            coll.enabled = true;

        foreach (Rigidbody rigd in rigidBodies)
            rigd.isKinematic = false;

        GetComponent<NavMeshAgent>().enabled = false;
       // GetComponent<CapsuleCollider>().enabled = false;
    }

    private void RagdollOff()
    {
        foreach(Collider coll in ragdollColliders)
            coll.enabled = true;

        foreach(Rigidbody rigd in rigidBodies)
            rigd.isKinematic = true;
    }

    private void GetComponents()
    {
        agent = GetComponent<NavMeshAgent>();
        ragdollColliders = agentRig.GetComponentsInChildren<Collider>();
        rigidBodies = agentRig.GetComponentsInChildren<Rigidbody>();
    }
}
