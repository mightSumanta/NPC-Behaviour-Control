using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter2
{
    public class GameManager_TogglePause : MonoBehaviour
    {
        GameManager_Master gameManagerMaster;
        bool isPaused;


        private void OnEnable()
        {
            initiate();
            gameManagerMaster.GameMenuToggleEvent += togglePause;
            gameManagerMaster.RestartLevelEvent += togglePause;
            gameManagerMaster.InventoryUIToggleEvent += togglePause;
            gameManagerMaster.GotoMainMenuEvent += togglePause;
        }

        private void OnDisable()
        {
            gameManagerMaster.GameMenuToggleEvent -= togglePause;
            gameManagerMaster.RestartLevelEvent -= togglePause;
            gameManagerMaster.InventoryUIToggleEvent -= togglePause;
            gameManagerMaster.GotoMainMenuEvent -= togglePause;
        }

        void initiate()
        {
            isPaused = true;
            gameManagerMaster = GetComponent<GameManager_Master>();
        }

        void togglePause()
        {
            if(!isPaused)
            {
                Time.timeScale = 1;
                isPaused = true;
            }
            else
            {
                Time.timeScale = 0;
                isPaused = false;
            }

        }

    }

}
