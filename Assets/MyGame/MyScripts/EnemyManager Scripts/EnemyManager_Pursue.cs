using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Chapter5
{
	public class EnemyManager_Pursue : MonoBehaviour 
	{

        private EnemyManager_Master enemyManagerMasterScript;
        private NavMeshAgent myNavMeshAgent;
        private float checkRate;
        private float nextCheck;

        void OnEnable()
        {
            initiate();
            enemyManagerMasterScript.EnemyDieEvent += disableThisScript;
        }

        void OnDisable()
        {
            enemyManagerMasterScript.EnemyDieEvent -= disableThisScript;
        }

        void Update () 
		{
            if (Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;
                tryToChaseTarget();
            }
        }

		
		void initiate()
		{
            enemyManagerMasterScript = GetComponent<EnemyManager_Master>();
            if (GetComponent<NavMeshAgent>() != null)
                myNavMeshAgent = GetComponent<NavMeshAgent>();
            checkRate = Random.Range(0.8f, 1.2f);
		}

        void tryToChaseTarget()
        {
            if (enemyManagerMasterScript.enemyTarget != null && myNavMeshAgent != null && !enemyManagerMasterScript.isNavPaused)
            {
                myNavMeshAgent.SetDestination(enemyManagerMasterScript.enemyTarget.position);

                if (myNavMeshAgent.remainingDistance > myNavMeshAgent.stoppingDistance)
                {
                    enemyManagerMasterScript.callEnemyWalkingEvent();
                    enemyManagerMasterScript.isOnRoute = true;
                }
            }
        }

        void disableThisScript()
        {
            if (myNavMeshAgent != null)
                myNavMeshAgent.enabled = false;
            this.enabled = false;
        }
	}

}
