using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter4
{
	public class ItemManager_AnimationControl : MonoBehaviour 
	{
        private ItemManager_Master itemManagerMasterScript;
        public Animator myAnimator;

        void OnEnable()
		{
            initiate();
            itemManagerMasterScript.ThrowItemEvent += disableMyAnimator;
            itemManagerMasterScript.PickupItemEvent += enableMyAnimator;
		}

		void OnDisable()
		{
            itemManagerMasterScript.ThrowItemEvent -= disableMyAnimator;
            itemManagerMasterScript.PickupItemEvent -= enableMyAnimator;
        }

		void initiate()
		{
            itemManagerMasterScript = GetComponent<ItemManager_Master>();
		}

        void enableMyAnimator()
        {
            if (myAnimator != null)
                myAnimator.enabled = true;
        }

        void disableMyAnimator()
        {
            if (myAnimator != null)
                myAnimator.enabled = false;
        }
	}

}
