using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter2;
using Chapter7;


namespace Chapter4
{
	public class ItemManager_Layer : MonoBehaviour 
	{

        private ItemManager_Master itemManagerMasterScript;
        public string itemThrowLayer;
        public string itemPickupLayer;

		void OnEnable()
		{
            initiate();
            setLayerOnEnable();

            itemManagerMasterScript.PickupItemEvent += setItemPickupLayer;
            itemManagerMasterScript.ThrowItemEvent += setItemThrowLayer;
		}

		void OnDisable()
		{
            itemManagerMasterScript.PickupItemEvent -= setItemPickupLayer;
            itemManagerMasterScript.ThrowItemEvent -= setItemThrowLayer;
        }

		void initiate()
		{
            itemManagerMasterScript = GetComponent<ItemManager_Master>();
		}

        void setItemThrowLayer()
        {
            setLayer(transform, itemThrowLayer);
        }

        void setItemPickupLayer()
        {
            setLayer(transform, itemPickupLayer);
        }

        void setLayerOnEnable()
        {
            if (itemPickupLayer == "")
                itemPickupLayer = "Item";
            if (itemThrowLayer == "")
                itemThrowLayer = "Item";
            //if (!GameManager_References.isGameManagerReferencesComplete)
           if (transform.root.CompareTag(GameManager_References._playerTag))
                setItemPickupLayer();
           else
                setItemThrowLayer();
        }

        void setLayer(Transform tForm, string itemLayer)
        {
            tForm.gameObject.layer = LayerMask.NameToLayer(itemLayer);

            foreach (Transform child in tForm)
                setLayer(child, itemLayer);
        }

        IEnumerator delay()
        {
            yield return new WaitForSeconds(0.1f);
            
        }
    }

}
