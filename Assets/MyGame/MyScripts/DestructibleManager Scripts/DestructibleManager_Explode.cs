using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter2;
using Chapter4;
using Chapter5;

namespace Chapter7
{
    public class DestructibleManager_Explode : MonoBehaviour
    {

        private DestructibleManager_Master destructibleManagerMasterScript;
        public float explosionRange;
        public float alertNearbyEnemyRange;
        public LayerMask enemyLayerToAlert;
        public float explosionForce;
        private float distance;
        private int rawDamage;
        private int damageToApply;
        private Collider[] struckCollider;
        private Transform myTransform;
        private RaycastHit hitObjects;
        int playerDamage;
        Transform playerTransform;

        void OnEnable()
        {
            initiate();
            destructibleManagerMasterScript.destroyMeEvent += explosionSphere;
        }

        void OnDisable()
        {
            destructibleManagerMasterScript.destroyMeEvent -= explosionSphere;
        }

        void initiate()
        {
            destructibleManagerMasterScript = GetComponent<DestructibleManager_Master>();
            myTransform = transform;
            rawDamage = Random.Range(80, 100);
        }

        void explosionSphere()
        {
            int count = 0;
            myTransform.parent = null;
            GetComponent<Collider>().enabled = false;

            struckCollider = Physics.OverlapSphere(myTransform.position, explosionRange);

            foreach (Collider col in struckCollider)
            {
                distance = Vector3.Distance(myTransform.position, col.transform.position);
                damageToApply = (int)Mathf.Abs((1 - distance / explosionRange) * rawDamage);

                if (Physics.Linecast(myTransform.position, col.transform.position, out hitObjects))
                {
                    if (hitObjects.transform == col.transform || col.transform.GetComponent<EnemyManager_TakeDamage>() != null)
                    {
                        col.SendMessage("damageProcess", damageToApply, SendMessageOptions.DontRequireReceiver);
                    }
                    if (col.transform.gameObject == GameManager_References._player && hitObjects.transform == col.transform)
                    {
                        count++;
                        playerDamage = damageToApply;
                        playerTransform = col.transform;
                    }
                }

                if (col.GetComponent<Rigidbody>() != null)
                {
                    col.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, myTransform.position, explosionRange, 1, ForceMode.Impulse);
                }
            }

            Collider[] collider;
            collider = Physics.OverlapSphere(transform.position, alertNearbyEnemyRange, enemyLayerToAlert);
            foreach (Collider colli in collider)
            {
                colli.transform.root.SendMessage("distract", transform.position, SendMessageOptions.DontRequireReceiver);
            }

            if (count > 0)
            {
                playerTransform.SendMessage("callDecreasePlayerHealthEvent", playerDamage, SendMessageOptions.DontRequireReceiver);
                //Debug.Log(playerDamage);
            }

            Destroy(gameObject, 0.05f);
        }
    }
}