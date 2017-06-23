using Chapter4;
using Chapter7;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter3;
using Chapter2;

namespace Chapter8
{
	public class MeleeManager_Reset : MonoBehaviour 
	{
        private MeleeManager_Master meleeManagerMasterScript;
        private ItemManager_Master itemManagerMasterScript;
        private BigItemManager_Master bigItemManagerMasterScript;
        private PlayerManager_Master playermanagerMasterScript;

		void OnEnable()
		{
            initiate();

            if (itemManagerMasterScript != null)
                itemManagerMasterScript.ThrowItemEvent += resetMelee;

            if (bigItemManagerMasterScript != null)
                bigItemManagerMasterScript.ThrowBigItemEvent += resetMelee;

            if (playermanagerMasterScript != null)
                playermanagerMasterScript.InventoryChangedEvent += resetMelee;
		}

		void OnDisable()
		{
            if (itemManagerMasterScript != null)
                itemManagerMasterScript.ThrowItemEvent -= resetMelee;

            if (bigItemManagerMasterScript != null)
                bigItemManagerMasterScript.ThrowBigItemEvent -= resetMelee;

            if (playermanagerMasterScript != null)
                playermanagerMasterScript.InventoryChangedEvent -= resetMelee;
        }
	
		void initiate()
		{
            meleeManagerMasterScript = GetComponent<MeleeManager_Master>();

            if (GetComponent<ItemManager_Master>() != null)
                itemManagerMasterScript = GetComponent<ItemManager_Master>();

            if (GetComponent<BigItemManager_Master>() != null)
                bigItemManagerMasterScript = GetComponent<BigItemManager_Master>();

            if (GameManager_References._player.GetComponent<PlayerManager_Master>() != null)
                playermanagerMasterScript = GameManager_References._player.GetComponent<PlayerManager_Master>();

        }

        void resetMelee()
        {
            meleeManagerMasterScript.isInUse = false;
        }
	}

}
