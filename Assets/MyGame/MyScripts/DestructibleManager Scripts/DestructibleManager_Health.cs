using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter7
{
	public class DestructibleManager_Health : MonoBehaviour 
	{

        private DestructibleManager_Master destructibleManagerMasterScript;
        public int health;
        private int startingHealth;
        private bool isExploding = false; 
		
		void OnEnable()
		{
            initiate();
            destructibleManagerMasterScript.deductHealthEvent += deductHealth;
		}

		void OnDisable()
		{
            destructibleManagerMasterScript.deductHealthEvent -= deductHealth;
        }
	
		void initiate()
		{
            destructibleManagerMasterScript = GetComponent<DestructibleManager_Master>();
            startingHealth = health;
		}

        void deductHealth(int damage)
        {
            health -= damage;
            checkIfHealthLow();
            if (health <= 0 && !isExploding)
            {
                isExploding = true;
                destructibleManagerMasterScript.callDestroyMeEvent();
            }
        }

        void checkIfHealthLow()
        {
            if (health <= startingHealth / 2)
            {
                destructibleManagerMasterScript.callHealthLowEvent();
            }
        }
	}

}
