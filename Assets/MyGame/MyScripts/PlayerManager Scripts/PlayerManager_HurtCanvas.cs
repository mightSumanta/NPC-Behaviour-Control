using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter3
{
	public class PlayerManager_HurtCanvas : MonoBehaviour 
	{
        public GameObject hurtCanvas;
        private PlayerManager_Master playerManagerMasterScript;
        private float showCanvasFor = 2;

		void OnEnable()
		{
            initiate();
            playerManagerMasterScript.DecreasePlayerHealthEvent += turnOnHurtEffect;
		}

		void OnDisable()
		{
            playerManagerMasterScript.DecreasePlayerHealthEvent -= turnOnHurtEffect;
		}

		void initiate()
		{
            playerManagerMasterScript =  GetComponent<PlayerManager_Master>();
		}

        void turnOnHurtEffect(int foo)
        {
            if (hurtCanvas != null)
            {
                StopAllCoroutines();
                hurtCanvas.SetActive(true);
                StartCoroutine(hideCanvasAfter());
            }
        }

        IEnumerator hideCanvasAfter()
        {
            yield return new WaitForSeconds(showCanvasFor);
            hurtCanvas.SetActive(false);
        }
	}

}
