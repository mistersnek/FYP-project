using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Conditions
{
    [Condition("Behaviours/BehaviourScripts/IsHealthLow")]
    [Help("Checks whether a target is close and in sight depending on a given distance and an angle")]
    public class IsHealthLow : GOCondition
    {

        public AIHealth aiHealth;

        public override bool Check()
        {
            aiHealth = gameObject.GetComponent<AIHealth>();
            if(aiHealth.currentHealth <= aiHealth.maxHealth / 2 && GameObject.FindGameObjectWithTag("HealthPoint") != null) 
                return true;
                else
                return false;
        }
    }
}