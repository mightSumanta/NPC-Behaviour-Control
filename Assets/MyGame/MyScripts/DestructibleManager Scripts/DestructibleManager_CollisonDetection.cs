using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter7
{
	public class DestructibleManager_CollisonDetection : MonoBehaviour 
	{

        private DestructibleManager_Master destructibleManagerMasterScript;
        private Collider[] hitCollider;
        private Rigidbody myRigidbody;
        public float thresholdMass = 50;
        public float thresholdSpeed = 6;

		void Start () 
		{
            initiate();
		}
	
		void initiate()
		{
            destructibleManagerMasterScript = GetComponent<DestructibleManager_Master>();
            if (GetComponent<Rigidbody>() != null)
                myRigidbody = GetComponent<Rigidbody>();
		}

        void OnCollisionEnter(Collision collision)
        {
            if (collision.contacts.Length > 0)
            {
                if (collision.contacts[0].otherCollider.GetComponent<Rigidbody>() != null)
                {
                    collisionCheck(collision.contacts[0].otherCollider.GetComponent<Rigidbody>());
                }
                else
                    selfSpeedCheck();
            }
        }

        void collisionCheck(Rigidbody otherRigidBody)
        {
            if (otherRigidBody.mass > thresholdMass && otherRigidBody.velocity.sqrMagnitude > thresholdSpeed * thresholdSpeed)
            {
                int damage = (int)otherRigidBody.mass;
                destructibleManagerMasterScript.callDeductHealthEvent(damage);
            }

            else
                selfSpeedCheck();
        }

        void selfSpeedCheck()
        {
            if (myRigidbody.velocity.sqrMagnitude > thresholdSpeed * thresholdSpeed)
            {
                int damage = (int)myRigidbody.mass;
                destructibleManagerMasterScript.callDeductHealthEvent(damage);
            }
        }
    }

}
