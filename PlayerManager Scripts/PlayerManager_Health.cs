using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Chapter2;


namespace Chapter3
{
	public class PlayerManager_Health : MonoBehaviour 
	{
        private GameManager_Master gameManagerMasterScript;
        private PlayerManager_Master playerManagerMasterScript;
        public int playerHealth;
        public Text healthText;
        public GameObject healthPanel;
        private float checkRate = 5;
        private float nextCheck;

		void OnEnable()
		{
            initiate();
            gameManagerMasterScript.GameMenuToggleEvent += toggleUI;
            gameManagerMasterScript.GameOverEvent += toggleUI;
            playerManagerMasterScript.IncreasePlayerHealthEvent += increaseHealth;
            playerManagerMasterScript.DecreasePlayerHealthEvent += decreaseHealth;
		}

		void OnDisable()
		{
            gameManagerMasterScript.GameMenuToggleEvent -= toggleUI;
            gameManagerMasterScript.GameOverEvent -= toggleUI;
            playerManagerMasterScript.IncreasePlayerHealthEvent -= increaseHealth;
            playerManagerMasterScript.DecreasePlayerHealthEvent -= decreaseHealth;
        }

        void Update()
        {
            if(Time.time > nextCheck)
            {
                if(playerHealth < 100)
                {
                    playerManagerMasterScript.callIncreasePlayerHealthEvent(10);
                }
                nextCheck = Time.time + checkRate;
            }
        }

        void Start()
        {
            //StartCoroutine(testHealthDeduction());
        }

        void initiate()
		{
            gameManagerMasterScript = GameObject.Find("GameManager").GetComponent<GameManager_Master>();
            playerManagerMasterScript = GetComponent<PlayerManager_Master>();
		}

        IEnumerator testHealthDeduction()
        {
            yield return new WaitForSeconds(2);
            playerManagerMasterScript.callDecreasePlayerHealthEvent(50);
        }

        void increaseHealth(int heal)
        {
            playerHealth += heal;
            if (playerHealth > 100)
                playerHealth = 100;
            setUI();
        }

        void decreaseHealth(int damage)
        {
            playerHealth -= damage;
            if (playerHealth <= 0)
            {
                playerHealth = 0;
                gameManagerMasterScript.callGameOverEvent();
            }
            setUI();
        }

        void setUI()
        {
            if (healthText != null)
                healthText.text = playerHealth.ToString();
        }

        void toggleUI()
        {
            if (healthPanel != null)
                healthPanel.SetActive(!healthPanel.activeSelf);
        }
	}

}
