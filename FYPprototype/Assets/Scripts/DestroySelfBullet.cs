using UnityEngine;

public class DestroySelfBullet : MonoBehaviour
{
    public GameObject particle;

    public TrailRenderer Trail;

     private void Awake() {
        Trail = GetComponentInParent<TrailRenderer>();
    }

    //On collision with objects with tags, play the particle system and destroy itself
    void OnCollisionEnter(Collision col)
    {
        if (!col.transform.CompareTag("AI") || col.transform.CompareTag("Environment"))
        {
            
            /*Trail.autodestruct = true;
            Trail = null;*/

            Instantiate(particle, this.transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
