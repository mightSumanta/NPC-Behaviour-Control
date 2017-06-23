using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter7
{
	public class DestructibleManager_TakeDamage : MonoBehaviour 
	{

        private DestructibleManager_Master destructibleManagerMasterScript;

		void Start () 
		{
            initiate();
		}
	
		void initiate()
		{
            destructibleManagerMasterScript = GetComponent<DestructibleManager_Master>();
		}

        public void damageProcess(int damage)
        {
            destructibleManagerMasterScript.callDeductHealthEvent(damage);
        }
	}

}
