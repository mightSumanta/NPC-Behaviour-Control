using Chapter2;
using Chapter3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter7
{
	public class BigItemManager_Master : MonoBehaviour 
	{
        private PlayerManager_Master playerManagerMasterScript;

        public delegate void BigItemGeneralEventHandler();
        public event BigItemGeneralEventHandler ThrowBigItemEvent;
        public event BigItemGeneralEventHandler putDownItemEvent;

        public delegate void BigItemPickupActionEventHandler(Transform item);
        public event BigItemPickupActionEventHandler PickupActionEvent;

        void OnEnable()
		{
            initiate();
		}

		void initiate()
		{
            if (GameManager_References._player != null)
                playerManagerMasterScript = GameManager_References._player.GetComponent<PlayerManager_Master>();
        }

        public void callThrowBigItemEvent()
        {
            if (ThrowBigItemEvent != null)
            {
                ThrowBigItemEvent();
            }
            playerManagerMasterScript.callHandsEmptyEvent();
            playerManagerMasterScript.callInventoryChangedEvent();
        }

        public void callputDownItemEvent()
        {
            if (putDownItemEvent != null)
                putDownItemEvent();
            playerManagerMasterScript.callHandsEmptyEvent();
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
