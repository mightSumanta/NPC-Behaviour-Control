using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter9
{
	public class NPCManager_FollowState : NPCManager_InterfaceState 
	{

        private readonly NPCManager_StatePattern npc;
        private Collider[] colliders;
        private Vector3 lookAtPoint;
        private Vector3 heading;
        private float dotProduct;

        public void toMeleeAttackState() {}
        public void toPursueState() {}
        public void toRangeAttackState() {}

        public NPCManager_FollowState(NPCManager_StatePattern pattern)
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

        public void updateState()
        {
            look();
            follow();
        }

        void look()
        {
            //close range
            colliders = Physics.OverlapSphere(npc.transform.position, npc.nearAlertRange, npc.myEnemyLayers);

            if (colliders.Length > 0)
            {
                alertStateActions(colliders[0].transform);
                return;
            }

            ////medium range
            //colliders = Physics.OverlapSphere(npc.transform.position, (npc.sightRange + npc.nearAlertRange) / 2, npc.myEnemyLayers);

            //if (colliders.Length > 0)
            //{
            //    visibilityCalculations(colliders[0].transform);

            //    if(dotProduct > 0)
            //    {
            //        alertStateActions(colliders[0].transform);
            //        return;
            //    }
            //}

            //max range
            colliders = Physics.OverlapSphere(npc.transform.position, npc.sightRange, npc.myEnemyLayers);

            foreach (Collider col in colliders)
            {
                RaycastHit hit;

                visibilityCalculations(col.transform);

                if (Physics.Linecast(npc.head.position, lookAtPoint, out hit, npc.sightLayers))
                {
                    foreach (string tag in npc.myEnemyTags)
                    {
                        if (hit.transform.CompareTag(tag))
                        {
                            if (dotProduct > 0)
                            {
                                alertStateActions(col.transform);
                                return;
                            }
                        }
                    }
                }
            }
        }

        void follow()
        {
            npc.meshRendererFlag.material.color = Color.blue;

            if (!npc.myNavMeshAgent.enabled)
            {
                return;
            }

            if (npc.myFollowtarget != null)
            {
                npc.myNavMeshAgent.SetDestination(npc.myFollowtarget.position);
                keepWalking();
            }
            else
            {
                toPatrolState();
            }

            if (haveReachedDestination())
            {
                stopWalking();
            }
        }

        void alertStateActions(Transform target)
        {
            npc.locationOfInterest = target.position;
            toAlertState();
        }

        void visibilityCalculations(Transform target)
        {
            lookAtPoint = new Vector3(target.position.x, target.position.y + npc.offset, target.position.z);
            heading = lookAtPoint - npc.transform.position;
            dotProduct = Vector3.Dot(heading, npc.transform.forward);
        }

        bool haveReachedDestination()
        {
            if (npc.myNavMeshAgent.remainingDistance <= npc.myNavMeshAgent.stoppingDistance &&
                !npc.myNavMeshAgent.pathPending)
            {
                stopWalking();
                return true;
            }
            else
            {
                keepWalking();
                return false;
            }
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


    }

}
