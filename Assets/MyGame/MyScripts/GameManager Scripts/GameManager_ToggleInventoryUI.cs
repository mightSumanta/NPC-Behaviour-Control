using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter3;

namespace Chapter2
{
    public class GameManager_ToggleInventoryUI : MonoBehaviour
    {
        [Tooltip("Set true if this game has an Inventory")]
        public bool hasInventory;
        public GameObject inventoryUI;
        public string toggleInventoryButton;
        GameManager_Master gameManagerMasterScript;

        void Start()
        {
            initiate();
        }

        void Update()
        {
            checkAction();
        }

        void initiate()
        {
            gameManagerMasterScript = GetComponent<GameManager_Master>();
            if (toggleInventoryButton == "")
            {
                Debug.LogWarning("Provide ToggleInventoryButton name first");
                this.enabled = false;
            }

        }

        void checkAction()
        {
            if (Input.GetButtonUp(toggleInventoryButton) && !gameManagerMasterScript.isMenuUI
                && hasInventory && !gameManagerMasterScript.isGameOver)
            {
                toggleInventoryUI();
            }
        }

        public void toggleInventoryUI()
        {
            if (inventoryUI != null)
            {
                inventoryUI.SetActive(!inventoryUI.activeSelf);
                gameManagerMasterScript.isInventoryUI = !gameManagerMasterScript.isInventoryUI;
                gameManagerMasterScript.callInventoryUIEvent();
            }
        }
    }

}
