using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Chapter9
{
	public class NPCManager_TurnOffComponents : MonoBehaviour 
	{
        private NPCManager_Master npcManagerMasterScript;
        private Animator myAnimator;
        private NavMeshAgent myNavMeshAgent;
        private NPCManager_StatePattern myPattern;

		void OnEnable()
		{
            initiate();
            npcManagerMasterScript.NPCDieEvent += turnOffAnimator;
            npcManagerMasterScript.NPCDieEvent += turnOffNavMeshAgent;
            npcManagerMasterScript.NPCDieEvent += turnOffStatePattern;
            //npcManagerMasterScript.NPCDieEvent += turnOffTagLayer;
        }

		void OnDisable()
		{
            npcManagerMasterScript.NPCDieEvent -= turnOffAnimator;
            npcManagerMasterScript.NPCDieEvent -= turnOffNavMeshAgent;
            npcManagerMasterScript.NPCDieEvent -= turnOffStatePattern;
            //npcManagerMasterScript.NPCDieEvent -= turnOffTagLayer;
        }
	
		void initiate()
		{
            npcManagerMasterScript = GetComponent<NPCManager_Master>();

            if (GetComponent<Animator>() != null)
            {
                myAnimator = GetComponent<Animator>();
            }

            if (GetComponent<NavMeshAgent>() != null)
            {
                myNavMeshAgent = GetComponent<NavMeshAgent>();
            }

            if (GetComponent<NPCManager_StatePattern>() != null)
            {
                myPattern = GetComponent<NPCManager_StatePattern>();
            }
		}

        void turnOffAnimator()
        {
            if (myAnimator != null)
            {
                myAnimator.enabled = false;
            }
        }

        void turnOffNavMeshAgent()
        {
            if (myNavMeshAgent != null)
            {
                myNavMeshAgent.enabled = false;
            }
        }

        void turnOffStatePattern()
        {
            if (myPattern != null)
            {
                myPattern.enabled = false;
            }
        }

        void turnOffTagLayer()
        {
            gameObject.tag = "Untagged";
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
	}

}
