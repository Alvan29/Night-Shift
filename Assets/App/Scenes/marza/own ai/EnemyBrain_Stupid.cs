using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDL_tutorial
{
    public class EnemyBrain_Stupid : MonoBehaviour
    {
        public Transform target;

        private EnemyReferences enemyReferences;
        private float pathUpdateDeadline;
        private float shootingDistance;

        private void Awake()
        {
            enemyReferences = GetComponent<EnemyReferences>();
        }

        void Start()
        {
            shootingDistance = enemyReferences.navMeshagent.stoppingDistance;
        }

        void Update()
        {
            if (target != null)
            {
                bool inRange = Vector3.Distance(transform.position, target.position) <= shootingDistance;

                if (inRange)
                {
                    LookAtTarget();
                }
                else
                {
                    UpdatePath();
                }

                // Ganti parameter dari "Shoot" ke "shooting"
                enemyReferences.animator.SetBool("Shooting", inRange);
            }

            enemyReferences.animator.SetFloat("Speed", enemyReferences.navMeshagent.desiredVelocity.sqrMagnitude);
        }

        private void LookAtTarget()
        {
            Vector3 lookPos = target.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
        }

        private void UpdatePath()
        {
            if (Time.time >= pathUpdateDeadline)
            {
                Debug.Log("Update Path");
                pathUpdateDeadline = Time.time + enemyReferences.pathUpdateDelay;
                enemyReferences.navMeshagent.SetDestination(target.position);
            }
        }
    }
}
