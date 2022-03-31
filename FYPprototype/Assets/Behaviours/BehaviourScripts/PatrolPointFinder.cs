using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;
using System.Collections.Generic;

namespace BBUnity.Actions
{
    /// <summary>
    /// It is an action to obtain a random position of an area.
    /// </summary>
    [Action("Behaviours/BehaviourScripts/PatrolPointFinder")]
    [Help("Checks which patrol point it is on")]
    public class PatrolPointFinder : GOAction
    {
        /// <summary>The Name property represents the GameObject Input Parameter that must have a BoxCollider or SphereColider.</summary>
        /// <value>The Name property gets/sets the value of the GameObject field, area.</value>

        [OutParam("patrol Points")]
        [Help("Position of patrol points")]
        public List<Transform> patrolPoints = new List<Transform>();

        [InParam("patrolPointsIndex")]
        [Help("Position of patrol points")]
        public int patrolPointsIndex;

        int counter;

        [OutParam("wanderTarget")]
        [Help("Position of patrol points")]
        public Vector3 WanderTarget { get; set; }

        public Vector3 distanceToPatrolPoint;

        /// <summary>Initialization Method of GetRandomInArea</summary>
        /// <remarks>Verify if there is an area, showing an error if it does not exist.Check that the area is a box or sphere to differentiate the
        /// calculations when obtaining the random position of those areas. Once differentiated, you get the limits of the area to calculate a
        /// random position.</remarks>
        public override void OnStart()
        {

                foreach (GameObject go in GameObject.FindGameObjectsWithTag("PatrolPoint"))
                    patrolPoints.Add(go.GetComponent<Transform>());


            WanderTarget = patrolPoints[patrolPointsIndex].position;
        }

        /// <summary>Abort method of GetRandomInArea.</summary>
        /// <remarks>Complete the task.</remarks>
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}