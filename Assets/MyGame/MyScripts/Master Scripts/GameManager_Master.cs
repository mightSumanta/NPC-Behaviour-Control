using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter2
{
    public class GameManager_Master : MonoBehaviour
    {
        public delegate void GameManagerEventHandler();
        public event GameManagerEventHandler GameMenuToggleEvent;
        public event GameManagerEventHandler InventoryUIToggleEvent;
        public event GameManagerEventHandler RestartLevelEvent;
        public event GameManagerEventHandler GotoMainMenuEvent;
        public event GameManagerEventHandler GameOverEvent;
        public event GameManagerEventHandler InstructionPanleEvent;

        public bool isGameOver;
        public bool isInventoryUI;
        public bool isMenuUI;
        public bool isInstructionUI;

        public void callGameMenuToggleEvent()
        {
            if (GameMenuToggleEvent != null)
                GameMenuToggleEvent();
        }


        public void callInventoryUIEvent()
        {
            if (InventoryUIToggleEvent != null)
                InventoryUIToggleEvent();
        }

        public void callRestartLevelEvent()
        {
            if (RestartLevelEvent != null)
                RestartLevelEvent();
        }

        public void callGameOverEvent()
        {
            if (GameMenuToggleEvent != null)
            {
                GameMenuToggleEvent();
                GameOverEvent();
                Time.timeScale = 1;
                
                foreach(Transform child in GameManager_References._player.transform)
                {
                    foreach (Transform chi in child)
                        Destroy(chi.gameObject);
                    child.parent = null;
                }
                GameManager_References._player.SetActive(false);

                if (GameObject.Find("Resume") != null)
                    GameObject.Find("Resume").SetActive(false);
                isGameOver = true;
            }
        }

        public void callGotoMainMenu()
        {
            if (GotoMainMenuEvent != null)
                GotoMainMenuEvent();
        }

        public void callInstructionPanelEvent()
        {
            if (InstructionPanleEvent != null)
                InstructionPanleEvent();
        }
    }

}
