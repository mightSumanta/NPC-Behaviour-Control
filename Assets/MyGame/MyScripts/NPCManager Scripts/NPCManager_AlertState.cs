using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter9
{
    public class NPCManager_AlertState : NPCManager_InterfaceState
    {
        private readonly NPCManager_StatePattern npc;
        private float informRate = 3;
        private float nextInform;
        private float offset = 0.3f;
        private Vector3 targetPosition;
        private RaycastHit hit;
        private Collider[] colliders;
        private Collider[] friendlyCollider;
        private Vector3 lookAtTarget;
        private int detectionCount;
        //private int lastDetectionCount;
        private Transform possibleTarget;

        public void toAlertState() {}
        public void toMeleeAttackState() {}
        public void toRangeAttackState() {}

        public NPCManager_AlertState(NPCManager_StatePattern npcStatePattern)
        {
            npc = npcStatePattern;
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
            //check medium range
            colliders = Physics.OverlapSphere(npc.transform.position,
               npc.nearAlertRange, npc.myEnemyLayers);

            //lastDetectionCount = detectionCount;

            foreach (Collider col in colliders)
            {
                detectionCount++;
                possibleTarget = col.transform;
                //if (npc.transform.CompareTag("Enemy"))
                //{
                //    Debug.Log(detectionCount);
                //}
            }

            colliders = Physics.OverlapSphere(npc.transform.position, npc.sightRange, npc.myEnemyLayers);

            //lastDetectionCount = detectionCount;

            foreach(Collider col in colliders)
            {
                lookAtTarget = new Vector3(col.transform.position.x, col.transform.position.y + offset, col.transform.position.z);

                if (Physics.Linecast(npc.head.position, lookAtTarget, out hit, npc.sightLayers))
                {
                    foreach(string tags in npc.myEnemyTags)
                    {
                        if (hit.transform.CompareTag(tags))
                        {
                            detectionCount++;
                            possibleTarget = col.transform;
                            //Debug.Log(detectionCount.ToString());
                            break;
                        }
                    }
                }
            }

            //Check if detection count has changed else set it to 0
            //if (detectionCount == lastDetectionCount)
            //{
            //    detectionCount = 0;
            //}

            //to pursue
            if (detectionCount >= npc.requiredDetectionCount)
            {
                detectionCount = 0;
                npc.locationOfInterest = possibleTarget.position;
                npc.pursueTarget = possibleTarget.root;
                informNearbyAllies();
                toPursueState();
            }

            gotoLocationOfInterest();
        }

        void gotoLocationOfInterest()
        {
            npc.meshRendererFlag.material.color = Color.yellow;

            if (npc.myNavMeshAgent.enabled && npc.locationOfInterest != Vector3.zero)
            {
                npc.myNavMeshAgent.SetDestination(npc.locationOfInterest);
                npc.myNavMeshAgent.Resume();
                npc.npcManagerMasterScript.callNPCWalkAnimEvent();

                if (npc.myNavMeshAgent.remainingDistance <= npc.myNavMeshAgent.stoppingDistance &&
                    !npc.myNavMeshAgent.pathPending)
                {
                    npc.npcManagerMasterScript.callNPCIdleAnimEvent();
                    npc.locationOfInterest = Vector3.zero;
                    toPatrolState();
                }
            }
        }

        void informNearbyAllies()
        {
            if (Time.time > nextInform)
            {
                nextInform = Time.time + informRate;

                friendlyCollider = Physics.OverlapSphere(npc.transform.position, npc.sightRange, npc.myFriendlyLayers);

                if (friendlyCollider.Length == 0) 
                {
                    return;
                }

                foreach (Collider ally in friendlyCollider)
                {
                    if (ally.transform.root.GetComponent<NPCManager_StatePattern>() != null)
                    {
                        NPCManager_StatePattern allyNPC = ally.transform.root.GetComponent<NPCManager_StatePattern>();

                        if (allyNPC.currentState == allyNPC.patrolState)
                        {
                            allyNPC.pursueTarget = npc.pursueTarget;
                            allyNPC.locationOfInterest = npc.pursueTarget.position;
                            allyNPC.currentState = allyNPC.alertState;
                            allyNPC.npcManagerMasterScript.callNPCWalkAnimEvent();
                        }
                    }
                }
            }
        }
    }

}
