using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter8
{
	public class MeleeManager_Master : MonoBehaviour 
	{
        public bool isInUse = false;
        public float swingRate = 0.7f;

        public delegate void MeleeGeneralEventHandler();
        public event MeleeGeneralEventHandler playerInputEvent;
        public event MeleeGeneralEventHandler meleeResetEvent;

        public delegate void MeleeHitEventHandler(Collision hitCollider, Transform hitTransform);
        public event MeleeHitEventHandler hitEnemyEvent;

		public void callPlayerInputEvent()
        {
            if (playerInputEvent != null)
                playerInputEvent();
        }

        public void callMeleeResetEvent()
        {
            if (meleeResetEvent != null)
                meleeResetEvent();
        }

        public void callHitEnemyEvent(Collision hitCol, Transform hitTrans)
        {
            if (hitEnemyEvent != null)
                hitEnemyEvent(hitCol, hitTrans);
        }

    }

}
