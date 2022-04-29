using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Conditions
{
    [Condition("Perception/IsPlayerInSightAndRange")]
    [Help("Checks whether a target is close and in sight depending on a given distance and an angle")]
    public class IsPlayerInSightAndRange : GOCondition
    {
        [InParam("closeDistance")]
        [Help("The maximun distance to consider that the target is close")]
        public float closeDistance;

        [InParam("target")]
        [Help("Target to check the distance")]
        public GameObject target;

        public FieldOfView aiFOV;

        public override bool Check()
        {
            aiFOV = gameObject.GetComponent<FieldOfView>();            

            if (aiFOV.canSeePlayer == true && (gameObject.transform.position - target.transform.position).sqrMagnitude < closeDistance * closeDistance)
            {
                return true;
            }
            else
                return false;
        }
    }    
}