using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter9
{
	public class NPCManager_Animator : MonoBehaviour 
	{

        private NPCManager_Master npcManagerMasterScript;
        private Animator myAnimator;

		void OnEnable()
		{
            initiate();
            npcManagerMasterScript.NPCAttackAnimEvent += activateAttackAnimation;
            npcManagerMasterScript.NPCStruckAnimEvent += activateStruckAnimation;
            npcManagerMasterScript.NPCWalkAnimEvent += activateWalkAnimation;
            npcManagerMasterScript.NPCRecoveredAnimEvent += activateRecoveredAnimation;
            npcManagerMasterScript.NPCIdleAnimEvent += activateIdleAnimation;
		}

		void OnDisable()
		{
            npcManagerMasterScript.NPCAttackAnimEvent -= activateAttackAnimation;
            npcManagerMasterScript.NPCStruckAnimEvent -= activateStruckAnimation;
            npcManagerMasterScript.NPCWalkAnimEvent -= activateWalkAnimation;
            npcManagerMasterScript.NPCRecoveredAnimEvent -= activateRecoveredAnimation;
            npcManagerMasterScript.NPCIdleAnimEvent -= activateIdleAnimation;
        }
	
		void initiate()
		{
            npcManagerMasterScript = GetComponent<NPCManager_Master>();

            if (GetComponent<Animator>() != null)
            {
                myAnimator = GetComponent<Animator>();
            }
		}

        void activateWalkAnimation()
        {
            if (myAnimator != null)
            {
                if (myAnimator.enabled)
                {
                    myAnimator.SetBool(npcManagerMasterScript.animIsPursuing, true);
                }
            }
        }

        void activateIdleAnimation()
        {
            if (myAnimator != null)
            {
                if (myAnimator.enabled)
                {
                    myAnimator.SetBool(npcManagerMasterScript.animIsPursuing, false);
                }
            }
        }

        void activateAttackAnimation()
        {
            if (myAnimator != null)
            {
                if (myAnimator.enabled)
                {
                    myAnimator.SetTrigger(npcManagerMasterScript.animMeleeTrigger);
                }
            }
        }

        void activateRecoveredAnimation()
        {
            if (myAnimator != null)
            {
                if (myAnimator.enabled)
                {
                    myAnimator.SetTrigger(npcManagerMasterScript.animRecoveredTrigger);
                }
            }
        }

        void activateStruckAnimation()
        {
            if (myAnimator != null)
            {
                if (myAnimator.enabled)
                {
                    myAnimator.SetTrigger(npcManagerMasterScript.animStruckTrigger);
                }
            }
        }
    }

}
