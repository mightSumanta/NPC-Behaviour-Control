using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter9
{
	public class NPCManager_Master : MonoBehaviour 
	{
        public delegate void NPCGeneralEventHandler();
        public event NPCGeneralEventHandler NPCDieEvent;
        public event NPCGeneralEventHandler NPCHealthLowEvent;
        public event NPCGeneralEventHandler NPCHealthRecoveredEvent;
        public event NPCGeneralEventHandler NPCWalkAnimEvent;
        public event NPCGeneralEventHandler NPCStruckAnimEvent;
        public event NPCGeneralEventHandler NPCAttackAnimEvent;
        public event NPCGeneralEventHandler NPCRecoveredAnimEvent;
        public event NPCGeneralEventHandler NPCIdleAnimEvent;

        public delegate void NPCHealthEventHandler(int health);
        public event NPCHealthEventHandler NPCDeductHealthEvent;
        public event NPCHealthEventHandler NPCIncreaseHealthEvent;

        public string animIsPursuing = "isPursuing";
        public string animStruckTrigger = "Struck";
        public string animMeleeTrigger = "Melee";
        public string animRecoveredTrigger = "Recovered";

        public void callNPCDieEvent()
        {
            if (NPCDieEvent != null)
            {
                NPCDieEvent();
            } 
        }

        public void callNPCHealthLowEvent()
        {
            if (NPCHealthLowEvent != null)
            {
                NPCHealthLowEvent();
            }
        }

        public void callHealthRecoveredEvent()
        {
            if (NPCHealthRecoveredEvent != null)
            {
                NPCHealthRecoveredEvent();
            }
        }

        public void callNPCWalkAnimEvent()
        {
            if (NPCWalkAnimEvent != null)
            {
                NPCWalkAnimEvent();
            }
        }

        public void callNPCStruckAnimEvent()
        {
            if (NPCStruckAnimEvent != null)
            {
                NPCStruckAnimEvent();
            }
        }

        public void callNPCAttackAnimEvent()
        {
            if (NPCAttackAnimEvent != null)
            {
                NPCAttackAnimEvent();
            }
        }

        public void callNPCRecoveredAnimEvent()
        {
            if (NPCRecoveredAnimEvent != null)
            {
                NPCRecoveredAnimEvent();
            }
        }

        public void callNPCIdleAnimEvent()
        {
            if (NPCIdleAnimEvent != null)
            {
                NPCIdleAnimEvent();
            }
        }

        public void callNPCIncreaseHealthEvent(int health)
        {
            if (NPCIncreaseHealthEvent != null)
            {
                NPCIncreaseHealthEvent(health);
            }
        }

        public void callNPCDeductHealthEvent(int health)
        {
            if (NPCDeductHealthEvent != null)
            {
                NPCDeductHealthEvent(health);
            }
        }

    }

}
