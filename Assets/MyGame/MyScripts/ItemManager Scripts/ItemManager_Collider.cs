using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter2;


namespace Chapter4
{
	public class ItemManager_Collider : MonoBehaviour 
	{
        private ItemManager_Master itemManagerMasterScript;
        public Collider[] colliders;
        public PhysicMaterial myMaterial;
        
		void OnEnable()
		{
            initiate();
            checkIfItemInHand();
            itemManagerMasterScript.PickupItemEvent += turnCollidersOff;
            itemManagerMasterScript.ThrowItemEvent += turnCollidersOn;
		}

		void OnDisable()
		{
            itemManagerMasterScript.PickupItemEvent -= turnCollidersOff;
            itemManagerMasterScript.ThrowItemEvent -= turnCollidersOn;
        }

        void checkIfItemInHand()
        {
            if (transform.root.CompareTag(GameManager_References._playerTag))
                turnCollidersOff();
        }

		void initiate()
		{
            itemManagerMasterScript = GetComponent<ItemManager_Master>();
            colliders = transform.GetComponents<Collider>();
		}

        void turnCollidersOn()
        {
            if (colliders.Length > 0) 
            {
                foreach (Collider col in colliders) 
                {
                    col.enabled = true;

                    if (myMaterial != null)
                    {
                        col.material = myMaterial;
                    }
                }
            }
        }

        void turnCollidersOff()
        {
            if (colliders.Length > 0)
            {
                foreach (Collider col in colliders)
                {
                    col.enabled = false;
                }
            }
        }

        IEnumerator delay()
        {
            yield return new WaitForSeconds(0.1f);
        }
    }

}
