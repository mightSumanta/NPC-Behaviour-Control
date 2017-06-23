using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter5;

namespace Chapter6
{
	public class GunManager_ApplyDamage : MonoBehaviour 
	{

        private GunManager_Master gunManagerMasterScript;
        public int damage = 10;
		
		void OnEnable()
		{
            initiate();
            gunManagerMasterScript.enemyShotEvent += applyDamage;
            gunManagerMasterScript.defaultShotEvent += applyDamage;
		}

		void OnDisable()
		{
            gunManagerMasterScript.enemyShotEvent -= applyDamage;
            gunManagerMasterScript.defaultShotEvent -= applyDamage;
        }
	
		void initiate()
		{
            gunManagerMasterScript = GetComponent<GunManager_Master>();
		}

        void applyDamage(RaycastHit hitPos, Transform hitTrans)
        {
            hitTrans.SendMessage("damageProcess", damage, SendMessageOptions.DontRequireReceiver);
            hitTrans.SendMessage("setMyAttacker", transform.root, SendMessageOptions.DontRequireReceiver);
            hitTrans.SendMessage("callDecreasePlayerHealthEvent", damage, SendMessageOptions.DontRequireReceiver);
        }
	}

}
