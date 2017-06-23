using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter5
{
	public class EnemyManager_Animation : MonoBehaviour 
	{

        private EnemyManager_Master enemyManagerMasterScript;
        private Animator enemyAnimator;

		void OnEnable()
		{
            initiate();
            enemyManagerMasterScript.EnemyDieEvent += disableAnimator;
            enemyManagerMasterScript.EnemyWalkingEvent += setAnimationToWalk;
            enemyManagerMasterScript.EnemyReachedTargetEvent += setAnimationToIdle;
            enemyManagerMasterScript.EnemyAttackEvent += setAnimationToAttack;
            enemyManagerMasterScript.EnemyLosesHealthEvent += setAnimationToStruck;
		}

		void OnDisable()
		{
            enemyManagerMasterScript.EnemyDieEvent -= disableAnimator;
            enemyManagerMasterScript.EnemyWalkingEvent -= setAnimationToWalk;
            enemyManagerMasterScript.EnemyReachedTargetEvent -= setAnimationToIdle;
            enemyManagerMasterScript.EnemyAttackEvent -= setAnimationToAttack;
            enemyManagerMasterScript.EnemyLosesHealthEvent -= setAnimationToStruck;
        }

		void initiate()
		{
            enemyManagerMasterScript = GetComponent<EnemyManager_Master>();
            if (GetComponent<Animator>() != null)
                enemyAnimator = GetComponent<Animator>();
		}

        void setAnimationToWalk()
        {
            if (enemyAnimator != null)
            {
                if (enemyAnimator.enabled)
                {
                    enemyAnimator.SetBool("isPursuing", true);
                }
            }
        }

        void setAnimationToIdle()
        {
            if (enemyAnimator != null)
            {
                if (enemyAnimator.enabled)
                {
                    enemyAnimator.SetBool("isPursuing", false);
                }
            }
        }

        void setAnimationToAttack()
        {
            if (enemyAnimator != null)
            {
                if (enemyAnimator.enabled)
                {
                    enemyAnimator.SetTrigger("Attack");
                }
            }
        }

        void setAnimationToStruck(int dummy)
        {
            if (enemyAnimator != null)
            {
                if (enemyAnimator.enabled)
                {
                    enemyAnimator.SetTrigger("Struck");
                }
            }
        } 
        
        void disableAnimator()
        {
            if (enemyAnimator != null)
                enemyAnimator.enabled = false;
        }       
    }

}
