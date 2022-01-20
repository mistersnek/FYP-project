using UnityEngine;

public class DestroySelfBullet : MonoBehaviour
{
    public GameObject particle;

    private void Start()
    {
        // particle.Stop();
    }

    //On collision with objects with tags, play the particle system and destroy itself
    void OnCollisionEnter(Collision col)
    {
        if (col.transform.CompareTag("Player") || col.transform.CompareTag("Environment"))
        {
            Instantiate(particle, this.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
