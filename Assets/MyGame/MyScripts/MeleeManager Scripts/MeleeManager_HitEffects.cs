using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter5;
using Chapter9;

namespace Chapter8
{
    public class MeleeManager_HitEffects : MonoBehaviour
    {
        private MeleeManager_Master meleeManagerMasterScript;
        public GameObject defaultHitEffect;
        public GameObject enemyHitEffect;

        void OnEnable()
        {
            initiate();
            meleeManagerMasterScript.hitEnemyEvent += spawnHitEffects;
        }

        void OnDisable()
        {
            meleeManagerMasterScript.hitEnemyEvent -= spawnHitEffects;
        }

        void initiate()
        {
            meleeManagerMasterScript = GetComponent<MeleeManager_Master>();
        }

        void spawnHitEffects(Collision hitCol, Transform hitTrans)
        {
            Quaternion quatAngle = Quaternion.LookRotation(hitCol.contacts[0].normal);

            if (hitTrans.GetComponent<EnemyManager_TakeDamage>() != null)
            {
                Instantiate(enemyHitEffect, hitCol.contacts[0].point, quatAngle);
            }
            else if (hitTrans.GetComponent<NPCManager_TakeDamage>() != null)
            {
                Instantiate(enemyHitEffect, hitCol.contacts[0].point, quatAngle);
            }
            else
            {
                Instantiate(defaultHitEffect, hitCol.contacts[0].point, quatAngle);
            }
        }
	}

}
