using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter2;


namespace Chapter6
{
	public class GunManager_DynamicCrosshair : MonoBehaviour 
	{

        private GunManager_Master gunManagerMasterScript;
        public Transform canvasDynamicCrosshair;
        private Transform playerTransform;
        private Transform weaponCamera;
        private float playerSpeed;
        private float nextCapture;
        private float captureRate = 0.5f;
        private Vector3 lastPosition;
        public Animator crosshairAnimator;
        public string weaponCameraName;
		
		void Start () 
		{
            initiate();
		}
	
		void Update () 
		{
            capturePlayerSpeed();
            applySpeedToAnimation();
		}

		void initiate()
		{
            gunManagerMasterScript = GetComponent<GunManager_Master>();
            playerTransform = GameManager_References._player.transform;
            findWeaponCamera(playerTransform);
            setCameraOnDynamicCrosshairCanvas();
            setPlaneDistanceOnDynamicCrosshairCanvas();
		}

        void capturePlayerSpeed()
        {
            if (Time.time > nextCapture)    
            {
                nextCapture = Time.time + captureRate;
                playerSpeed = (playerTransform.position - lastPosition).magnitude / captureRate;
                lastPosition = playerTransform.position;
                gunManagerMasterScript.callSpeedCaptureEvent(playerSpeed);
            }
        }

        void applySpeedToAnimation()
        {
            if (crosshairAnimator != null)
                crosshairAnimator.SetFloat("Speed", playerSpeed);
        }

        void findWeaponCamera(Transform transformToSearchThrough)
        {
            if (transformToSearchThrough != null)
            {
                if (transformToSearchThrough.name == weaponCameraName)
                {
                    weaponCamera = transformToSearchThrough;
                    return;
                }

                foreach (Transform child in transformToSearchThrough)
                {
                    findWeaponCamera(child);
                }
            }
        }

        void setCameraOnDynamicCrosshairCanvas()
        {
            if (canvasDynamicCrosshair != null && weaponCamera != null)
            {
                canvasDynamicCrosshair.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
                canvasDynamicCrosshair.GetComponent<Canvas>().worldCamera = weaponCamera.GetComponent<Camera>();
            }
        }

        void setPlaneDistanceOnDynamicCrosshairCanvas()
        {
            if (canvasDynamicCrosshair != null)
            {
                canvasDynamicCrosshair.GetComponent<Canvas>().planeDistance = 1; 
            }
        }
	}

}
