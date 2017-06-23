using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter2;
using Chapter3;

namespace Chapter4
{
	public class ItemManager_Master : MonoBehaviour 
	{
        private PlayerManager_Master playerManagerMasterScript;

        public delegate void ItemGeneralEventHandler();
        public event ItemGeneralEventHandler ThrowItemEvent;
        public event ItemGeneralEventHandler PickupItemEvent;

        public delegate void ItemPickupActionEventHandler(Transform item);
        public event ItemPickupActionEventHandler PickupActionEvent;
        
		void OnEnable()
		{
            initiate();	
		}

		void initiate()
		{
            if (GameManager_References._player != null)
                playerManagerMasterScript = GameManager_References._player.GetComponent<PlayerManager_Master>();
		}

        public void callThrowItemEvent()
        {
            if (ThrowItemEvent != null)
            {
                ThrowItemEvent();
            }
            playerManagerMasterScript.callHandsEmptyEvent();
            playerManagerMasterScript.callInventoryChangedEvent();
        }

        public void callPickupItemEvent()
        {
            if (PickupItemEvent != null)
            {
                PickupItemEvent();
            }
            playerManagerMasterScript.callInventoryChangedEvent();
        }

        public void callItemPickupActionEvent(Transform itemObj)
        {
            if (PickupActionEvent != null)
            {
                PickupActionEvent(itemObj);
            }
        }

	}

}
