using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Chapter2;
using Chapter4;
using Chapter7;

namespace Chapter3
{
	public class PlayerManager_DetectPickupItem : MonoBehaviour 
	{
        private GameManager_Master gameManagerMasterScript;
        private ItemManager_Master itemManagerMasterScript;
        private bool inGUIMethod;
        public int fontSizeForDetectedUI = 10;

        public LayerMask providedLayer1;
        public LayerMask providedLayer2;
        public Transform rayFireTransformPoint;
        public string pickupButton;

        private Transform detectedItems;
        public Transform controller;
        private RaycastHit hit;
        private float detectionRange = 3;
        private float detectionRadius = 0.7f;
        private bool itemInRange;

        private float lableWidth = 100;
        private float lableHeight = 30;

        void OnEnable()
        {
            itemManagerMasterScript = GetComponent<ItemManager_Master>();
        }

        void Update () 
		{
            castRayForDetection();
            checkForItemPickupAction();
		}

        void castRayForDetection()
        {
            if (Physics.SphereCast(rayFireTransformPoint.position, detectionRadius, rayFireTransformPoint.forward,
                out hit, detectionRange, providedLayer1))
            {
                detectedItems = hit.transform;
                itemInRange = true;
            }

            else if (Physics.SphereCast(rayFireTransformPoint.position, detectionRadius, rayFireTransformPoint.forward,
                out hit, detectionRange, providedLayer2))
            {
                detectedItems = hit.transform;
                itemInRange = true;
            }

            else
            {
                foreach(Transform child in transform.FindChild("FirstPersonCharacter"))
                {
                    if (child.gameObject.activeSelf == true && (child.CompareTag("Item") || child.CompareTag("BigItem")))
                    {
                        detectedItems = child;
                    }
                }
                itemInRange = false;
            }
        }

        void checkForItemPickupAction()
        {
            if (Input.GetButtonDown(pickupButton) && Time.timeScale > 0 && itemInRange &&
                detectedItems.root.tag != GameManager_References._playerTag)// && detectedItems.tag == GameManager_References._itemTag)
            {
                //Debug.Log("Item picked up");
                detectedItems.GetComponent<ItemManager_Master>().callItemPickupActionEvent(controller);
                if (detectedItems.GetComponent<BigItemManager_Master>() != null)
                    detectedItems.GetComponent<BigItemManager_Master>().callItemPickupActionEvent(controller);
            }

            else if (detectedItems != null)
            {
                if (Input.GetButtonDown(pickupButton) && Time.timeScale > 0 && detectedItems.root.tag == GameManager_References._playerTag)
                {
                    if (itemManagerMasterScript != null && detectedItems.CompareTag(GameManager_References._itemTag))
                    {
                        detectedItems.GetComponent<ItemManager_Throw>().putDownAction();
                    }
                    else if (detectedItems.GetComponent<BigItemManager_Master>() != null && detectedItems.CompareTag("BigItem"))
                        detectedItems.GetComponent<BigItemManager_Master>().callputDownItemEvent();
                }
            }
        }

        void OnGUI()
        {
            GUIStyle myStyle = new GUIStyle(GUI.skin.button);
            myStyle.fontSize = fontSizeForDetectedUI;
            
            if (itemInRange && detectedItems != null && !detectedItems.root.CompareTag(GameManager_References._playerTag) 
                && !checkActiveUI())
                GUI.Label(new Rect(Screen.width / 2 - lableWidth / 2, Screen.height - 50, lableWidth, lableHeight), 
                    detectedItems.name, myStyle);

        }

        bool checkActiveUI()
        {
            int flag = 0;
            GameObject[] myObj = GameObject.FindGameObjectsWithTag("UI");
            foreach (GameObject obj in myObj)
            {
                if (obj.activeSelf == true)
                {
                    flag++;
                }
                break;
            }
            if (flag == 0)
                return false;
            else
                return true;
        }
	}

}
