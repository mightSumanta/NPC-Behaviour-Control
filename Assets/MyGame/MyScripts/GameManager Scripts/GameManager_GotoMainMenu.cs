using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Chapter2
{
    public class GameManager_GotoMainMenu : MonoBehaviour
    {
        GameManager_Master gameManagerMasterScript;

        private void OnEnable()
        {
            initiate();
            gameManagerMasterScript.GotoMainMenuEvent += gotoMainMenu;
        }

        private void OnDisable()
        {
            gameManagerMasterScript.GotoMainMenuEvent -= gotoMainMenu;
        }

        void initiate()
        {
            gameManagerMasterScript = GetComponent<GameManager_Master>();
        }

        void gotoMainMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}
