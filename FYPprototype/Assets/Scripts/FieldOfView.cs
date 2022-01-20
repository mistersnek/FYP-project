//Code adapted from:
//Comp-3 Interactive - https://www.youtube.com/watch?v=j1-OyLo77ss

using System;
using System.Collections;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    //limits the angle to 360 degrees
    [Range(0,360)]
    public float angle;

    public GameObject player;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }


    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if(rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                
                //casts a raycast starting at the AI agent, directed towards the player, with a distance that ends when it reaches the player taking into consideration any obstacles
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
                    
                
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }
}
