using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter6
{
	public class GunManager_Master : MonoBehaviour 
	{
        public delegate void GunGeneralEventHandler();
        public event GunGeneralEventHandler playerInputEvent;
        public event GunGeneralEventHandler gunNotUsableEvent;
        public event GunGeneralEventHandler requestReloadEvent;
        public event GunGeneralEventHandler requestGunResetEvent;
        public event GunGeneralEventHandler toggleFireModeEvent;

        public delegate void GunHitEventHandler(RaycastHit hitPosition, Transform hitTransform);
        public event GunHitEventHandler defaultShotEvent;
        public event GunHitEventHandler enemyShotEvent;

        public delegate void GunAmmoEventHandler(int currentAmmo, int maxAmmo);
        public event GunAmmoEventHandler ammoChangedEvent;

        public delegate void GunCrosshairEventHandler(float speed);
        public event GunCrosshairEventHandler speedCaptureEvent;

        public delegate void GunNPCEventHandler(float random);
        public event GunNPCEventHandler NPCInputEvent;

        public bool isGunLoaded;
        public bool isReloading;

        public void callPlayerInputEvent()
        {
            if (playerInputEvent != null)
            {
                playerInputEvent();
            }
        }

        public void callGunNotUsableEvent()
        {
            if (gunNotUsableEvent!= null)
            {
                gunNotUsableEvent();
            }
        }

        public void callRequestReloadEvent()
        {
            if (requestReloadEvent!= null)
            {
                requestReloadEvent();
            }
        }

        public void callRequestGunResetEvent()
        {
            if (requestGunResetEvent!= null)
            {
                requestGunResetEvent();
            }
        }

        public void callToggleFireModeEvent()
        {
            if (toggleFireModeEvent!= null)
            {
                toggleFireModeEvent();
            }
        }

        public void callDefaultShotEvent(RaycastHit hPos, Transform hTrans)
        {
            if (defaultShotEvent!= null)
            {
                defaultShotEvent(hPos, hTrans);
            }
        }

        public void callEnemyShotEvent(RaycastHit hPos, Transform hTrans)
        {
            if (enemyShotEvent!= null)
            {
                enemyShotEvent(hPos, hTrans);
            }
        }

        public void callAmmoChangedEvent(int cAmmo, int mAmmo)
        {
            if (ammoChangedEvent!= null)
            {
                ammoChangedEvent(cAmmo, mAmmo);
            }
        }

        public void callSpeedCaptureEvent(float speed)
        {
            if (speedCaptureEvent!= null)
            {
                speedCaptureEvent(speed);
            }
        }

        public void callNPCInputEvent(float rnd)
        {
            if (NPCInputEvent != null)
            {
                NPCInputEvent(rnd);
            }
        }

    }

}
