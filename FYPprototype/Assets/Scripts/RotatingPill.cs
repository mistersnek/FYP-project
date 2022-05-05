using UnityEngine;

public class RotatingPill : MonoBehaviour
{
    private float healthGain = 20f;

    AIHealth health;

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, 20 * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "AI")
        {           
            health = col.transform.root.GetComponent<AIHealth>();

            //example: current health is 50, if lower than the max health - 20, give it 20 health
            if(health.currentHealth < 
            health.maxHealth - healthGain)
                health.IncreaseHealth(healthGain);
            //else give it the remaining health    
            else
                health.IncreaseHealth(health.maxHealth - health.currentHealth);
            
            Debug.Log("Current health + " + healthGain + " :" + health.currentHealth);
            Destroy(gameObject);
        }
    }
}