using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;
using System.Collections.Generic;

namespace BBUnity.Actions
{
    /// <summary>
    /// It is an action to obtain a random position of an area.
    /// </summary>
    [Action("Behaviours/BehaviourScripts/ChangePatrolIndex")]
    [Help("Checks which patrol point it is on")]
    public class ChangePatrolIndex : GOAction
    {
        [InParam("patrolPoints[]")]
        [Help("Position of patrol points")]
        public List<Transform> patrolPoints = new List<Transform>();

        [InParam("CurrentPatrolPointIndex")]
        public int currentPatrolPointsIndex;

        [OutParam("PatrolPointsIndex")]
        [Help("Patrol points index")]
        public int patrolPointsIndex;

        public Vector3 distanceToPatrolPoint;

        public override void OnStart()
        {
            distanceToPatrolPoint = gameObject.transform.position - patrolPoints[patrolPointsIndex].position;

            patrolPointsIndex = currentPatrolPointsIndex + 1;

            if (patrolPointsIndex >= patrolPoints.Count)
            {
               patrolPointsIndex = 0;
            }
        }

        public override TaskStatus OnUpdate()
        {
            //Debug.Log("is changing to " + patrolPointsIndex);
            return TaskStatus.COMPLETED;
        }
    }
}