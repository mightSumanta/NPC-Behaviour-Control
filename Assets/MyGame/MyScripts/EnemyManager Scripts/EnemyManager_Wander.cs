using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Chapter5
{
	public class EnemyManager_Wander : MonoBehaviour 
	{
        private EnemyManager_Master enemyManagerMasterScript;
        private NavMeshAgent myNavMeshAgent;
        private float checkRate;
        private float nextCheck;
        private float wanderRange = 20;
        private Transform myTransform;
        private NavMeshHit navHit;
        private Vector3 wanderTarget;

        void Update() 
		{
            if (Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;
                checkForWanderAvail();
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
            checkRate = Random.Range(0.8f, 1.2f);

            myTransform = transform;
        }

        void checkForWanderAvail()
        {
            if (!enemyManagerMasterScript.isOnRoute && !enemyManagerMasterScript.isNavPaused)
            {
                if (enemyManagerMasterScript.enemyTarget == null)
                {
                    if (randomWanderTarget(myTransform.position, wanderRange, out wanderTarget))
                    {
                        myNavMeshAgent.SetDestination(wanderTarget);
                        enemyManagerMasterScript.isOnRoute = true;
                        enemyManagerMasterScript.callEnemyWalkingEvent();
                    }
                }
                
                else
                {
                    Vector3 toOther = enemyManagerMasterScript.enemyTarget.position - myTransform.position;
                    if (Vector3.Dot(toOther, myTransform.forward) < 0.98f)
                        if (randomWanderTarget(myTransform.position, wanderRange, out wanderTarget))
                        {
                            myNavMeshAgent.SetDestination(wanderTarget);
                            enemyManagerMasterScript.isOnRoute = true;
                            enemyManagerMasterScript.callEnemyWalkingEvent();
                        }

                }     
           
            }

        }

        bool randomWanderTarget(Vector3 centre, float range, out Vector3 result)
        {
            Vector3 randomPoint = centre + Random.insideUnitSphere * wanderRange;
            if (randomPoint.y < 0)
                randomPoint.y = 0;
            if (NavMesh.SamplePosition(randomPoint, out navHit, 10f, NavMesh.AllAreas))
            {
                result = navHit.position;
                return true;
            }
            else
            {
                result = randomPoint;
                return false;
            }
        }

        void disableThisScript()
        {
            this.enabled = false;
        }
	}

}
