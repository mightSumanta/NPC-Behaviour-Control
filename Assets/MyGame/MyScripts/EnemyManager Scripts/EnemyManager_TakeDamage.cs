using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter5
{
	public class EnemyManager_TakeDamage : MonoBehaviour 
	{

        private EnemyManager_Master enemyManagerMasterScript;
        public int damageMultiplier = 1;
        public bool shouldRemoveCollider;

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
            enemyManagerMasterScript = gameObject.transform.root.GetComponent<EnemyManager_Master>();
		}

        public void damageProcess(int damage)
        {
            int damageToApply = damage * damageMultiplier;
            enemyManagerMasterScript.callEnemyLosesHealthEvent(damageToApply);
        }

        void disableThisScript()
        {
            if (shouldRemoveCollider)
            {
                if (GetComponent<Collider>() != null)
                    Destroy(GetComponent<Collider>());
                if (GetComponent<Rigidbody>() != null)
                    Destroy(GetComponent<Rigidbody>());
            }

            Destroy(this);
        }
	}

}
