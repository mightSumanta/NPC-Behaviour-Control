using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter9
{
	public class NPCManager_CollisionField : MonoBehaviour 
	{
        private NPCManager_Master npcManagerMasterScript;
        private Rigidbody attackingItem;
        private int damageToApply;
        public float massRequirement = 50;
        public float speedRequirement = 1;
        private float damageFactor = 0.1f;

        void OnEnable()
		{
            initiate();
            npcManagerMasterScript.NPCDieEvent += disableGameObject;
        }

		void OnDisable()
		{
            npcManagerMasterScript.NPCDieEvent -= disableGameObject;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Rigidbody>() != null)
            {
                attackingItem = other.GetComponent<Rigidbody>();
                if (attackingItem.mass >= massRequirement &&
                    attackingItem.velocity.sqrMagnitude >= speedRequirement * speedRequirement)
                {
                    damageToApply = (int)(damageFactor * attackingItem.mass * attackingItem.velocity.magnitude);
                    npcManagerMasterScript.callNPCDeductHealthEvent(damageToApply);
                    //Debug.Log(damageToApply);
                }
            }
        }

        void initiate()
		{
            npcManagerMasterScript = transform.root.GetComponent<NPCManager_Master>();
		}

        void disableGameObject()
        {
            gameObject.SetActive(false);
        }
    }

}
