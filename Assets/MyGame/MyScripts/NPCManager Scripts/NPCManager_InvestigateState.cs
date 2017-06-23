using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter9
{
	public class NPCManager_InvestigateState : NPCManager_InterfaceState 
	{
        private readonly NPCManager_StatePattern npc;
        //private float offset = 0.3f;
        private RaycastHit hit;
        private Vector3 lookAtTarget;

        public void toMeleeAttackState() {}
        public void toRangeAttackState() {}

        public NPCManager_InvestigateState(NPCManager_StatePattern pattern)
        {
            npc = pattern;
        }

        public void toAlertState()
        {
            npc.currentState = npc.alertState;
        }

        public void toPatrolState()
        {
            npc.currentState = npc.patrolState;
        }

        public void toPursueState()
        {
            npc.currentState = npc.pursueState;
        }

        public void updateState()
        {
            look();
        }

        void look()
        {
            if (npc.pursueTarget == null)
            {
                toPatrolState();
                return;
            }

            checkTargetInSight();
        }

        void checkTargetInSight()
        {
            lookAtTarget = new Vector3(npc.pursueTarget.position.x,
                npc.pursueTarget.position.y + npc.offset, npc.pursueTarget.position.z);

            if (Physics.Linecast(npc.head.position, lookAtTarget, out hit))
            {
                if (hit.transform.root == npc.pursueTarget)
                {
                    npc.locationOfInterest = npc.pursueTarget.position;
                    gotoLocationOfInterest();

                    if (Vector3.Distance(npc.transform.position, lookAtTarget) <= npc.sightRange)
                    {
                        toPursueState();
                    }
                }
                else
                {
                    toAlertState();
                }
            }
            else
            {
                toAlertState();
            }
        }

        void gotoLocationOfInterest()
        {
            npc.meshRendererFlag.material.color = Color.cyan;

            if (npc.myNavMeshAgent.enabled && npc.locationOfInterest != Vector3.zero)
            {
                npc.myNavMeshAgent.SetDestination(npc.locationOfInterest);
                npc.myNavMeshAgent.Resume();
                npc.npcManagerMasterScript.callNPCWalkAnimEvent();

                if (npc.myNavMeshAgent.remainingDistance <= npc.myNavMeshAgent.stoppingDistance)
                {
                    npc.locationOfInterest = Vector3.zero;
                    toPatrolState();
                }
            }
            else
            {
                toPatrolState();
            }
        }

    }

}
