using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;
using UnityEngine.AI;

namespace BBUnity.Actions
{
    /// <summary>
    /// It is an action to move the GameObject to a given position.
    /// </summary>
    [Action("Behaviour/BehaviourScripts/MoveToPatrolPoint")]
    [Help("Moves the game object to a given patrol point by using a NavMeshAgent")]
    public class MoveToPatrolPoint : GOAction
    {
        ///<value>Input target position where the game object will be moved Parameter.</value>
        [InParam("target")]
        [Help("Target position where the game object will be moved")]
        public Vector3 target;

        private NavMeshAgent navAgent;
        private Animator anim;

        /// <summary>Initialization Method of MoveToPosition.</summary>
        /// <remarks>Check if there is a NavMeshAgent to assign a default one and assign the destination to the NavMeshAgent the given position.</remarks>
        public override void OnStart()
        {
            anim = gameObject.GetComponent<Animator>();
            navAgent = gameObject.GetComponent<NavMeshAgent>();
            navAgent.speed = 3f;
            if (navAgent == null)
            {            
                navAgent = gameObject.AddComponent<NavMeshAgent>();               
            }
            navAgent.SetDestination(target);
            anim.SetBool("walking", true);
        }

        public override TaskStatus OnUpdate()
        {
            if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
                return TaskStatus.COMPLETED;

            return TaskStatus.RUNNING;
        }

    }
}