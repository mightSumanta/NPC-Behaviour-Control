using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter5
{
	public class EnemyManager_Master : MonoBehaviour 
	{

        public bool isOnRoute;
        public bool isNavPaused;

        public Transform enemyTarget;

        public delegate void EnemyGeneralEventHandler();
        public event EnemyGeneralEventHandler EnemyDieEvent;
        public event EnemyGeneralEventHandler EnemyWalkingEvent;
        public event EnemyGeneralEventHandler EnemyReachedTargetEvent;
        public event EnemyGeneralEventHandler EnemyAttackEvent;
        public event EnemyGeneralEventHandler EnemyLostTargetEvent;
        public event EnemyGeneralEventHandler EnemyHealthLowEvent;
        public event EnemyGeneralEventHandler EnemyHealthRecoveredEvent;

        public delegate void EnemyHealthEventHandler(int health);
        public event EnemyHealthEventHandler EnemyLosesHealthEvent;
        public event EnemyHealthEventHandler EnemyIncreaseHealthEvent;

        public delegate void EnemyNavigationEventHandler(Transform targetTransform);
        public event EnemyNavigationEventHandler EnemyNavToTargetEvent;

        public void callEnemyDieEvent()
        {
            if (EnemyDieEvent != null)
                EnemyDieEvent();
        }

        public void callEnemyWalkingEvent()
        {
            if (EnemyWalkingEvent != null)
                EnemyWalkingEvent();
        }

        public void callEnemyReachedTargeEvent()
        {
            if (EnemyReachedTargetEvent != null)
                EnemyReachedTargetEvent();
        }

        public void callEnemyAttackEvent()
        {
            if (EnemyAttackEvent != null)
                EnemyAttackEvent();
        }

        public void callEnemyLostTargetEvent()
        {
            if (EnemyLostTargetEvent != null)
                EnemyLostTargetEvent();
            enemyTarget = null;
        }

        public void callEnemyHealthLowEvent()
        {
            if (EnemyHealthLowEvent != null)
                EnemyHealthLowEvent();
            enemyTarget = null;
        }

        public void callEnemyHealthRecoveredEvent()
        {
            if (EnemyHealthRecoveredEvent != null)
                EnemyHealthRecoveredEvent();
            enemyTarget = null;
        }

        public void callEnemyIncreaseHealthEvent(int health)
        {
            if (EnemyIncreaseHealthEvent != null)
                EnemyIncreaseHealthEvent(health);
        }

        public void callEnemyLosesHealthEvent(int health)
        {
            if (EnemyLosesHealthEvent != null)
                EnemyLosesHealthEvent(health);
        }

        public void callEnemyNavToTargetEvent(Transform targetTransform)
        {
            if (EnemyNavToTargetEvent != null)
                EnemyNavToTargetEvent(targetTransform);
            enemyTarget = targetTransform;
        }
    }

}
