using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter9
{
	public class NPCManager_StruckState : NPCManager_InterfaceState 
	{

        private readonly NPCManager_StatePattern npc;
        private Collider[] colliders;
        private Collider[] friendlyColliders;

        public void toMeleeAttackState() {}
        public void toPatrolState() {}
        public void toPursueState() {}
        public void toRangeAttackState() {}

        public NPCManager_StruckState(NPCManager_StatePattern pattern)
        {
            npc = pattern;
        }

        public void toAlertState()
        {
            npc.currentState = npc.alertState;
        }

        public void updateState()
        {
            informNearbyAllies();
        }

        void informNearbyAllies()
        {
            checkForPossibleAttacker();

            if (npc.myAttacker != null)
            {
                friendlyColliders = Physics.OverlapSphere(npc.transform.position,
                    (npc.nearAlertRange + npc.sightRange) / 2, npc.myFriendlyLayers);

                //Debug.Log("Inform");

                if (isAttackerClose())
                {
                    alertNearbyAllies();
                    setSelfStateToInvestigate();
                }
            }
                        
        }

        bool isAttackerClose()
        {
            if (Vector3.Distance(npc.transform.position, npc.myAttacker.position) <= npc.sightRange * 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void alertNearbyAllies()
        {
            foreach (Collider ally in friendlyColliders)
            {
                if (ally.transform.root.GetComponent<NPCManager_StatePattern>() != null)
                {
                    NPCManager_StatePattern npcPattern = ally.transform.root.GetComponent<NPCManager_StatePattern>();

                    if (npcPattern.currentState == npcPattern.patrolState)
                    {
                        npcPattern.pursueTarget = npc.myAttacker;
                        npcPattern.locationOfInterest = npc.myAttacker.position;
                        npcPattern.currentState = npcPattern.investigateState;
                        npcPattern.npcManagerMasterScript.callNPCWalkAnimEvent();
                    }
                }
            }
        }

        void setSelfStateToInvestigate()
        {
            npc.pursueTarget = npc.myAttacker;
            npc.locationOfInterest = npc.myAttacker.position;

            if (npc.capturedState == npc.patrolState)
            {
                npc.capturedState = npc.investigateState;
            }
        }

        void checkForPossibleAttacker()
        {
            Collider[] target;
            target = Physics.OverlapSphere(npc.transform.position, npc.sightRange * 2, npc.myEnemyLayers);

            foreach (Collider col in target)
            {
                npc.myAttacker = col.transform;
            }
        }

    }

}
