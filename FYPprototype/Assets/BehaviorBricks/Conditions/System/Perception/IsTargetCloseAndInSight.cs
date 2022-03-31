using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Conditions
{
    /// <summary>
    /// It is a perception condition to check if the objective is close depending on a given distance and angle.
    [Condition("Perception/IsTargetCloseAndInSight")]
    [Help("Checks whether a target is close and in sight depending on a given distance and an angle")]
    public class IsTargetCloseAndInSight : GOCondition
    {
        ///<value>Input Target Parameter to to check the distance and angle.</value>
        [InParam("target")]
        [Help("Target to check the distance and angle")]
        public GameObject target;

        ///<value>Input view angle parameter to consider that the target is in sight.value>
        [InParam("angle")]
        [Help("The view angle to consider that the target is in sight")]
        public float angle;

        [InParam("radius")]
        [Help("The view radius to consider that the target is in sight")]
        public float radius;

        /*///<value>Input maximun distance Parameter to consider that the target is close.</value>
        [InParam("closeDistance")]
        [Help("The maximun distance to consider that the target is close")]
        public float closeDistance;*/

        [InParam("Target Mask")]
        [Help("Mask to define what is the target")]
        public LayerMask targetMask;

        [InParam("Obstruction Mask")]
        [Help("Mask to lose site when player is behind object with selected mask")]
        public LayerMask obstructionMask;

        /// <summary>
        /// Checks whether a target is close and in sight depending on a given distance,an angle and radius, 
        /// First calculates the magnitude between the gameobject and the target and then compares with the given distance, then
        /// casting a raycast to the target and then compare the angle of forward vector with de raycast direction.
        /// </summary>
        /// <returns>True if the magnitude between the gameobject and de target is lower that the given distance
        /// and if the angle of forward vector with the  raycast direction is lower than the given angle, false therwase.</returns>
		public override bool Check()
        {
            Vector3 directionToTarget = (target.transform.position - gameObject.transform.position);//.normalized;
            float distanceToTarget = Vector3.Distance(gameObject.transform.position, target.transform.position);
            Debug.Log(Vector3.Angle(gameObject.transform.forward, directionToTarget));
            if (Vector3.Angle(gameObject.transform.forward, directionToTarget) < angle / 2)
            //if (directionToTarget.sqrMagnitude < angle / 2)
            { 
                //casts a raycast starting at the AI agent, directed towards the player, with a distance that ends when it
                //reaches the player, taking into consideration any obstacles
                if (!Physics.Raycast(gameObject.transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    return true;
                else
                    return false;
            }
            else
                return false;
            }
           /* else
                return false;*/


            /*Vector3 dir = (target.transform.position - gameObject.transform.position);
            if (dir.sqrMagnitude > closeDistance * closeDistance)
                return false;
            RaycastHit hit;
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0, 0.1f, 0), dir, out hit))
            {
                return hit.collider.gameObject == target && Vector3.Angle(dir, gameObject.transform.forward) < angle * 0.5f;
            }
            return false;*/


        }
    }