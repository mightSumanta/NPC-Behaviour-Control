using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter7
{
	public class DestructibleManager_ParticleSoundSpawn : MonoBehaviour 
	{

        private DestructibleManager_Master destructibleManagerMaster;
        public GameObject explosionEffect;
        public float explosionVolume = 0.5f;
        public AudioClip explodingSound;
		
		void OnEnable()
		{
            initiate();
            destructibleManagerMaster.destroyMeEvent += spawnExplosionEffect;
            destructibleManagerMaster.destroyMeEvent += spawnSound;

        }

		void OnDisable()
		{
            destructibleManagerMaster.destroyMeEvent -= spawnExplosionEffect;
            destructibleManagerMaster.destroyMeEvent -= spawnSound;
        }
	
		void initiate()
		{
            destructibleManagerMaster = GetComponent<DestructibleManager_Master>();
		}

        void spawnExplosionEffect()
        {
            if (explosionEffect != null)
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        void spawnSound()
        {
            if (explodingSound != null)
                AudioSource.PlayClipAtPoint(explodingSound, transform.position, explosionVolume);
        }
	}

}
