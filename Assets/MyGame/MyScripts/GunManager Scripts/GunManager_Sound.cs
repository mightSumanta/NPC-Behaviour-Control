using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter6
{
	public class GunManager_Sound : MonoBehaviour 
	{

        private GunManager_Master gunManagerMasterScript;
        private Transform myTransform;
        public float shootVol;
        public float reloadVol;
        public AudioClip[] shootSound;
        public AudioClip reloadSound;
		
		void OnEnable()
		{
            initiate();
            gunManagerMasterScript.playerInputEvent += playShootSound;
            gunManagerMasterScript.NPCInputEvent += NPCShotSound;
		}

		void OnDisable()
		{
            gunManagerMasterScript.playerInputEvent -= playShootSound;
            gunManagerMasterScript.NPCInputEvent -= NPCShotSound;
        }

        void initiate()
		{
            gunManagerMasterScript = GetComponent<GunManager_Master>();
            myTransform = transform;
		}

        void playShootSound()
        {
            if (shootSound.Length > 0)
            {
                int index = Random.Range(0, shootSound.Length);
                AudioSource.PlayClipAtPoint(shootSound[index], myTransform.position, shootVol);
            }
        }

        void playReloadSound()
        {
            if (reloadSound != null)
            {
                AudioSource.PlayClipAtPoint(reloadSound, myTransform.position, reloadVol);
            }
        }

        void NPCShotSound(float dummy)
        {
            playShootSound();
        }
	}

}
