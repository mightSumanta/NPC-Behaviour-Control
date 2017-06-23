using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter2;


namespace Chapter8
{
	public class MeleeManager_StandardInput : MonoBehaviour 
	{
        private MeleeManager_Master meleeManagerMasterScript;
        private Transform myTransform;
        private float nextSwing;
        public string meleeButtonName;
		
		void Start () 
		{
            initiate();
		}
	
		void Update () 
		{
            checkForMeleeInput();
		}

		void initiate()
		{
            meleeManagerMasterScript = GetComponent<MeleeManager_Master>();
            myTransform = transform;
		}

        void checkForMeleeInput()
        {
            if (Time.timeScale > 0 && myTransform.root.CompareTag(GameManager_References._playerTag) &&
                !meleeManagerMasterScript.isInUse)
            {
                if (Input.GetButton(meleeButtonName) && Time.time > nextSwing)
                {
                    nextSwing = Time.time + meleeManagerMasterScript.swingRate;
                    meleeManagerMasterScript.isInUse = true;
                    meleeManagerMasterScript.callPlayerInputEvent();
                }
            }
        }
	}

}
