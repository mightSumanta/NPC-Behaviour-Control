using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Chapter9
{
	public class NPCManager_FleeState : NPCManager_InterfaceState 
	{

        private Vector3 directionToEnemy;
        private NavMeshHit navHit;
        private readonly NPCManager_StatePattern npc;

        public void toAlertState() {}
        public void toPursueState() {}
        public void toRangeAttackState() {}

        public NPCManager_FleeState(NPCManager_StatePattern pattern)
        {
            npc = pattern;
        }

        public void toMeleeAttackState()
        {
            keepWalking();
            npc.currentState = npc.meleeAttackState;
        }

        public void toPatrolState()
        {
            keepWalking();
            npc.currentState = npc.patrolState;
        }

        public void updateState()
        {
            checkForFlee();
            checkForFight();
        }

        void keepWalking()
        {
            npc.myNavMeshAgent.Resume();
            npc.npcManagerMasterScript.callNPCWalkAnimEvent();
        }

        void stopWalking()
        {
            npc.myNavMeshAgent.Stop();
            npc.npcManagerMasterScript.callNPCIdleAnimEvent();
        }

        void checkForFlee()
        {
            npc.meshRendererFlag.material.color = Color.white;

            Collider[] colliders = Physics.OverlapSphere(npc.transform.position, npc.sightRange, npc.myEnemyLayers);

            if (colliders.Length == 0)
            {
                toPatrolState();
                return;
            }

            directionToEnemy = npc.transform.position - colliders[0].transform.position;
            Vector3 checkPos = npc.transform.position + directionToEnemy;

            if (NavMesh.SamplePosition(checkPos, out navHit, 3f, NavMesh.AllAreas))
            {
                npc.myNavMeshAgent.SetDestination(navHit.position);
                keepWalking();
            }
            else
            {
                stopWalking();
            }
        }

        //if enemy pursues this transform while fleeing and get close enough then this event happens
        void checkForFight()                                        
        {
            if (npc.pursueTarget == null)
            {
                return;
            }

            float distanceToTarget = Vector3.Distance(npc.transform.position, npc.pursueTarget.position);

            if (npc.hasMeleeAttack && distanceToTarget <= npc.meleeAttackRange)
            {
                toMeleeAttackState();
            }
        }
    }

}
