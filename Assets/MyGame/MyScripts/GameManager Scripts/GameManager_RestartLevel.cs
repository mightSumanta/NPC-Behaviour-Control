using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Chapter2
{
    public class GameManager_RestartLevel : MonoBehaviour
    {
        GameManager_Master gameManagerMasterScript;

        private void OnEnable()
        {
            initiate();
            gameManagerMasterScript.RestartLevelEvent += restartLevel;
        }

        private void OnDisable()
        {
            gameManagerMasterScript.RestartLevelEvent -= restartLevel;
        }

        void initiate()
        {
            gameManagerMasterScript = GameObject.Find("GameManager").GetComponent<GameManager_Master>();
        }

        void restartLevel()
        {
            //Application.LoadLevel(Application.loadedLevel);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

}
