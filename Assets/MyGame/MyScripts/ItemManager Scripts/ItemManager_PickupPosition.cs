using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter2;


namespace Chapter4
{
	public class ItemManager_PickupPosition : MonoBehaviour 
	{

        private ItemManager_Master itemManagerMasterScript;
        public Vector3 itemLocalPosition;
        public Quaternion itemLocalRotation;

        void OnEnable()
		{
            initiate();
            setPositionOnPlayer();
            itemManagerMasterScript.PickupItemEvent += setPositionOnPlayer;
		}

		void OnDisable()
		{
            itemManagerMasterScript.PickupItemEvent -= setPositionOnPlayer;
        }

		void initiate()
		{
            itemManagerMasterScript = GetComponent<ItemManager_Master>();
		}

        void setPositionOnPlayer()
        {
            if (transform.root.CompareTag(GameManager_References._playerTag))
            {
                transform.localPosition = itemLocalPosition;
                transform.localRotation = itemLocalRotation;
            }
        }

        IEnumerator delay()
        {
            yield return new WaitForSeconds(0.1f);
            

        }
    }

}
