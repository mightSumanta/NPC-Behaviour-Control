using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter2;


namespace Chapter4
{
	public class ItemManager_RigidBody : MonoBehaviour 
	{
        private ItemManager_Master itemManagerMasterScript;
        public Rigidbody[] rigidBodies;

		void OnEnable()
		{
            initiate();
            checkIfItemInHand();
            itemManagerMasterScript.ThrowItemEvent += setIsKinematicToFalse;
            itemManagerMasterScript.PickupItemEvent += setIsKinematicToTrue;
		}

		void OnDisable()
		{
            itemManagerMasterScript.ThrowItemEvent -= setIsKinematicToFalse;
            itemManagerMasterScript.PickupItemEvent -= setIsKinematicToTrue;
        }

		void initiate()
		{
            itemManagerMasterScript = GetComponent<ItemManager_Master>();
		}

        void checkIfItemInHand()
        {
            if (transform.root.CompareTag(GameManager_References._playerTag))
            {
                setIsKinematicToTrue();
            }
        }

        void setIsKinematicToTrue()
        {
            if (rigidBodies.Length > 0)
            {
                foreach (Rigidbody rBody in rigidBodies)
                    rBody.isKinematic = true;
            }
        }

        void setIsKinematicToFalse()
        {
            if (rigidBodies.Length > 0)
            {
                foreach (Rigidbody rBody in rigidBodies)
                    rBody.isKinematic = false;
            }
        }

        IEnumerator delay()
        {
            yield return new WaitForSeconds(0.1f);
        }
    }

}
