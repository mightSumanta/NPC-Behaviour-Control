using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Chapter2;
using Chapter6;
using Chapter7;

namespace Chapter3
{
	public class PlayerManager_Inventory : MonoBehaviour 
	{
        //public Transform inventoryPlayerParent;
        public Transform inventoryUIParent;
        public GameObject uiButton;
        public Transform myTransform;

        //private GunManager_Master gunManagerMasterScript;
        private PlayerManager_Master playerManagerMasterScript;
        private GameManager_Master gameManagerMasterScript;
        private GameManager_ToggleInventoryUI inventoryUIScript;
        private float timeToPlaceInHands = 0.1f;
        private Transform currentlyHeldItem;
        private int counter;
        private string buttonText;
        private List<Transform> listInventory = new List<Transform>();
        public int currentActiveItemIndex;
        private int itemIndex;
        

        void OnEnable()
		{
            initiate();
            StartCoroutine(firstUpdateInventory());
            gameManagerMasterScript.RestartLevelEvent += updateInventoryListAndUI;
            gameManagerMasterScript.RestartLevelEvent += checkIfHandsEmpty;
            gameManagerMasterScript.GotoMainMenuEvent += updateInventoryListAndUI;
            gameManagerMasterScript.GotoMainMenuEvent += checkIfHandsEmpty;
            gameManagerMasterScript.InventoryUIToggleEvent += updateInventoryListAndUI;
            gameManagerMasterScript.InventoryUIToggleEvent += checkIfHandsEmpty;
            playerManagerMasterScript.InventoryChangedEvent += updateInventoryListAndUI;
            playerManagerMasterScript.InventoryChangedEvent += checkIfHandsEmpty;
            playerManagerMasterScript.BigItemPickupEvent += bigItemPickUpAction;
            playerManagerMasterScript.HandsEmptyEvent += clearHands;
        }

		void OnDisable()
		{
            gameManagerMasterScript.RestartLevelEvent -= updateInventoryListAndUI;
            gameManagerMasterScript.RestartLevelEvent -= checkIfHandsEmpty;
            gameManagerMasterScript.GotoMainMenuEvent -= updateInventoryListAndUI;
            gameManagerMasterScript.GotoMainMenuEvent -= checkIfHandsEmpty;
            gameManagerMasterScript.InventoryUIToggleEvent -= updateInventoryListAndUI;
            gameManagerMasterScript.InventoryUIToggleEvent -= checkIfHandsEmpty;
            playerManagerMasterScript.InventoryChangedEvent -= updateInventoryListAndUI;
            playerManagerMasterScript.InventoryChangedEvent -= checkIfHandsEmpty;
            playerManagerMasterScript.HandsEmptyEvent -= clearHands;
        }

        void Update()
        {
            if (scrollWheelAction())
                StartCoroutine(delayCheck());
        }

        void initiate()
		{
            inventoryUIScript = GameObject.Find("GameManager").GetComponent<GameManager_ToggleInventoryUI>();
            playerManagerMasterScript = GetComponent<PlayerManager_Master>();
            gameManagerMasterScript = GameObject.Find("GameManager").GetComponent<GameManager_Master>();
            //InvokeRepeating("scrollWheelAction", 0.01f, 0.5f);
        }

        void updateInventoryListAndUI()
        {
            counter = 0;
            listInventory.Clear();
            listInventory.TrimExcess();

            clearInventoryUI();
            foreach(Transform child in myTransform)
            {
                if (child.CompareTag("Item"))
                {
                    listInventory.Add(child);
                    GameObject obj = Instantiate(uiButton) as GameObject;
                    buttonText = child.gameObject.name;
                    obj.GetComponentInChildren<Text>().text = buttonText;
                    int index = counter;
                    obj.GetComponent<Button>().onClick.AddListener(delegate { activateInventoryItem(index); });
                    obj.GetComponent<Button>().onClick.AddListener(inventoryUIScript.toggleInventoryUI);
                    obj.transform.SetParent(inventoryUIParent, false);
                    counter++;
                }
            }
                
        }

        void checkIfHandsEmpty()
        {
            foreach (Transform child in myTransform)
            {
                if (child.CompareTag("BigItem"))
                {
                    currentlyHeldItem = child;
                    //Debug.Log(currentlyHeldItem.name);
                }
            }
            if (currentlyHeldItem == null && listInventory.Count > 0)
            {
                StartCoroutine(placeItemsInHands(listInventory[listInventory.Count - 1]));
            }
        }

        void clearHands()
        {
            currentlyHeldItem = null;
        }

        void clearInventoryUI()
        {
            foreach(Transform childItem in inventoryUIParent)
            {
                Destroy(childItem.gameObject);
            }
        }

        public void activateInventoryItem(int inventoryIndex)
        {
            //gunManagerMasterScript.callRequestGunResetEvent();
            if (currentlyHeldItem == null || !currentlyHeldItem.CompareTag("BigItem"))
            {
                currentActiveItemIndex = inventoryIndex;
                deactivateAllInventoryItems();
                StartCoroutine(placeItemsInHands(listInventory[inventoryIndex]));
            }
            else
            {
                currentlyHeldItem.GetComponent<BigItemManager_Master>().callThrowBigItemEvent();
                activateInventoryItem(inventoryIndex);
            }
        }

        void deactivateAllInventoryItems()
        {
            foreach (Transform child in myTransform)
            {
                if (child.CompareTag("Item"))
                    child.gameObject.SetActive(false);
            }
        }

        bool scrollWheelAction()
        {
            if (listInventory.Count > 0 && Time.timeScale > 0)
            {
                if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                {
                    if (currentActiveItemIndex + 1 > listInventory.Count - 1)
                    {
                        itemIndex = 0;
                    }
                    else
                        itemIndex = currentActiveItemIndex + 1;
                    //Debug.Log("check");
                    activateInventoryItem(itemIndex);
                    return true;
                }

                else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                {
                    if (currentActiveItemIndex - 1 < 0)
                    {
                        itemIndex = listInventory.Count - 1;
                    }
                    else
                        itemIndex = currentActiveItemIndex - 1;
                    //Debug.Log("checkRev");
                    activateInventoryItem(itemIndex);
                    return true;
                }
                return false;
            }

            else
                return false;
        }

        void checkForSanity()
        {
            int count = 0;
            foreach(Transform child in myTransform)
            {
                if (child.tag == GameManager_References._itemTag && child.gameObject.activeSelf == true) 
                {
                    count++;
                }
            }
            if (count > 1)
            {
                activateInventoryItem(currentActiveItemIndex);
                //Debug.Log("wtf");
            }
        }

        void bigItemPickUpAction()
        {
            deactivateAllInventoryItems();
            updateInventoryListAndUI();
            checkIfHandsEmpty();
        }

        IEnumerator placeItemsInHands(Transform itemToPlace)
        {
            yield return new WaitForSeconds(timeToPlaceInHands);
            currentlyHeldItem = itemToPlace;
            currentlyHeldItem.gameObject.SetActive(true);
            playerManagerMasterScript.callInventoryChangedEvent();
        }

        IEnumerator firstUpdateInventory()
        {
            yield return new WaitForSeconds(0.1f);
            updateInventoryListAndUI();
            checkIfHandsEmpty();
        }

        IEnumerator delayCheck()
        {
            yield return new WaitForSeconds(0.2f);
            checkForSanity(); 
        }
    }

}
