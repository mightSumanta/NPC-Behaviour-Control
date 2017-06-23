using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter8
{
	public class MeleeManager_SwingWeapon : MonoBehaviour 
	{
        private MeleeManager_Master meleeManagerMasterScript;
        public Collider myCollider;
        public Rigidbody myRigidBody;
        public Animator myAnimator;
		
		void OnEnable()
		{
            initiate();
            meleeManagerMasterScript.playerInputEvent += meleeSwing;
		}

		void OnDisable()
		{
            meleeManagerMasterScript.playerInputEvent -= meleeSwing;
        }
	
		void initiate()
		{
            meleeManagerMasterScript = GetComponent<MeleeManager_Master>();
		}

        void meleeSwing()
        {
            myCollider.enabled = true;
            myRigidBody.isKinematic = false;
            myAnimator.SetTrigger("Melee");
        }

        void meleeSwingComplete()
        {
            myCollider.enabled = false;
            myRigidBody.isKinematic = true;
            meleeManagerMasterScript.isInUse = false;
        }
	}

}
