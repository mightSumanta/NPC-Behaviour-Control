using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter9
{
	public class NPCManager_TakeDamage : MonoBehaviour 
	{
        private NPCManager_Master npcManagerMasterScript;
        public int damageMultiplier = 1;
		
		void OnEnable()
		{
            initiate();
            npcManagerMasterScript.NPCDieEvent += resetObject;
		}

		void OnDisable()
		{
            npcManagerMasterScript.NPCDieEvent -= resetObject;
        }
	
		void initiate()
		{
            npcManagerMasterScript = transform.root.GetComponent<NPCManager_Master>();
		}

        public void damageProcess(int damage)
        {
            int damageToApply = damage * damageMultiplier;
            npcManagerMasterScript.callNPCDeductHealthEvent(damageToApply);
        }

        void resetObject()
        {
            if (GetComponent<Rigidbody>() != null)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<Rigidbody>().useGravity = true;
            }

            gameObject.layer = LayerMask.NameToLayer("Default");
            gameObject.tag = "Untagged";
        }

    }

}
