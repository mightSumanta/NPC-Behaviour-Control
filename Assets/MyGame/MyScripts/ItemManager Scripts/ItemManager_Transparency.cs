using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter4
{
	public class ItemManager_Transparency : MonoBehaviour 
	{

        private ItemManager_Master itemManagerMasterScript;
        public Material tranparentMat;
        private Material defaultMat;

		void OnEnable()
		{
            initiate();
            itemManagerMasterScript.PickupItemEvent += setToTransparentMat;
            itemManagerMasterScript.ThrowItemEvent += setToDefaultMat;
		}

		void OnDisable()
		{
            itemManagerMasterScript.PickupItemEvent -= setToTransparentMat;
            itemManagerMasterScript.ThrowItemEvent -= setToDefaultMat;
        }
	
		void Start () 
		{
            captureStartingMaterial();
		}
	
		void initiate()
		{
            itemManagerMasterScript = GetComponent<ItemManager_Master>();
		}

        void captureStartingMaterial()
        {
            defaultMat = GetComponent<Renderer>().material;
        }

        void setToDefaultMat()
        {
            GetComponent<Renderer>().material = defaultMat;
        }

        void setToTransparentMat()
        {
            GetComponent<Renderer>().material = tranparentMat;
        }
    }

}
