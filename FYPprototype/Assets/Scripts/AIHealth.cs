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

        if(currentHealth <= 0.0f) Die();

    }

    private void Die()
    {
        Destroy(gameObject);
    }

   

}
