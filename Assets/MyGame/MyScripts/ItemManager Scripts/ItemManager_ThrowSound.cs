using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter4
{
	public class ItemManager_ThrowSound : MonoBehaviour 
	{

        private ItemManager_Master itemManagerMasterScript;

        public float myVolume;
        public AudioClip throwSound;

        void OnEnable()
		{
            initiate();
            itemManagerMasterScript.ThrowItemEvent += playSound;
        }

        void OnDisable()
		{
            itemManagerMasterScript.ThrowItemEvent -= playSound;
        }

        void initiate()
		{
            itemManagerMasterScript = GetComponent<ItemManager_Master>();
        }

        void playSound()
        {
            if (throwSound != null) 
                AudioSource.PlayClipAtPoint(throwSound, transform.position, myVolume);
        }
	}

}
