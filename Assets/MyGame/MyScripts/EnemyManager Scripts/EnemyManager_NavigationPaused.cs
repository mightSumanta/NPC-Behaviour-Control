using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Chapter5
{
    public class EnemyManager_NavigationPaused : MonoBehaviour
    {

        private EnemyManager_Master enemyManagerMasterScript;
        private NavMeshAgent myNavMeshAgent;
        private float pauseTime = 1;


        void OnEnable()
        {
            initiate();
            enemyManagerMasterScript.EnemyDieEvent += disableThisScript;
            enemyManagerMasterScript.EnemyLosesHealthEvent += pauseNavMeshAgent;
        }

        void OnDisable()
        {
            enemyManagerMasterScript.EnemyDieEvent -= disableThisScript;
            enemyManagerMasterScript.EnemyLosesHealthEvent -= pauseNavMeshAgent;
        }

        void initiate()
        {
            enemyManagerMasterScript = GetComponent<EnemyManager_Master>();
            if (GetComponent<NavMeshAgent>() != null)
                myNavMeshAgent = GetComponent<NavMeshAgent>();
        }

        void pauseNavMeshAgent(int dummy)
        {
            if (myNavMeshAgent != null)
            {
                if (myNavMeshAgent.enabled)
                {
                    myNavMeshAgent.ResetPath();
                    enemyManagerMasterScript.isNavPaused = true;
                    StartCoroutine(restartNavMeshAgent());
                }
            }
        }

        IEnumerator restartNavMeshAgent()
        {
            yield return new WaitForSeconds(pauseTime);
            enemyManagerMasterScript.isNavPaused = false;
        }

        void disableThisScript()
        {
            StopAllCoroutines();
            this.enabled = false;
        }
    }

}
