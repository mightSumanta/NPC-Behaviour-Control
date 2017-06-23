using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Chapter6
{
	public class GunManager_AmmoUI : MonoBehaviour 
	{
        private GunManager_StandardInput gunStdInput;
        private GunManager_Master gunManagerMasterScript;
        public InputField currentAmmoUI;
        public InputField carriedAmmoUI;
        public Image fireModeImage;
        public Text fireModeText;


		void OnEnable()
		{
            initiate();
            updateFireModeUI();
            gunManagerMasterScript.ammoChangedEvent += updateAmmoUI;
            gunManagerMasterScript.toggleFireModeEvent += updateFireModeUI;
		}

		void OnDisable()
		{
            gunManagerMasterScript.ammoChangedEvent -= updateAmmoUI;
            gunManagerMasterScript.toggleFireModeEvent += updateFireModeUI;
        }
	
		void initiate()
		{
            gunManagerMasterScript = GetComponent<GunManager_Master>();
            gunStdInput = GetComponent<GunManager_StandardInput>();
            updateFireModeUI();
		}

        void updateAmmoUI(int currentAmmo, int carriedAmmo)
        {
            if (currentAmmoUI != null)
                currentAmmoUI.text = currentAmmo.ToString();
            if (carriedAmmoUI != null)
                carriedAmmoUI.text = carriedAmmo.ToString();
        }

        void updateFireModeUI()
        {
            if (gunStdInput.isAutomatic)
            {
                fireModeImage.transform.parent.gameObject.SetActive(true);
                if (gunStdInput.isBurstFireActiveMethod())
                    fireModeText.text = "Burst";
                else
                    fireModeText.text = "Auto";
            }
            else
            {
                fireModeImage.enabled = false;
                fireModeText.enabled = false;
            }
        }
	}

}
