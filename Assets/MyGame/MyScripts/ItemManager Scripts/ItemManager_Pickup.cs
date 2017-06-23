using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter2;
using Chapter3;


namespace Chapter4
{
	public class ItemManager_Pickup : MonoBehaviour 
	{

        private ItemManager_Master itemManagerMasterScript;

		void OnEnable()
		{
            initiate();
            itemManagerMasterScript.PickupActionEvent += pickupAction;
		}

		void OnDisable()
		{
            itemManagerMasterScript.PickupActionEvent -= pickupAction;
        }

		void initiate()
		{
            itemManagerMasterScript = GetComponent<ItemManager_Master>();
		}

        void pickupAction(Transform parentTransform)
        {
            if (transform.CompareTag(GameManager_References._itemTag))
            {
                transform.SetParent(parentTransform);
                itemManagerMasterScript.callPickupItemEvent();
                transform.gameObject.SetActive(false);
            }
       
        }
	}

}
