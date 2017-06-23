using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter2;


namespace Chapter8
{
	public class MeleeManager_Strike : MonoBehaviour 
	{
        private MeleeManager_Master meleeManagerManagerScript;
        private float nexSwing;
        public float damage = 25;

		void Start () 
		{
            initiate();
		}
	
		void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject != GameManager_References._player && meleeManagerManagerScript.isInUse &&
                Time.time > nexSwing && Time.timeScale > 0)
            {
                nexSwing = Time.time + meleeManagerManagerScript.swingRate;
                collision.transform.SendMessage("damageProcess", damage, SendMessageOptions.DontRequireReceiver);
                meleeManagerManagerScript.callHitEnemyEvent(collision, collision.transform);
            }
        }

        void initiate()
		{
            meleeManagerManagerScript = GetComponent<MeleeManager_Master>();
		}
	}

}
