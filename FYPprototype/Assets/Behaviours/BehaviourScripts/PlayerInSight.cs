using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Conditions
{
    [Condition("Perception/PlayerInSight")]
    [Help("Checks whether a target is close and in sight depending on a given distance and an angle")]
    public class PlayerInSight : GOCondition
    {

        public FieldOfView aiFOV;

        public override bool Check()
        {
            aiFOV = gameObject.GetComponent<FieldOfView>();
             
            if (aiFOV.canSeePlayer == true)
            {
                return true;
            }
            else
                return false;
        }
    }
}