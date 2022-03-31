using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

namespace BBUnity.Actions
{
    /// <summary>
    /// It is an action to obtain a random position of an area.
    /// </summary>
    [Action("Behaviours/BehaviourScripts/LookAtPlayer")]
    [Help("Checks which patrol point it is on")]
    public class LookAtPlayer : GOAction
    {
        [InParam("target")]
        [Help("Target to check the distance")]
        public GameObject target;

        private NavMeshAgent navAgent;

        public override void OnStart()
        {
            navAgent = gameObject.GetComponent<NavMeshAgent>();
            navAgent.SetDestination(gameObject.transform.position);            
        }

        /// <summary>Abort method of GetRandomInArea.</summary>
        /// <remarks>Complete the task.</remarks>
        public override TaskStatus OnUpdate()
        {
            //Have AI look at the player
            Vector3 lookAtPosition = target.transform.position;
            lookAtPosition.y = gameObject.transform.position.y;
            gameObject.transform.LookAt(lookAtPosition);

            return TaskStatus.COMPLETED;
        }
    }
}