using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter9
{
	public class NPCManager_HeadLook : MonoBehaviour 
	{
        private NPCManager_StatePattern npc;
        private Animator myAnimator;

		void Start () 
		{
            initiate();
		}
	
		void initiate()
		{
            npc = GetComponent<NPCManager_StatePattern>();
            myAnimator = GetComponent<Animator>();
		}

        void OnAnimatorIK()
        {
            if (myAnimator.enabled)
            {
                if (npc.pursueTarget != null)
                {
                    myAnimator.SetLookAtWeight(1, 0.3f, 0.5f, 0.5f, 0.7f);
                    myAnimator.SetLookAtPosition(npc.pursueTarget.position);
                }
                else
                {
                    myAnimator.SetLookAtWeight(0);
                }
            }
        }
	}

}
