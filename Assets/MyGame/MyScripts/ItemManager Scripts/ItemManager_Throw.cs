using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter2;
using Chapter7;


namespace Chapter4
{
	public class ItemManager_Throw : MonoBehaviour 
	{
        private ItemManager_Master itemManagerMasterScript;
        private Transform myTransform;
        private Rigidbody myRigidBody;
        private Vector3 throwDirection;

        public bool canBeThrown;
        public string throwButtonName;
        public float throwForce;


		void OnEnable () 
		{
            initiate();
		}

        void Update () 
		{
            checkForThrowInput();
		}

		void initiate()
		{
            if (GetComponent<ItemManager_Master>() != null)
                itemManagerMasterScript = GetComponent<ItemManager_Master>();
            myTransform = transform;
            myRigidBody = GetComponent<Rigidbody>();
		}

        void checkForThrowInput()
        {
            if (Input.GetButtonDown(throwButtonName) && Time.timeScale > 0 && canBeThrown &&
                myTransform.root.CompareTag(GameManager_References._playerTag))
                throwActions();
        }

        void throwActions()
        {
            if (itemManagerMasterScript != null && transform.CompareTag(GameManager_References._itemTag))
            {
                //Debug.Log("Check");
                throwDirection = myTransform.parent.transform.forward;
                myTransform.parent = null;
                itemManagerMasterScript.callThrowItemEvent();
                throwItem();
            }
            if (GetComponent<BigItemManager_Master>() != null)
                GetComponent<BigItemManager_Master>().callThrowBigItemEvent();
        }

        public void putDownAction()
        {
            float temp = throwForce;
            throwForce = 1;
            if (itemManagerMasterScript != null && transform.CompareTag(GameManager_References._itemTag))
            {
                //Debug.Log("Check");
                throwForce = 1;
                throwDirection = myTransform.parent.transform.forward;
                myTransform.parent = null;
                itemManagerMasterScript.callThrowItemEvent();
                throwItem();
            }
            if (GetComponent<BigItemManager_Master>() != null)
                GetComponent<BigItemManager_Master>().callThrowBigItemEvent();
            throwForce = temp;
        }

        void throwItem()
        {
            myRigidBody.AddForce(throwDirection * throwForce, ForceMode.Impulse);
        }
	}

}
