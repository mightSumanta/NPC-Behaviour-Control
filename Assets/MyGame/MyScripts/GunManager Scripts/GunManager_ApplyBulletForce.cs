using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter6
{
	public class GunManager_ApplyBulletForce : MonoBehaviour 
	{

        private GunManager_Master gunManagerMasterScript;
        private Transform myTransform;
        public float forceToApply = 300;
		
		void OnEnable()
		{
            initiate();
            gunManagerMasterScript.defaultShotEvent += applyForce;
		}

		void OnDisable()
		{
            gunManagerMasterScript.defaultShotEvent -= applyForce;
        }
	
		void initiate()
		{
            gunManagerMasterScript = GetComponent<GunManager_Master>();
            myTransform = transform;
		}

        void applyForce(RaycastHit hitPos, Transform hitTransform)
        {
            if (hitTransform.GetComponent<Rigidbody>() != null)
            {
                hitTransform.GetComponent<Rigidbody>().AddForce(-myTransform.forward * forceToApply, ForceMode.Impulse);
            }
        }
	}

}
