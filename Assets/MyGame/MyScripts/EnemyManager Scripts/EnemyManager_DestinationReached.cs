using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Chapter5
{
	public class EnemyManager_DestinationReached : MonoBehaviour 
	{
        private EnemyManager_Master enemyManagerMasterScript;
        private NavMeshAgent myNavMeshAgent;
        private float checkRate;
        private float nextCheck;

        void Update() 
		{
            if (Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;
                checkIfDestinationReached();
            }
        }

		void OnEnable()
		{
            initiate();
            enemyManagerMasterScript.EnemyDieEvent += disableThisScript;
		}

		void OnDisable()
		{
            enemyManagerMasterScript.EnemyDieEvent -= disableThisScript;
		}

		void initiate()
		{
            enemyManagerMasterScript = GetComponent<EnemyManager_Master>();
            if (GetComponent<NavMeshAgent>() != null)
                myNavMeshAgent = GetComponent<NavMeshAgent>();
            checkRate = Random.Range(0.3f, 0.4f);
        }

        void checkIfDestinationReached()
        {
            if (myNavMeshAgent.remainingDistance < myNavMeshAgent.stoppingDistance)
            {
                enemyManagerMasterScript.callEnemyReachedTargeEvent();
                enemyManagerMasterScript.isOnRoute = false;
            }
        }

        void disableThisScript()
        {
            this.enabled = false;
        }
	}

}
