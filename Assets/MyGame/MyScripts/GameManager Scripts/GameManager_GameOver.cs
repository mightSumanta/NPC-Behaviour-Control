using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter2
{
    public class GameManager_GameOver : MonoBehaviour
    {
        private GameManager_Master gameManagerMasterscript;
        public GameObject gameOverPanel;

        private void OnEnable()
        {
            initiate();
            gameManagerMasterscript.GameOverEvent += showGameOverPanel;
        }

        private void OnDisable()
        {
            gameManagerMasterscript.GameOverEvent -= showGameOverPanel;
        }

        void initiate()
        {
            gameManagerMasterscript = GetComponent<GameManager_Master>();                
        }

        void showGameOverPanel()
        {
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }
        }
    }

}
