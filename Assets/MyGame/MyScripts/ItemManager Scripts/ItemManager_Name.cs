using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter5
{
    public class ItemManager_Name : MonoBehaviour
    {
        public string itemName;

        void OnEnable()
        {
            initiate();
        }

        void initiate()
        {
            transform.gameObject.name = itemName;
        }
    }

}
