using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter9;
using Chapter5;

namespace Chapter6
{
	public class GunManager_Shoot : MonoBehaviour 
	{

        private GunManager_Master gunManagerMasterScript;
        private Transform myTransform;
        private Transform camTransform;
        private RaycastHit hitTarget;
        public float gunRange;
        private float offsetFactor = 7;
        private Vector3 bulletStartPosition;

		void OnEnable()
		{
            initiate();
            gunManagerMasterScript.playerInputEvent += openFire;
            gunManagerMasterScript.speedCaptureEvent += setShootingPosition;
		}

		void OnDisable()
		{
            gunManagerMasterScript.playerInputEvent -= openFire;
            gunManagerMasterScript.speedCaptureEvent -= setShootingPosition;
        }
	
		void initiate()
		{
            gunManagerMasterScript = GetComponent<GunManager_Master>();
            myTransform = transform;
            camTransform = myTransform.parent;
		}

        void openFire()
        {
            //Debug.Log("Open Fire Called");
            if (Physics.Raycast(camTransform.TransformPoint(bulletStartPosition), camTransform.forward, out hitTarget, gunRange))
            {
                if (hitTarget.transform.GetComponent<NPCManager_TakeDamage>() != null)
                {
                    gunManagerMasterScript.callEnemyShotEvent(hitTarget, hitTarget.transform);
                }
                else if (hitTarget.transform.GetComponent<EnemyManager_TakeDamage>() != null)
                {
                    gunManagerMasterScript.callEnemyShotEvent(hitTarget, hitTarget.transform);
                }
                else
                {
                    gunManagerMasterScript.callDefaultShotEvent(hitTarget, hitTarget.transform);
                }
            }
        }

        void setShootingPosition(float playerSpeed)
        {
            float offset = playerSpeed / offsetFactor;
            bulletStartPosition = new Vector3(Random.Range(-offset, offset), Random.Range(-offset, offset), 1);
        }
	}

}
