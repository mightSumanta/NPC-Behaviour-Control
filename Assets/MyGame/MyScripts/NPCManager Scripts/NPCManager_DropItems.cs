using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter4;


namespace Chapter9
{
	public class NPCManager_DropItems : MonoBehaviour 
	{
        private NPCManager_Master npcManagerMasterScript;
        public GameObject[] itemsToDrop;

		void OnEnable()
		{
            initiate();
            npcManagerMasterScript.NPCDieEvent += dropItem;
		}

		void OnDisable()
		{
            npcManagerMasterScript.NPCDieEvent -= dropItem;
        }
	
		void initiate()
		{
            npcManagerMasterScript = GetComponent<NPCManager_Master>();
		}

        void dropItem()
        {
            if (itemsToDrop.Length > 0)
            {
                foreach (GameObject item in itemsToDrop)
                {
                    if (item.transform.root == transform.root)
                    {
                        StartCoroutine(pauseBeforeDrop(item));
                    }
                }
            }
        }

        IEnumerator pauseBeforeDrop(GameObject itemToDrop)
        {
            yield return new WaitForSeconds(0.05f);
            itemToDrop.SetActive(true);
            itemToDrop.transform.parent = null;
            yield return new WaitForSeconds(0.05f);
            if (itemToDrop.GetComponent<ItemManager_Master>() != null)
            {
                itemToDrop.GetComponent<ItemManager_Master>().callThrowItemEvent();
            }
        }
	}

}
