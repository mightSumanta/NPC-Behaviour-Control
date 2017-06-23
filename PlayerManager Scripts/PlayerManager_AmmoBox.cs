using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter3
{
	public class PlayerManager_AmmoBox : MonoBehaviour 
	{
        private PlayerManager_Master playerManagerMasterScript;

        [System.Serializable]
        public class AmmoInformations
        {
            public string ammoName;
            public int ammoMaxQuantity;
            public int ammoCurrentCarried;

            public AmmoInformations(string aName, int aMQuantity, int aCCarried)
            {
                ammoName = aName;
                ammoMaxQuantity = aMQuantity;
                ammoCurrentCarried = aCCarried;
            }
        }

        public List<AmmoInformations> diffAmmoInfos = new List<AmmoInformations>();

		void OnEnable()
		{
            initiate();
            playerManagerMasterScript.AmmoPickupEvent += pickedUpAmmo;
		}

		void OnDisable()
		{
            playerManagerMasterScript.AmmoPickupEvent -= pickedUpAmmo;
		}

		void initiate()
		{
            playerManagerMasterScript = GetComponent<PlayerManager_Master>();
		}

        void pickedUpAmmo(string ammoName, int ammoQuantity)
        {
            //Debug.Log("Inside AmmoBox");
            for (int i = 0; i < diffAmmoInfos.Count; i++)
            {
                //Debug.Log(diffAmmoInfos[i].ammoName);
                if (diffAmmoInfos[i].ammoName == ammoName)
                {
                    diffAmmoInfos[i].ammoCurrentCarried += ammoQuantity;
                    if (diffAmmoInfos[i].ammoCurrentCarried > diffAmmoInfos[i].ammoMaxQuantity)
                        diffAmmoInfos[i].ammoCurrentCarried = diffAmmoInfos[i].ammoMaxQuantity;

                    playerManagerMasterScript.callAmmoChangedEvent();
                    break;
                }

            }
        }
	}

}
