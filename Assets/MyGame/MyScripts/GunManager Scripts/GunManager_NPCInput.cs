using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter2;
using Chapter9;


namespace Chapter6
{
	public class GunManager_NPCInput : MonoBehaviour 
	{

        private GunManager_Master gunManagerMasterScript;
        private Transform myTransform;
        private RaycastHit hit;
        public LayerMask layerToDamage;
		
		void OnEnable()
		{
            initiate();
            gunManagerMasterScript.NPCInputEvent += npcFireGun;
		}

		void OnDisable()
		{
            gunManagerMasterScript.NPCInputEvent -= npcFireGun;
		}
	
		void initiate()
		{
            gunManagerMasterScript = GetComponent<GunManager_Master>();
            myTransform = transform;
		}

        void npcFireGun(float spread)
        {
            Vector3 startPos = new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), 0.5f);

            if (Physics.Raycast(myTransform.TransformPoint(startPos), 
                myTransform.forward, out hit, GetComponent<GunManager_Shoot>().gunRange, layerToDamage))
            {
                if (hit.transform.GetComponent<NPCManager_TakeDamage>() != null ||
                    hit.transform == GameManager_References._player.transform)
                {
                    gunManagerMasterScript.callEnemyShotEvent(hit, hit.transform);
                }
                else
                {
                    gunManagerMasterScript.callDefaultShotEvent(hit, hit.transform);
                }
            }
        }
	}

}
