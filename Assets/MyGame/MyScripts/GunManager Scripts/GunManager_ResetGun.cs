using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter2;
using Chapter4;


namespace Chapter6
{
	public class GunManager_ResetGun : MonoBehaviour 
	{

        private GunManager_Master gunManagerMasterScript;
        private ItemManager_Master itemManagerMasterScript;
        //private GameManager_Master gameManagerMasterScript;


        void OnEnable()
		{
            initiate();
            if (itemManagerMasterScript != null)
                itemManagerMasterScript.ThrowItemEvent += resetGun;
		}

		void OnDisable()
		{
            if (itemManagerMasterScript != null)
                itemManagerMasterScript.ThrowItemEvent -= resetGun;
        }

        //void Update()
        //{
        //    if (Time.timeScale == 0)
        //        resetGun();
        //}

        void initiate()
		{
            gunManagerMasterScript = GetComponent<GunManager_Master>();
            //gameManagerMasterScript = GameManager_References._player.GetComponent<GameManager_Master>();

            if (GetComponent<ItemManager_Master>() != null)
                itemManagerMasterScript = GetComponent<ItemManager_Master>();
		}

        void resetGun()
        {
            gunManagerMasterScript.callRequestGunResetEvent();
        }
	}

}
