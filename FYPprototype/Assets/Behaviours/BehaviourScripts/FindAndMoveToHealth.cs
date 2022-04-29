using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;
using UnityEngine.AI;

namespace BBUnity.Actions
{
    /// <summary>
    /// It is an action to move towards the given goal using a NavMeshAgent.
    /// </summary>
    [Action("Behaviours/BehaviourScripts/FindAndMoveToHealth")]
    [Help("Moves the game object towards a given target by using a NavMeshAgent")]
    public class FindAndMoveToHealth : GOAction
    {
        ///<value>Input target game object towards this game object will be moved Parameter.</value>
        [InParam("target")]
        [Help("Target game object towards this game object will be moved")]
        public GameObject target;

        private NavMeshAgent navAgent;
        private Animator anim;

        [InParam("agent speed")]
        [Help("Speed at which the agent moves")]
        public float speed;

        /// <summary>Initialization Method of MoveToGameObject.</summary>
        /// <remarks>Check if GameObject object exists and NavMeshAgent, if there is no NavMeshAgent, the default one is added.</remarks>
        public override void OnStart()
        {
            if (target == null)
            {
                Debug.LogError("The movement target of this game object is null", gameObject);
                return;
            }

            anim = gameObject.GetComponent<Animator>();
            navAgent = gameObject.GetComponent<NavMeshAgent>();
            navAgent.speed = speed;

            if (navAgent == null)
            {
                Debug.LogWarning("The " + gameObject.name + " game object does not have a Nav Mesh Agent component to navigate. One with default values has been added", gameObject);
                navAgent = gameObject.AddComponent<NavMeshAgent>();
            }

            navAgent.SetDestination(target.transform.position);

            if(anim != null)
                anim.SetBool("walking", true);
        }

        /// <summary>Method of Update of MoveToGameObject.</summary>
        /// <remarks>Verify the status of the task, if there is no objective fails, if it has traveled the road or is near the goal it is completed
        /// y, the task is running, if it is still moving to the target.</remarks>
        public override TaskStatus OnUpdate()
        {
            if (target == null)
                return TaskStatus.FAILED;
            if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
            {
                anim.SetBool("walking", false);
                return TaskStatus.COMPLETED;
            }
            else if (navAgent.destination != target.transform.position)
                navAgent.SetDestination(target.transform.position);
            return TaskStatus.RUNNING;
        }
    }
}
