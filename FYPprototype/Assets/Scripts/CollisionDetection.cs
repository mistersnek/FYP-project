using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public SwordController sc;
    public AIHealth damage;
    public GameObject hitParticle;
    public int swordDamage = 20;
    public bool hasAttacked;

    private void OnTriggerEnter(Collider other)
    {
        if(sc.isAttacking)
        {
            if (other.gameObject.CompareTag("AI") && !hasAttacked)
            {
                Debug.Log("attacked");
                hasAttacked = true;
                Instantiate(hitParticle, new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), other.transform.rotation);
                damage.TakeDamage(swordDamage);
            }
        }
    }
}
