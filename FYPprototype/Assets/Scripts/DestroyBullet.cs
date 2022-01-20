using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    public GameObject particle;

    private void Start()
    {
        // particle.Stop();
    }
    //because OnCollisionEnter doesnt work properly with character controllers, use this on the player object to make it work everytime
    void OnControllerColliderHit(ControllerColliderHit col)
    {
        if (col.transform.CompareTag("Bullet"))
        {
            Instantiate(particle, this.transform.position, Quaternion.identity);
            Destroy(col.gameObject);
        }
    }
}
