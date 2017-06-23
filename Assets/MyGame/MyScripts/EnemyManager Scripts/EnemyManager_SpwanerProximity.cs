using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter2;


namespace Chapter5
{
	public class EnemyManager_SpwanerProximity : MonoBehaviour 
	{

        public GameObject objectToSpawn;
        public int numberToSpawn;
        //public float proximity;
        //private float nextCheck;
        //private float checkRate;
        private Transform myTransform;
        //private Transform playerTransform;
        private Vector3 spawnArea;

        void Start () 
		{
            initiate();
            spawnObjects();
            Destroy(this);
            Destroy(gameObject);
        }
	
		//void Update () 
		//{
  //          checkDistance();
		//}

		void initiate()
		{
            myTransform = transform;
            ////playerTransform = GameManager_References._player.transform;
            //checkRate = Random.Range(0.8f, 1.2f);
        }

        //void OnTriggerEnter(Collider other)
        //{
        //    if (other.transform == playerTransform)
        //    {
                //spawnObjects();
                //Destroy(this);
                //Destroy(gameObject);
        //    }
        //}

        //void checkDistance()
        //{

        //}

        void spawnObjects()
        {
            for (int i = 0; i < numberToSpawn; i++)
            {
                spawnArea = myTransform.position + Random.insideUnitSphere * 5;
                Instantiate(objectToSpawn, spawnArea, myTransform.rotation);
            }
        }

        IEnumerator delay()
        {
            yield return new WaitForSeconds(0.2f);
        }
    }

}
