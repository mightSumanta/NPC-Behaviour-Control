using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter2
{
    public class GameManager_References : MonoBehaviour
    {
        //public static bool isGameManagerReferencesComplete;

        public string playerTag;
        public static string _playerTag;

        public string itemTag;
        public static string _itemTag;

        public string enemyTag;
        public static string _enemyTag;

        public static GameObject _player;

        private void OnEnable()
        {
            if(playerTag == "")
            {
                Debug.LogWarning("Provide player tag first");
            }

            if (enemyTag == "")
            {
                Debug.LogWarning("Provide enemy tag first");
            }

            if (itemTag == "")
            {
                Debug.LogWarning("Provide item tag first");
            }

            _playerTag = playerTag;
            _enemyTag = enemyTag;
            _itemTag = itemTag;

            _player = GameObject.FindGameObjectWithTag(_playerTag);

            //isGameManagerReferencesComplete = true;
        }


    }

}
