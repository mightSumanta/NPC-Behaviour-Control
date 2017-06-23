using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter2;


namespace Chapter5
{
	public class EnemyManager_Detection : MonoBehaviour 
	{

        private EnemyManager_Master enemyManagerMasterScript;
        private Transform golemTransform;
        public Transform head;
        public LayerMask playerLayer;
        public LayerMask sightLayer;
        private float checkRate;
        private float nextCheck;
        public float detectionRadius;
        private RaycastHit hit;

		void OnEnable()
		{
            initiate();
            enemyManagerMasterScript.EnemyDieEvent += disableThisScript;
		}

		void OnDisable()
		{
            enemyManagerMasterScript.EnemyDieEvent += disableThisScript;
        }

        void Update()
        {
            checkForEnemy();
        }

        void initiate()
		{
            enemyManagerMasterScript = GetComponent<EnemyManager_Master>();
            golemTransform = transform;
            if (head == null)
            {
                head = golemTransform;
            }
            checkRate = Random.Range(0.8f, 1.2f);
		}

        void checkForEnemy()
        {
            if (Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;
                Collider[] colliders = Physics.OverlapSphere(golemTransform.position, detectionRadius, playerLayer);

                if (colliders.Length > 0)
                {
                    foreach (Collider col in colliders)
                    {
                        if (col.CompareTag(GameManager_References._playerTag))
                        {
                            if (isTargetVisible(col.transform))
                                break;
                        }
                    }
                }
                else
                    enemyManagerMasterScript.callEnemyLostTargetEvent();
            }
        }

        bool isTargetVisible(Transform target)
        {
            if(Physics.Linecast(head.position, target.position, out hit, sightLayer))
            {
                if (hit.transform == target)
                {
                    enemyManagerMasterScript.callEnemyNavToTargetEvent(target);
                    return true;
                }
                else
                {
                    enemyManagerMasterScript.callEnemyLostTargetEvent();
                    return false;
                }
            }
            else
            {
                enemyManagerMasterScript.callEnemyLostTargetEvent();
                return false;
            }
        }

        void disableThisScript()
        {
            this.enabled = false;
        }
	}

}
