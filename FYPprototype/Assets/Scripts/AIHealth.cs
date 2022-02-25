using UnityEngine;
using UnityEngine.AI;

public class AIHealth : MonoBehaviour
{
    [HideInInspector]
    public float currentHealth;

    public float maxHealth;
    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        agent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log("Current health:" +currentHealth);

        if(currentHealth <= 0.0f) Ragdoll();

    }

    private void Ragdoll()
    {
        MonoBehaviour[] scripts = gameObject.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = false;
        }
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Animator>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
    }
}
