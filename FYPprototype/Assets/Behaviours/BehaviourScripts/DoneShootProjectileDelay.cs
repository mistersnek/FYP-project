using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using UnityEngine;

namespace BBSamples.PQSG // Programmers Quick Start Guide
{
    [Action("Behaviours/BehaviourScripts/DoneShootProjectileDelay")]
    [Help("Periodically clones a 'bullet' and shoots it throught the Forward axis " +
          "with the specified velocity. This action never ends.")]
    public class DoneShootProjectileDelay : DoneShootProjectile
    {
        // Define the input parameter delay, with the waited game loops between shoots.
        [InParam("delay", DefaultValue = 30)]
        public int delay;

        // Game loops since the last shoot.
        private int elapsed = 0;

        // Main class method, invoked by the execution engine.
        public override TaskStatus OnUpdate()
        {
            //Have AI look at the player
            Vector3 lookAtPosition = targetToShootAt.transform.position;
            lookAtPosition.y = gameObject.transform.position.y;
            gameObject.transform.LookAt(lookAtPosition);

            if (delay > 0)
            {
                ++elapsed;
                elapsed %= delay;
                if (elapsed != 0)
                {
                    return TaskStatus.RUNNING;
                }
            }

            base.OnUpdate();
            return TaskStatus.RUNNING;
        }
    }
}