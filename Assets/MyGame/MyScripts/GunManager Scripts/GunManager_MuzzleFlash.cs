using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter6
{
	public class GunManager_MuzzleFlash : MonoBehaviour 
	{
        private GunManager_Master gunManagerMasterScript;
        public ParticleSystem muzzleFlash;



		void OnEnable()
		{
            initiate();
            gunManagerMasterScript.playerInputEvent += playMuzzleFlash;
            gunManagerMasterScript.NPCInputEvent += playMuzzleFlashForNPC;
		}

		void OnDisable()
		{
            gunManagerMasterScript.playerInputEvent -= playMuzzleFlash;
            gunManagerMasterScript.NPCInputEvent -= playMuzzleFlashForNPC;
        }
	
		void initiate()
		{
            gunManagerMasterScript = GetComponent<GunManager_Master>();
		}

        void playMuzzleFlash()
        {
            if (muzzleFlash != null)
            {
                muzzleFlash.Play();
            }
        }

        void playMuzzleFlashForNPC(float dummy)
        {
            if (muzzleFlash != null)
            {
                muzzleFlash.Play();
            }
        }
	}

}
