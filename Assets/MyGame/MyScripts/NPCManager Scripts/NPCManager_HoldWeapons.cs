using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter9
{
	public class NPCManager_HoldWeapons : MonoBehaviour 
	{

        private NPCManager_StatePattern npc;
        private Animator myAnimator;
        public Transform rightHandTarget;
        public Transform leftHandTarget;
		
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
            if (npc.rangeWeapon == null)
            {
                return;
            }

            if (myAnimator.enabled)
            {
                if (npc.rangeWeapon.activeSelf)
                {
                    if (rightHandTarget != null)
                    {
                        myAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                        myAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                        myAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
                        myAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);
                    }
                    if (leftHandTarget != null)
                    {
                        myAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                        myAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                        myAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);
                        myAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);
                    }
                }
            }
        }
    }

}
