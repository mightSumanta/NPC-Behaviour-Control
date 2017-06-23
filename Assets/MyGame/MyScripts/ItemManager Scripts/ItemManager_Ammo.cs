using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Chapter2;
using Chapter3;

namespace Chapter4
{
	public class ItemManager_Ammo : MonoBehaviour 
	{

        private ItemManager_Master itemManagerMasterscript;
        private GameObject myPlayer;
        public string ammoName;
        public int quantity;
        public bool isTriggerPickup;

        //void Start()
        //{
        //    
        //}

        void OnEnable()
		{
            initiate();
            itemManagerMasterscript.PickupItemEvent += ammoPickUp;
            //StartCoroutine(delayScript());
		}

		void OnDisable()
		{
			itemManagerMasterscript.PickupItemEvent -= ammoPickUp;
		}

        void OnTriggerEnter(Collider myCollider)
        {
            if (myCollider.CompareTag(GameManager_References._playerTag) && isTriggerPickup)
                ammoPickUp();
        }
        
        void initiate()
		{
            itemManagerMasterscript = GetComponent<ItemManager_Master>();
            myPlayer = GameManager_References._player;

            if (isTriggerPickup)
            {
                if (GetComponent<Collider>() != null)
                    GetComponent<Collider>().isTrigger = true;
                if (GetComponent<Rigidbody>() != null)
                    GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        //IEnumerator delayScript()
        //{
        //    yield return new WaitForSeconds(0.01f);

        //}

        void ammoPickUp()
        {
            //Debug.Log("Inside ItemManager_Ammo " + ammoName + " " + quantity);
            myPlayer.GetComponent<PlayerManager_Master>().callAmmoPickupEvent(ammoName, quantity);
            Destroy(gameObject);
        }

	}

}
