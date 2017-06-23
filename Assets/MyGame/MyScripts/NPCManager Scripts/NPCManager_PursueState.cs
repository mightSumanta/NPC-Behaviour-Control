using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter9
{
    public class NPCManager_PursueState : NPCManager_InterfaceState
    {

        private readonly NPCManager_StatePattern npc;
        private float capturedDistance;
        private int count;

        public void toPursueState() {}

        public NPCManager_PursueState(NPCManager_StatePattern pattern)
        {
            npc = pattern;
        }

        public void toAlertState()
        {
            keepWalking();
            npc.currentState = npc.alertState;
        }

        public void toMeleeAttackState()
        {
            npc.currentState = npc.meleeAttackState;
        }

        public void toPatrolState()
        {
            keepWalking();
            npc.currentState = npc.patrolState;
        }

        public void toRangeAttackState()
        {
            npc.currentState = npc.rangeAttackState;
        }

        public void updateState()
        {
            look();
            pursue();
        }

        void look()
        {
            if (npc.pursueTarget == null)
            {
                toAlertState();
                return;
            }

            Collider[] colliders = Physics.OverlapSphere(npc.transform.position, npc.sightRange, npc.myEnemyLayers);

            if(colliders.Length == 0)
            {
                npc.pursueTarget = null;
                toAlertState();
                return;
            }

            capturedDistance = npc.sightRange * 2;

            foreach (Collider col in colliders)
            {
                float distanceToTarget = Vector3.Distance(npc.transform.position, col.transform.position);

                if (distanceToTarget < capturedDistance)
                {
                    capturedDistance = distanceToTarget;
                    npc.pursueTarget = col.transform.root;
                }
            }
        }

        void pursue()
        {
            count = 0;
            checkForVisibility();

            if (count > 0)
            {
                npc.meshRendererFlag.material.color = Color.red;
                if (npc.myNavMeshAgent.enabled && npc.pursueTarget != null)
                {
                    npc.myNavMeshAgent.SetDestination(npc.pursueTarget.position);
                    npc.locationOfInterest = npc.pursueTarget.position;
                    keepWalking();

                    float distanceToTarget = Vector3.Distance(npc.transform.position, npc.pursueTarget.position);

                    if (distanceToTarget <= npc.rangeAttackRange && distanceToTarget > npc.meleeAttackRange)
                    {
                        if (npc.hasRangeAttack)
                        {
                            toRangeAttackState();
                        }
                    }
                    else if (distanceToTarget <= npc.sightRange)
                    {
                        if (npc.hasMeleeAttack && distanceToTarget <= npc.meleeAttackRange)
                        {
                            toMeleeAttackState();
                        }
                        else if (npc.hasRangeAttack)
                        {
                            toRangeAttackState();
                        }
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

        void checkForVisibility()
        {
            Collider[] collider;
            RaycastHit hit;

            collider = Physics.OverlapSphere(npc.transform.position, npc.nearAlertRange, npc.myEnemyLayers);
            if (collider.Length > 0)
            {
                count++;
            }

            collider = Physics.OverlapSphere(npc.transform.position, npc.sightRange, npc.myEnemyLayers);
            if (collider.Length > 0)
            {
                if (Physics.Linecast(npc.head.position, collider[0].transform.position, out hit, npc.sightLayers))
                {
                    foreach (string tag in npc.myEnemyTags)
                    {
                        if (hit.transform.CompareTag(tag))
                        {
                            count++;
                        }
                    }
                }
            }
        }

        void keepWalking()
        {
            npc.myNavMeshAgent.Resume();
            npc.npcManagerMasterScript.callNPCWalkAnimEvent();
        }
    }

}
