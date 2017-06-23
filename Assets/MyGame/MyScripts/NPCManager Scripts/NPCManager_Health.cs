using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter9
{
	public class NPCManager_Health : MonoBehaviour 
	{
        private NPCManager_Master npcManagerMasterScript;
        public int npcHealth = 100;
        private bool healthCritical;
        private int healthLow = 25;

		void OnEnable()
		{
            initiate();
            npcManagerMasterScript.NPCDeductHealthEvent += decreaseHealth;
            npcManagerMasterScript.NPCIncreaseHealthEvent += increaseHealth;
		}

		void OnDisable()
		{
            npcManagerMasterScript.NPCDeductHealthEvent += decreaseHealth;
            npcManagerMasterScript.NPCIncreaseHealthEvent += increaseHealth;
        }
	
		//void Update () 
		//{
		//	if (Input.GetKeyDown(KeyCode.Period))
  //          {
  //              npcManagerMasterScript.callNPCIncreaseHealthEvent(10);
  //          }
		//}

		void initiate()
		{
            npcManagerMasterScript = GetComponent<NPCManager_Master>();
		}

        void increaseHealth(int healthChange)
        {
            npcHealth += healthChange;
             
            if (npcHealth > 100)
            {
                npcHealth = 100;
            }

            checkHealthFraction();
        }

        void decreaseHealth(int healthChange)
        {
            npcHealth -= healthChange;

            //Debug.Log(npcHealth);

            if (npcHealth <= 0)
            {
                npcHealth = 0;
                npcManagerMasterScript.callNPCDieEvent();
                Destroy(gameObject, Random.Range(10, 20));
            }

            checkHealthFraction();
        }

        void checkHealthFraction()
        {
            if (npcHealth <= healthLow && npcHealth > 0)
            {
                npcManagerMasterScript.callNPCHealthLowEvent();
                healthCritical = true;
            }
            else if (npcHealth > healthLow && healthCritical)
            {
                npcManagerMasterScript.callHealthRecoveredEvent();
                healthCritical = false;
            }
        }
	}

}
