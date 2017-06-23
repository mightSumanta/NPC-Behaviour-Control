using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Chapter5
{
	public class EnemyManager_NavFlee : MonoBehaviour 
	{

        public bool isFleeing;
        private EnemyManager_Master enemyManagerMasterScript;
        private NavMeshAgent myNavMeshAgent;
        private NavMeshHit myNavHit;
        private Transform myTransform;
        private Transform fleeTarget;
        private Vector3 runPosition;
        private Vector3 directionToPlayer;
        public float fleeRange = 25;
        private float checkRate;
        private float nextCheck;

		
		void OnEnable()
		{
            initiate();
            enemyManagerMasterScript.EnemyDieEvent += disableThisScript;
            enemyManagerMasterScript.EnemyNavToTargetEvent += setFleeTarget;
            enemyManagerMasterScript.EnemyHealthLowEvent += flee;
            enemyManagerMasterScript.EnemyHealthRecoveredEvent += stopFleeing;
		}

		void OnDisable()
		{
            enemyManagerMasterScript.EnemyDieEvent -= disableThisScript;
            enemyManagerMasterScript.EnemyNavToTargetEvent -= setFleeTarget;
            enemyManagerMasterScript.EnemyHealthLowEvent -= flee;
            enemyManagerMasterScript.EnemyHealthRecoveredEvent -= stopFleeing;
        }
	
		void Update () 
		{
            if (Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;
                checkForFlee();
            }
		}

		void initiate()
		{
            enemyManagerMasterScript = GetComponent<EnemyManager_Master>();
            myTransform = transform;
            if (GetComponent<NavMeshAgent>() != null)
            {
                myNavMeshAgent = GetComponent<NavMeshAgent>();
            }
            checkRate = Random.Range(0.3f, 0.4f);
		}

        void setFleeTarget(Transform target)
        {
            fleeTarget = target;
        }

        void flee()
        {
            isFleeing = true;
            if (GetComponent<EnemyManager_Pursue>() != null)
                GetComponent<EnemyManager_Pursue>().enabled = false;
        }

        void stopFleeing()
        {
            isFleeing = false;
            if (GetComponent<EnemyManager_Pursue>() != null)
                GetComponent<EnemyManager_Pursue>().enabled = true;
        }

        bool directionToFlee(out Vector3 result)
        {
            directionToPlayer = myTransform.position - fleeTarget.position;
            Vector3 checkPosition = myTransform.position + directionToPlayer;

            if(NavMesh.SamplePosition(checkPosition, out myNavHit, 1.0f, NavMesh.AllAreas))
            {
                result = myNavHit.position;
                return true;
            }
            else
            {
                result = myTransform.position;
                return false;
            }
        }

        void checkForFlee()
        {
            if(isFleeing)
            {
                if (fleeTarget != null && !enemyManagerMasterScript.isOnRoute && !enemyManagerMasterScript.isNavPaused)
                {
                    if (directionToFlee(out runPosition) && Vector3.Distance(myTransform.position, fleeTarget.position) < fleeRange)
                    {
                        myNavMeshAgent.SetDestination(runPosition);
                        enemyManagerMasterScript.callEnemyWalkingEvent();
                        enemyManagerMasterScript.isOnRoute = true;
                    }
                }
            }
        }

        void disableThisScript()
        {
            this.enabled = false;
        }
	}

}
