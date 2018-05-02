using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for
        public float fovWidthAngle;
        public float fovDistance;


        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

            agent.updateRotation = false;
            agent.updatePosition = true;
        }


        private void Update()
        {
            //if (target != null)
            //    agent.SetDestination(target.position);

            Vector3 targetDir = target.position - transform.position;
            float angle = Vector3.Angle(targetDir, transform.forward);
            float distance = targetDir.magnitude;

            if (angle < fovWidthAngle && distance < fovDistance)
            {
                Debug.Log(this.name + " sees you!");
                Debug.DrawRay(transform.position, targetDir, Color.blue, 2);
            }
            //if (agent.remainingDistance > agent.stoppingDistance)
            //    character.Move(agent.desiredVelocity, false, false);
            //else
            //    character.Move(Vector3.zero, false, false);
        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Bullet")
            {
                Destroy(this.gameObject);
            }
            Debug.Log("Ow");
        }

        void OnDrawGizmosSelected()
        {

            Quaternion leftRayRotation = Quaternion.AngleAxis(-fovWidthAngle, Vector3.up);
            Quaternion rightRayRotation = Quaternion.AngleAxis(fovWidthAngle, Vector3.up);
            Vector3 leftRayDirection = leftRayRotation * transform.forward;
            Vector3 rightRayDirection = rightRayRotation * transform.forward;
            Gizmos.DrawRay(transform.position, leftRayDirection * fovDistance);
            Gizmos.DrawRay(transform.position, rightRayDirection * fovDistance);
        }
    }
}
