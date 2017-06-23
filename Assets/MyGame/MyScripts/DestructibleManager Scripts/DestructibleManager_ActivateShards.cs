using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter7
{
	public class DestructibleManager_ActivateShards : MonoBehaviour 
	{

        private DestructibleManager_Master destructibleManagerMasterScript;
        public string shardsLayer = "Ignore Raycast";
        public GameObject shards;
        public bool shardShouldDisappear = true;
        private float myMass;

		void OnEnable()
		{
            initiate();
            destructibleManagerMasterScript.destroyMeEvent += activateShards;
		}

		void OnDisable()
		{
            destructibleManagerMasterScript.destroyMeEvent -= activateShards;
        }
	
		void initiate()
		{
            destructibleManagerMasterScript = GetComponent<DestructibleManager_Master>();

            if (GetComponent<Rigidbody>() != null)
            {
                myMass = GetComponent<Rigidbody>().mass;
            }
		}

        void activateShards()
        {
            if (shards != null)
            {
                shards.transform.parent = null;
                shards.SetActive(true);

                foreach(Transform shard in shards.transform)
                {
                    shard.tag = "Untagged";
                    shard.gameObject.layer = LayerMask.NameToLayer(shardsLayer);

                    shard.GetComponent<Rigidbody>().AddExplosionForce(myMass, transform.position, 20, 0, ForceMode.Impulse);

                    if(shardShouldDisappear)
                    {
                        Destroy(shard.gameObject, 15);
                    }
                }
            }
        }
	}

}
