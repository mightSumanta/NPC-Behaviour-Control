using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter6
{
	public class GunManager_HitEffects : MonoBehaviour 
	{

        private GunManager_Master gunManagerMasterScript;
        public GameObject defautHitEffect;
        public GameObject enemyHitEffect;

		void OnEnable()
		{
            initiate();
            gunManagerMasterScript.defaultShotEvent += spawnDefaultHitEffect;
            gunManagerMasterScript.enemyShotEvent += spawnEnemyHitEffect;
		}

		void OnDisable()
		{
            gunManagerMasterScript.defaultShotEvent -= spawnDefaultHitEffect;
            gunManagerMasterScript.enemyShotEvent -= spawnEnemyHitEffect;
        }
	
		void initiate()
		{
            gunManagerMasterScript = GetComponent<GunManager_Master>();
		}

        void spawnDefaultHitEffect(RaycastHit hitPos, Transform hitObj)
        {
            if (defautHitEffect != null)
            {
                Quaternion quatAngle = Quaternion.LookRotation(hitPos.normal);
                Instantiate(defautHitEffect, hitPos.point, quatAngle);
            }
        }

        void spawnEnemyHitEffect(RaycastHit hitPos, Transform hitObj)
        {
            if (enemyHitEffect != null)
            {
                Quaternion quatAngle = Quaternion.LookRotation(hitPos.normal);
                Instantiate(enemyHitEffect, hitPos.point, quatAngle);
            }
        }
    }

}
