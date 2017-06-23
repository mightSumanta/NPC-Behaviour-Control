using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter6
{
	public class GunManager_Animator : MonoBehaviour 
	{

        private GunManager_Master gunManagerMasterScript;
        private Animator myAnimator;
		
		void OnEnable()
		{
            initiate();
            gunManagerMasterScript.playerInputEvent += playShootAnimation;
            gunManagerMasterScript.NPCInputEvent += NPCShootAnimation;
		}

		void OnDisable()
		{
            gunManagerMasterScript.playerInputEvent -= playShootAnimation;
            gunManagerMasterScript.NPCInputEvent -= NPCShootAnimation;
        }
	
		void initiate()
		{
            gunManagerMasterScript = GetComponent<GunManager_Master>();
            if (GetComponent<Animator>() != null)
                myAnimator = GetComponent<Animator>();
		}

        void playShootAnimation()
        {
            if (myAnimator != null)
            {
                myAnimator.SetTrigger("Shoot");
            }
        } 

        void NPCShootAnimation(float dummy)
        {
            playShootAnimation();
        }
	}

}
