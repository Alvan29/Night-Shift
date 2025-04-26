using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;


namespace GDL_tutorial
{
    [DisallowMultipleComponent]
   public class EnemyReferences :MonoBehaviour
    {
        [HideInInspector] public NavMeshAgent navMeshagent;
        [HideInInspector] public Animator animator;
        [HideInInspector] public EnemyShooter shooter;

        [Header ("Stats")]

        public float pathUpdateDelay = 2.0f;
        private void Awake()
        {
            navMeshagent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            shooter = GetComponent<EnemyShooter>();
        }
    }
}
