//Code adapted from:
//Comp-3 Interactive - https://www.youtube.com/watch?v=j1-OyLo77ss

using System;
using System.Collections;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    private float curRadius;
    //limits the angle to 360 degrees
    [Range(0,360)]
    public float angle;

    private float curAngle;

    public GameObject player;
    public ThirdPersonMovement crouch;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        crouch = player.GetComponent<ThirdPersonMovement>();
        StartCoroutine(FOVRoutine());
        curRadius = radius;
        curAngle = angle;
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

        if (crouch.isCrouched == true && canSeePlayer == false)
        {
            angle = curAngle / 2;
            radius = curRadius / 2;
        } 
            else if (crouch.isCrouched == false || canSeePlayer == true)
        {
            angle = curAngle;
            radius = curRadius;
        }

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
