using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter7
{
	public class DestructibleManager_HealthLowEffect : MonoBehaviour 
	{

        private DestructibleManager_Master destructibleManagerMasterScript;
        public bool isHealthLow = false;
        public float degenRate = 1;
        private float nextDegenTime;
        public int healthLoss = 7;
        public GameObject[] lowHealthEffect;

		void OnEnable()
		{
            initiate();
            destructibleManagerMasterScript.healthLowEvent += spawnLowHealthEffect;
            destructibleManagerMasterScript.healthLowEvent += checkForHealthLow;
		}

		void OnDisable()
		{
            destructibleManagerMasterScript.healthLowEvent -= spawnLowHealthEffect;
            destructibleManagerMasterScript.healthLowEvent -= checkForHealthLow;
        }

        void Update()
        {
            if (Time.time > nextDegenTime && Time.timeScale > 0 && isHealthLow)
            {
                nextDegenTime = Time.time + degenRate;
                destructibleManagerMasterScript.callDeductHealthEvent(healthLoss);
            }
        }

        void initiate()
		{
            destructibleManagerMasterScript = GetComponent<DestructibleManager_Master>();
		}

        void checkForHealthLow()
        {
            isHealthLow = true;
        }

        void spawnLowHealthEffect()
        {
            if (lowHealthEffect != null)
            {
                for (int i = 0; i < lowHealthEffect.Length; i++)
                {
                    lowHealthEffect[i].SetActive(true);
                }
            }
        }
	}

}
