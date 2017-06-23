using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Chapter2
{
    public class MainMenu : MonoBehaviour
    {
        GameManager_Master myScript;

        private void Start()
        {
            myScript = GetComponent<GameManager_Master>();
        }

        public void playGame()
        {
            //Application.LoadLevel(1);
            SceneManager.LoadScene(1);
            myScript.callGotoMainMenu();

        }
       
        public void exitGame()
        {
            Application.Quit();
        }
    }

}
