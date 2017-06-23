using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter7
{
	public class DestructibleManager_Master : MonoBehaviour 
	{
        public delegate void GeneralEventHandler();
        public event GeneralEventHandler destroyMeEvent;
        public event GeneralEventHandler healthLowEvent;

        public delegate void HealthEventHandler(int damage);
        public event HealthEventHandler deductHealthEvent;

        public void callDestroyMeEvent()
        {
            if (destroyMeEvent != null)
            {
                destroyMeEvent();
            }
        }

        public void callHealthLowEvent()
        {
            if (healthLowEvent != null)
            {
                healthLowEvent();
            }
        }

        public void callDeductHealthEvent(int damage)
        {
            if (deductHealthEvent != null)
            {
                deductHealthEvent(damage);
            }
        }
        
	}

}
