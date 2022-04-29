using UnityEngine;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using BBUnity.Actions;        // GOAction
using UnityEngine.AI;

namespace BBSamples.PQSG // Programmers Quick Start Guide
{

    [Action("Behaviours/BehaviourScripts/DoneShootProjectile")]
    [Help("Clone a 'bullet' and shoots it throught the Forward axis with the " +
          "specified velocity.")]
    public class DoneShootProjectile : GOAction
    {
        // Define the input parameter "shootPoint".
        [InParam("shootPoint")]
        public Transform shootPoint;

        // Define the input parameter "bullet" (the prefab to be cloned).
        [InParam("bullet")]
        public GameObject bullet;

        // Define the input parameter velocity, and provide a default
        // value of 30.0 when used as CONSTANT in the editor.
        [InParam("velocity", DefaultValue = 60.0f)]
        public float velocity;

        [InParam("upwardForce")]
        public float upwardForce;

        //For the Animator reference
        private Animator anim;

        //What is the target
        [InParam("Target to shoot at")]
        [Help("Target to check the distance")]
        public GameObject targetToShootAt;

        //For the NavMeshAgent reference
        private NavMeshAgent navAgent;


        public override void OnStart()
        {         
            navAgent = gameObject.GetComponent<NavMeshAgent>();
            anim = gameObject.GetComponent<Animator>();
            navAgent.SetDestination(gameObject.transform.position);
            anim.SetBool("walking", false);
            
            base.OnStart();
        }

        public override TaskStatus OnUpdate()
        {
            if (shootPoint == null)
            {
                return TaskStatus.FAILED;
            }

            if (Time.timeScale == 0) {
                return TaskStatus.COMPLETED;
            }


            //Quaternion rotation = Quaternion.LookRotation(target.transform.position - gameObject.transform.position, gameObject.transform.TransformDirection(Vector3.up));
            shootPoint.LookAt(targetToShootAt.transform);
            anim.SetTrigger("attacking");
            GameObject newBullet = GameObject.Instantiate(bullet, shootPoint.position, Quaternion.identity) as GameObject;
            newBullet.GetComponent<Rigidbody>().velocity = velocity * shootPoint.forward;
            newBullet.GetComponent<Rigidbody>().AddForce(gameObject.transform.up * upwardForce, ForceMode.Impulse);           

            return TaskStatus.COMPLETED;
        }
    }
}