using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter4
{
	public class ItemManager_UI : MonoBehaviour 
	{
        private ItemManager_Master itemManagerMasterScript;
        public GameObject myUI;

		void OnEnable()
		{
            initiate();
            itemManagerMasterScript.ThrowItemEvent += disableMyUI;
            itemManagerMasterScript.PickupItemEvent += enableMyUI;
		}

		void OnDisable()
		{
            itemManagerMasterScript.ThrowItemEvent -= disableMyUI;
            itemManagerMasterScript.PickupItemEvent -= enableMyUI;
        }

		void initiate()
		{
            itemManagerMasterScript = GetComponent <ItemManager_Master>();
		}

        void enableMyUI()
        {
            if (myUI != null)
                myUI.SetActive(true);
        }

        void disableMyUI()
        {
            if (myUI != null)
                myUI.SetActive(false);
        }
	}

}
