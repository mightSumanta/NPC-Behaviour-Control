using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter8
{
	public class MeleeManager_Sound : MonoBehaviour 
	{
        private MeleeManager_Master meleeManagerMasterScript;
        //private Transform myTransform;
        public AudioClip swingSound;
        public AudioClip strikeSound;
        public float swingVol = 0.7f;
        public float strikeVol = 0.7f;
        float temp;

        void OnEnable()
		{
            initiate();
            meleeManagerMasterScript.hitEnemyEvent += playStrikeSound;
		}

		void OnDisable()
		{
            meleeManagerMasterScript.hitEnemyEvent -= playStrikeSound;
        }
	
		void initiate()
		{
            meleeManagerMasterScript = GetComponent<MeleeManager_Master>();
            //myTransform = transform;
            temp = swingVol;
        }

        void playSwingSound()      //Called by Animator
        {
            if (swingSound != null)
            {
                swingVol = temp;
                AudioSource.PlayClipAtPoint(swingSound, transform.position, swingVol);
            }
        }

        void playStrikeSound(Collision dummyC, Transform dummyT)
        {
            if (strikeSound != null)
            {
                swingVol = 0; 
                AudioSource.PlayClipAtPoint(strikeSound, transform.position, strikeVol);
            }
        }
	}

}
