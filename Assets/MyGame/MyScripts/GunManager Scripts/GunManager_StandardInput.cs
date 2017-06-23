using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter2;
using Chapter3;


namespace Chapter6
{
    public class GunManager_StandardInput : MonoBehaviour
    {
        private GunManager_Master gunManagerMasterScript;
        public float fireRate = 0.5f;
        private float nextCheck;
        private Transform myTransform;
        public bool isAutomatic;
        public bool hasBurstFire;
        private bool isBurstFireActive;
        public string reloadButtonName;
        public string toggleFireButtonName;
        public string fireButtonName;
        private bool isScrollUp;
        //private float checkScrollRate = 1f;
        //private float nextScrollCheck;


        void Start()
        {
            initiate();
        }

        void Update()
        {
            checkForFireAvail();
            checkForToggleFireMode();
            checkForReloadRequest();
        }

        void initiate()
        {
            gunManagerMasterScript = GetComponent<GunManager_Master>();
            myTransform = transform;
            gunManagerMasterScript.isGunLoaded = true; //Enables player to shoot right away
        }

        void checkForFireAvail()
        {
            if (Time.time > nextCheck && Time.timeScale > 0 &&
                myTransform.root.CompareTag(GameManager_References._playerTag))
            {
                if (isAutomatic && !isBurstFireActive)
                {
                    if (Input.GetButton(fireButtonName))
                    {
                        //Debug.Log("Full Auto");
                        attemptFire();
                    }
                }

                else if (isAutomatic && isBurstFireActive)
                {
                    if (Input.GetButtonDown(fireButtonName))
                    {
                        //Debug.Log("Burst Fire");
                        StartCoroutine(runBurstFire());
                    }
                }

                else if (!isAutomatic)
                {
                    if (Input.GetButtonDown(fireButtonName))
                    {
                        //Debug.Log("Single Shot");
                        attemptFire();
                    }
                }
            }
        }

        void attemptFire()
        {
            nextCheck = Time.time + fireRate;
            if (gunManagerMasterScript.isGunLoaded)
            {
                //Debug.Log("Shooting");
                gunManagerMasterScript.callPlayerInputEvent();
            }
            else
            {
                gunManagerMasterScript.callGunNotUsableEvent();
            }
        }

        void checkForReloadRequest()
        {
            if (Input.GetButtonDown(reloadButtonName) && Time.timeScale > 0 &&
                myTransform.root.CompareTag(GameManager_References._playerTag))
            {
                gunManagerMasterScript.callRequestReloadEvent();
            }
        }

        void checkForToggleFireMode()
        {
            if (Input.GetButtonDown(toggleFireButtonName) && Time.timeScale > 0 &&
                myTransform.root.CompareTag(GameManager_References._playerTag))
            {
                //Debug.Log("Fire Mode toggled");
                isBurstFireActive = !isBurstFireActive;
                gunManagerMasterScript.callToggleFireModeEvent();
            }
        }

        public bool isBurstFireActiveMethod()
        {
            if (isBurstFireActive)
                return true;
            else
                return false;
        }

        IEnumerator runBurstFire()
        {
            attemptFire();
            yield return new WaitForSeconds(fireRate);
            attemptFire();
            yield return new WaitForSeconds(fireRate);
            attemptFire();
        }

    }

}
