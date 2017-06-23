using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace Chapter2
{
    public class GameManager_TogglePlayerControl : MonoBehaviour
    {
        public FirstPersonController playerController;
        GameManager_Master gameMasterManagerScript;

        private void OnEnable()
        {
            initiate();
            gameMasterManagerScript.GameMenuToggleEvent += togglePlayerController;
            gameMasterManagerScript.InventoryUIToggleEvent += togglePlayerController;
            //gameMasterManagerScript.GameOverEvent += togglePlayerController;
        }

        private void OnDisable()
        {
            gameMasterManagerScript.GameMenuToggleEvent -= togglePlayerController;
            gameMasterManagerScript.InventoryUIToggleEvent -= togglePlayerController;
            //gameMasterManagerScript.GameOverEvent -= togglePlayerController;
        }

        void initiate()
        {
            gameMasterManagerScript = GetComponent<GameManager_Master>();
        }

        void togglePlayerController()
        {
            if(playerController != null)
            {
                playerController.enabled = !playerController.enabled;
            }

        }

    }

}
