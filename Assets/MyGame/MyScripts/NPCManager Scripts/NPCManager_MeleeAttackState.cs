using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter9
{
	public class NPCManager_MeleeAttackState : NPCManager_InterfaceState 
	{

        private readonly NPCManager_StatePattern npc;
        private float targetToDistance;

        public void toMeleeAttackState() {}
        public void toRangeAttackState() {}

        public NPCManager_MeleeAttackState(NPCManager_StatePattern pattern)
        {
            npc = pattern;
        }

        public void toAlertState()
        {
            keepWalking();
            npc.currentState = npc.alertState;
        }

        public void toPatrolState()
        {
            keepWalking();
            npc.currentState = npc.patrolState;
        }

        public void toPursueState()
        {
            keepWalking();
            npc.currentState = npc.pursueState;
        }

        public void updateState()
        {
            look();
            tryToAttack();
        }

        void keepWalking()
        {
            npc.myNavMeshAgent.Resume();
            npc.npcManagerMasterScript.callNPCWalkAnimEvent();
        }

        void look()
        {
            if (npc.pursueTarget == null)
            {
                toPatrolState();
                return;
            }

            Collider[] colliders = Physics.OverlapSphere(npc.transform.position, npc.meleeAttackRange, npc.myEnemyLayers);

            if (colliders.Length == 0)
            {
                //npc.pursueTarget = null;
                toPursueState();
                return;
            }
            else
            {
                foreach (Collider col in colliders)
                {
                    if (col.transform.root == npc.pursueTarget)
                    {
                        return;
                    }
                }
            }

            //npc.pursueTarget = null;
            toPursueState();
        }

        void tryToAttack()
        {
            if (npc.pursueTarget != null)
            {
                npc.meshRendererFlag.material.color = Color.black;

                if (Time.time > npc.nextAttack && !npc.isMeleeAttacking)
                {
                    npc.nextAttack = Time.time + npc.attackRate;

                    if (Vector3.Distance(npc.transform.position, npc.pursueTarget.position) <= npc.meleeAttackRange)
                    {
                        Vector3 newPos = new Vector3(npc.pursueTarget.position.x, npc.pursueTarget.position.y,
                            npc.pursueTarget.position.z);
                        npc.transform.LookAt(newPos);
                        npc.npcManagerMasterScript.callNPCAttackAnimEvent();
                        npc.isMeleeAttacking = true;
                    }
                    else
                    {
                        toPursueState();
                    }
                }
                
            }
            else
            {
                toPatrolState();
            }
        }

    }

}
