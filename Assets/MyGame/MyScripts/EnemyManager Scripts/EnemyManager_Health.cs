using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter5
{
	public class EnemyManager_Health : MonoBehaviour 
	{

        private EnemyManager_Master enemyManagerMasterScript;
        public int maxHealth = 100;
        int enemyHealth;
        public int enemyMinHealth = 25;
        private float checkRate = 5;
        private float nextCheck;

		
		void OnEnable()
		{
            initiate();
            enemyManagerMasterScript.EnemyLosesHealthEvent += enemyLosesHealth;
            enemyManagerMasterScript.EnemyIncreaseHealthEvent += enemyRecoversHealth;
		}

		void OnDisable()
		{
            enemyManagerMasterScript.EnemyLosesHealthEvent -= enemyLosesHealth;
            enemyManagerMasterScript.EnemyIncreaseHealthEvent -= enemyRecoversHealth;
        }
	
		void initiate()
		{
            enemyManagerMasterScript = GetComponent<EnemyManager_Master>();
            enemyHealth = maxHealth;
        }

        void Update()
        {
            if(Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;
                if (enemyHealth < maxHealth)
                {
                    enemyManagerMasterScript.callEnemyIncreaseHealthEvent(10);
                    checkForHealthRecovery();
                }
            }
        }

        void enemyLosesHealth(int damage)
        {
            enemyHealth -= damage;
            if (enemyHealth <= 0)
            {
                enemyHealth = 0;
                enemyManagerMasterScript.callEnemyDieEvent();
                Destroy(gameObject, Random.Range(10, 15));
            }
            checkForMinHealth();
        }

        void checkForMinHealth()
        {
            if (enemyHealth <= enemyMinHealth && enemyHealth > 0)
                enemyManagerMasterScript.callEnemyHealthLowEvent();
        }

        void checkForHealthRecovery()
        {
            if (enemyHealth == maxHealth )
                enemyManagerMasterScript.callEnemyHealthRecoveredEvent();
        }

        void enemyRecoversHealth(int healthChange)
        {
            enemyHealth += healthChange;
            if (enemyHealth > maxHealth)
            {
                enemyHealth = maxHealth;
            }
            checkForMinHealth();
        }
    }

}
