using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter2;
using Chapter3;


namespace Chapter6
{
	public class GunManager_AmmoProperties : MonoBehaviour 
	{
        private PlayerManager_Master playerManagerMasterScript;
        private GunManager_Master gunManagerMasterScript;
        private PlayerManager_AmmoBox myAmmoBox;
        private Animator myAnimator;

        public int clipSize;
        public int currentAmmo;
        public string ammoName;
        public float reloadTime;
		
		void OnEnable()
		{
            initiate();
            checkStartingSanity();
            checkAmmoStatus();
            gunManagerMasterScript.isReloading = false;

            gunManagerMasterScript.playerInputEvent += deductAmmo;
            gunManagerMasterScript.playerInputEvent += checkAmmoStatus;
            gunManagerMasterScript.requestReloadEvent += tryToReload;
            gunManagerMasterScript.gunNotUsableEvent += tryToReload;
            gunManagerMasterScript.requestGunResetEvent += resetGunReloading;

            if (playerManagerMasterScript != null)
            {
                playerManagerMasterScript.AmmoChangedEvent += requestUIUpdate;
            }

            if (myAmmoBox != null)
            {
                StartCoroutine(updateAmmoUIWhenEnabled());
            }
		}

		void OnDisable()
		{
            gunManagerMasterScript.playerInputEvent -= deductAmmo;
            gunManagerMasterScript.playerInputEvent -= checkAmmoStatus;
            gunManagerMasterScript.requestReloadEvent -= tryToReload;
            gunManagerMasterScript.gunNotUsableEvent -= tryToReload;
            gunManagerMasterScript.requestGunResetEvent -= resetGunReloading;

            if (playerManagerMasterScript != null)
            {
                playerManagerMasterScript.AmmoChangedEvent -= requestUIUpdate;
            }
        }
	
		void Start () 
		{
            //initiate();
            //StartCoroutine(updateAmmoUIWhenEnabled());

            //if (playerManagerMasterScript != null)
            //{
            //    playerManagerMasterScript.AmmoChangedEvent += requestUIUpdate;
            //}
        }
	
		void initiate()
		{
            gunManagerMasterScript = GetComponent<GunManager_Master>();
            if (GetComponent<Animator>() != null)
                myAnimator = GetComponent<Animator>();
            if (GameManager_References._player != null)
            {
                playerManagerMasterScript = GameManager_References._player.GetComponent<PlayerManager_Master>();
                myAmmoBox = GameManager_References._player.GetComponent<PlayerManager_AmmoBox>(); 
            }
        }

        void deductAmmo()
        {
            currentAmmo--;
            requestUIUpdate();
        }

        void tryToReload()
        {
            for (int i = 0; i < myAmmoBox.diffAmmoInfos.Count; i++)
            {
                if (myAmmoBox.diffAmmoInfos[i].ammoName == ammoName)
                {
                    if (myAmmoBox.diffAmmoInfos[i].ammoCurrentCarried > 0 && currentAmmo != clipSize &&
                        !gunManagerMasterScript.isReloading)
                    {
                        gunManagerMasterScript.isReloading = true;
                        gunManagerMasterScript.isGunLoaded = false;
                        

                        if (myAnimator != null)
                        {
                            myAnimator.SetTrigger("Reload");
                        }

                        else
                        {
                            StartCoroutine(reloadWithoutAnimation());
                        }
                    }

                    break;
                }
            }
        }

        void checkAmmoStatus()
        {
            if (currentAmmo <= 0)
            {
                currentAmmo = 0;
                gunManagerMasterScript.isGunLoaded = false;
            }

            else if (currentAmmo > 0)
            {
                gunManagerMasterScript.isGunLoaded = true;
            }
        }

        void checkStartingSanity()
        {
            if (currentAmmo > clipSize)
            {
                currentAmmo = clipSize;
            }
        }

        void requestUIUpdate()
        {
            for (int i = 0; i < myAmmoBox.diffAmmoInfos.Count; i++)
            {
                if (myAmmoBox.diffAmmoInfos[i].ammoName == ammoName)
                {
                    gunManagerMasterScript.callAmmoChangedEvent(currentAmmo, myAmmoBox.diffAmmoInfos[i].ammoCurrentCarried);
                    break;
                }
            }
        }

        void resetGunReloading()
        {
            gunManagerMasterScript.isReloading = false;
            checkAmmoStatus();
            requestUIUpdate();
        } 

        public void onReloadCompletion()
        {
            for (int i = 0; i < myAmmoBox.diffAmmoInfos.Count; i++)
            {
                if (myAmmoBox.diffAmmoInfos[i].ammoName == ammoName)
                {
                    int ammoToLoad = clipSize - currentAmmo;

                    if (myAmmoBox.diffAmmoInfos[i].ammoCurrentCarried >= ammoToLoad)
                    {
                        currentAmmo += ammoToLoad;
                        myAmmoBox.diffAmmoInfos[i].ammoCurrentCarried -= ammoToLoad;
                    }

                    else if (myAmmoBox.diffAmmoInfos[i].ammoCurrentCarried < ammoToLoad &&
                        myAmmoBox.diffAmmoInfos[i].ammoCurrentCarried != 0)
                    {
                        currentAmmo += myAmmoBox.diffAmmoInfos[i].ammoCurrentCarried;
                        myAmmoBox.diffAmmoInfos[i].ammoCurrentCarried = 0;
                    }
                    break;
                }
                
            }
            resetGunReloading();
        }

        IEnumerator reloadWithoutAnimation()
        {
            yield return new WaitForSeconds(reloadTime);
            onReloadCompletion();
        }

        IEnumerator updateAmmoUIWhenEnabled()
        {
            yield return new WaitForSeconds(0.05f); //Ensures UI update when weapon changed
            requestUIUpdate();
        }

        //IEnumerator updateIsReloadingValue()
        //{
        //    yield return new WaitForSeconds(reloadTime);
        //    gunManagerMasterScript.isReloading = false;
        //}
	}

}
