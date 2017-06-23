using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter2
{
    public class GameManager_ToggleMenu : MonoBehaviour
    {

        GameManager_Master gameManagerMaster;
        public GameObject menu;

        void Start()
        {
            gameManagerMaster.isMenuUI = false;
            gameManagerMaster = GetComponent<GameManager_Master>();
        }

        void Update()
        { 
            checkForToggleMenuRequest();
        }

        private void OnEnable()
        {
            initiate();
            gameManagerMaster.GameMenuToggleEvent += toggleMenu;
        }

        private void OnDisable()
        {
            gameManagerMaster.GameMenuToggleEvent -= toggleMenu;
        }

        void initiate()
        {
            gameManagerMaster = GetComponent<GameManager_Master>();
        }

        void checkForToggleMenuRequest()
        {
            if (Input.GetKeyUp(KeyCode.Escape) && !gameManagerMaster.isInventoryUI && !gameManagerMaster.isGameOver)
                gameManagerMaster.callGameMenuToggleEvent();
        }

        void toggleMenu()
        {
            if (menu != null)
            {
                menu.SetActive(!menu.activeSelf);
                gameManagerMaster.isMenuUI = !gameManagerMaster.isMenuUI;
            }
            else
                Debug.LogWarning("Assign a UI to the ToggleMenuScript first");
        }
    }

}
