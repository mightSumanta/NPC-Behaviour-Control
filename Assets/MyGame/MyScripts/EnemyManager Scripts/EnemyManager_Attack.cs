using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter2;
using Chapter3;


namespace Chapter5
{
	public class EnemyManager_Attack : MonoBehaviour 
	{

        private EnemyManager_Master enemyManagerMasterScript;
        private Transform targetToAttack;
        private Transform myTransform;
        public float attackRate = 1;
        private float nextAttack;
        public float attackRange;
        public int attackDamage = 10;

		
		void OnEnable()
		{
            initiate();
            enemyManagerMasterScript.EnemyDieEvent += disableThisScript;
            enemyManagerMasterScript.EnemyNavToTargetEvent += setAttackTarget;
		}

		void OnDisable()
		{
            enemyManagerMasterScript.EnemyDieEvent -= disableThisScript;
            enemyManagerMasterScript.EnemyNavToTargetEvent -= setAttackTarget;
        }

        void Update () 
		{
            tryToAttack();
		}

		void initiate()
		{
            enemyManagerMasterScript = GetComponent<EnemyManager_Master>();
            myTransform = transform;
		}

        void setAttackTarget(Transform target)
        {
            targetToAttack = target;
        }

        void tryToAttack()
        {
            if (targetToAttack != null)
            {
                if (Time.time > nextAttack)
                {
                    nextAttack = Time.time + attackRate;
                    if (Vector3.Distance(myTransform.position, targetToAttack.position) <= attackRange)
                    {
                        Vector3 faceEnemy = new Vector3(targetToAttack.position.x, myTransform.position.y, targetToAttack.position.z);
                        myTransform.LookAt(faceEnemy);
                        Vector3 toOther = targetToAttack.position - myTransform.position;
                        if (Vector3.Dot(toOther, myTransform.forward) > 0.5f)
                        {
                            enemyManagerMasterScript.callEnemyAttackEvent();
                            enemyManagerMasterScript.isOnRoute = false;
                        }
                        else
                            myTransform.LookAt(faceEnemy);
                    }
                }
            }
        }

        public void onEnemyAttack() //Called by hpunch animation
        {
            if (targetToAttack != null)
            {
                if (Vector3.Distance(myTransform.position, targetToAttack.position) <= attackRange &&
                    targetToAttack.GetComponent<PlayerManager_Master>() != null)
                {
                    Vector3 toOther = targetToAttack.position - myTransform.position;

                    if (Vector3.Dot(toOther, myTransform.forward) > 0.5f)
                    {
                        targetToAttack.GetComponent<PlayerManager_Master>().callDecreasePlayerHealthEvent(attackDamage);
                    }

                }
            }
        }

        void disableThisScript()
        {
            this.enabled = false;
        }
	}

}
