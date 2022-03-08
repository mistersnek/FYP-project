using UnityEngine;

public class RotatingPill : MonoBehaviour
{
    private float healthGain = 20f;
    AIHealth health;

    private void Awake()
    {
        health = FindObjectOfType<AIHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, 20 * Time.deltaTime);
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "AI" && health.currentHealth <= 100)
        {
            health.IncreaseHealth(healthGain);
            Destroy(gameObject);
        }
        else if (health.currentHealth > 100)
            Destroy(gameObject);
    }
}