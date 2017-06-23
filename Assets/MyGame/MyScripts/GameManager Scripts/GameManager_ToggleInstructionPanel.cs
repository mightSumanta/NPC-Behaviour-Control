using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Chapter2
{
	public class GameManager_ToggleInstructionPanel : MonoBehaviour 
	{
        public GameObject myInstructionPanel;
        public GameObject menuPanel;
        GameManager_Master gameManagerMasterScript;

	
		void OnEnable()
		{
            initiate();
            gameManagerMasterScript.InstructionPanleEvent += toggleInstructionPanel;
            gameManagerMasterScript.GameOverEvent += onGameOverAction;
		}

		void OnDisable()
		{
            gameManagerMasterScript.InstructionPanleEvent -= toggleInstructionPanel;
            gameManagerMasterScript.GameOverEvent -= onGameOverAction;
		}

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0 && gameManagerMasterScript.isInstructionUI)
            {
                gameManagerMasterScript.isInstructionUI = !gameManagerMasterScript.isInstructionUI;
                setMenuPanel();
            }
        }

        void initiate()
		{
            gameManagerMasterScript = GetComponent<GameManager_Master>();
        }

        void toggleInstructionPanel()
        {
            if (myInstructionPanel != null)
            {
                gameManagerMasterScript.isInstructionUI = !gameManagerMasterScript.isInstructionUI;
                menuPanel.SetActive(!menuPanel.activeSelf);
                myInstructionPanel.SetActive(!myInstructionPanel.activeSelf);
            }
        }

        void setMenuPanel()
        {
            menuPanel.SetActive(true);
            myInstructionPanel.SetActive(false);
            gameManagerMasterScript.callGameMenuToggleEvent();
        }

        void onGameOverAction()
        {
            if (myInstructionPanel != null)
                myInstructionPanel.SetActive(false);
        }
	}

}
