using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter5
{
	public class EnemyManager_CollisionField : MonoBehaviour 
	{

        private EnemyManager_Master enemyManagerMasterScript;
        private Rigidbody attackingItem;
        private int damageToApply;
        public float massRequirement = 50;
        public float speedRequirement = 1;
        private float damageFactor = 0.1f;

		void OnEnable()
		{
            initiate();
            enemyManagerMasterScript.EnemyDieEvent += disableGameObject;
		}

		void OnDisable()
		{
            enemyManagerMasterScript.EnemyDieEvent += disableGameObject;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Rigidbody>() != null)
            {
                attackingItem = other.GetComponent<Rigidbody>();
                if (attackingItem.mass >= massRequirement &&
                    attackingItem.velocity.sqrMagnitude >= speedRequirement * speedRequirement)
                {
                    damageToApply = (int)(damageFactor * attackingItem.mass * attackingItem.velocity.magnitude);
                    enemyManagerMasterScript.callEnemyLosesHealthEvent(damageToApply);
                    //Debug.Log(damageToApply);
                }
            }
        }

        void initiate()
		{
            enemyManagerMasterScript = transform.root.GetComponent<EnemyManager_Master>();
            //Collider[] col = transform.root.GetComponentsInChildren<Collider>();
		}

        void disableGameObject()
        {
            gameObject.SetActive(false);
        }
	}

}
