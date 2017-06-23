using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter2;

namespace Chapter3
{
	public class PlayerManager_Master : MonoBehaviour 
	{
        public delegate void PlayerGeneralEventHandler();
        public event PlayerGeneralEventHandler InventoryChangedEvent;
        public event PlayerGeneralEventHandler HandsEmptyEvent;
        public event PlayerGeneralEventHandler AmmoChangedEvent;
        public event PlayerGeneralEventHandler BigItemPickupEvent;

        public delegate void AmmoPickupEventHandler(string ammoName, int quantity);
        public event AmmoPickupEventHandler AmmoPickupEvent;

        public delegate void PlayerHealthEventHandler(int healthChange);
        public event PlayerHealthEventHandler DecreasePlayerHealthEvent;
        public event PlayerHealthEventHandler IncreasePlayerHealthEvent;

        public void callInventoryChangedEvent()
        {
            if (InventoryChangedEvent != null)
                InventoryChangedEvent();
        }

        public void callHandsEmptyEvent()
        {
            if (HandsEmptyEvent != null)
                HandsEmptyEvent();
        }

        public void callAmmoChangedEvent()
        {
            if (AmmoChangedEvent != null)
                AmmoChangedEvent();
        }

        public void callBigItemPickupEvent()
        {
            if (BigItemPickupEvent != null)
                BigItemPickupEvent();
        }

        public void callAmmoPickupEvent(string ammoName, int quantity)
        {
            if (AmmoPickupEvent != null)
                AmmoPickupEvent(ammoName, quantity);
        }

        public void callDecreasePlayerHealthEvent(int damage)
        {
            if (DecreasePlayerHealthEvent != null)
                DecreasePlayerHealthEvent(damage);
        }

        public void callIncreasePlayerHealthEvent(int heal)
        {
            if (IncreasePlayerHealthEvent != null)
                IncreasePlayerHealthEvent(heal);
        }

    }

}
