using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter2
{
    public class GameManager_ToggleCursor : MonoBehaviour
    {
        GameManager_Master gameManagerMasterScript;
        bool isCursorEnabled;

        private void OnEnable()
        {
            initiate();
            gameManagerMasterScript.GameMenuToggleEvent += toggleCursor;
//            gameManagerMasterScript.GameOverEvent += toggleCursor;
            gameManagerMasterScript.InventoryUIToggleEvent += toggleCursor;
        }

        private void OnDisable()
        {
            gameManagerMasterScript.GameMenuToggleEvent -= toggleCursor;
//            gameManagerMasterScript.GameOverEvent -= toggleCursor;
            gameManagerMasterScript.InventoryUIToggleEvent -= toggleCursor;
        }

        void Update()
        {
            checkAction();
        }

        void initiate()
        {
            isCursorEnabled = false;
            gameManagerMasterScript = GetComponent<GameManager_Master>();
        }

        void toggleCursor()
        {
            isCursorEnabled = !isCursorEnabled;
        }

        void checkAction()
        {
            if(isCursorEnabled)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

}
