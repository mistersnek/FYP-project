using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPill : MonoBehaviour
{

    public GameObject target;

    public AIHealth agentHealth;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.transform.position, Vector3.up, 20 * Time.deltaTime);
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "AI")
        {
            agentHealth.currentHealth = agentHealth.currentHealth + 20f;
            target = null;
            Destroy(gameObject);
            

        }
    }
}
