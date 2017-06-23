using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter4
{
	public class ItemManager_Tag : MonoBehaviour 
	{
        public string itemTag;

		void OnEnable()
		{
            initiate();
		}

		void initiate()
		{
            transform.gameObject.tag = itemTag;
        }
	}

}
