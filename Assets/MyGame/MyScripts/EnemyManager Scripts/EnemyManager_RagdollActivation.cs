using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter5
{
	public class EnemyManager_RagdollActivation : MonoBehaviour 
	{

        private EnemyManager_Master enemyManagerMasterScript;
        private Collider myCollider;
        private Rigidbody myRigidbody;

		
		void OnEnable()
		{
            initiate();
            enemyManagerMasterScript.EnemyDieEvent += activateRagdoll;
		}

		void OnDisable()
		{
            enemyManagerMasterScript.EnemyDieEvent -= activateRagdoll;
        }
	
		void initiate()
		{
            enemyManagerMasterScript = transform.root.GetComponent<EnemyManager_Master>();
            if (GetComponent<Collider>() != null)
                myCollider = GetComponent<Collider>();
            if (GetComponent<Rigidbody>() != null)
                myRigidbody = GetComponent<Rigidbody>();
        }

        void activateRagdoll()
        {
            if (myRigidbody != null)
            {
                myRigidbody.isKinematic = false;
                myRigidbody.useGravity = true;
            }

            if (myCollider != null)
            {
                myCollider.isTrigger = false;
                myCollider.enabled = true;
            }

        }
	}

}
