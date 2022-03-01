using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public SwordController swordController;
    //public AIHealth damage;
    public GameObject hitParticle;
    public int swordDamage = 20;
    public bool hasAttacked;

    private void OnTriggerEnter(Collider other)
    {
        if(swordController.isAttacking)
            if (other.gameObject.tag == "AI" && !hasAttacked)
            {   
                Debug.Log("attacked");
                hasAttacked = true;
                Instantiate(hitParticle, new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), other.transform.rotation);

                AIHealth damage = other.gameObject.GetComponentInParent<AIHealth>();
                damage.TakeDamage(swordDamage);

            }
            }
    }