using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Chapter9
{
    public class NPCManager_PatrolState : NPCManager_InterfaceState
    {

        private readonly NPCManager_StatePattern npc;
        private int nextWaypoint = 0;
        private Collider[] colliders;
        private Vector3 lookAtPoint;
        private Vector3 heading;
        private float dotProduct;

        public void toMeleeAttackState() {}
        public void toPatrolState() {}
        public void toPursueState() {}
        public void toRangeAttackState() {}

        public NPCManager_PatrolState(NPCManager_StatePattern npcStatePat)
        {
            npc = npcStatePat;
        }

        public void toAlertState()
        {
            npc.currentState = npc.alertState;
        }

        public void updateState()
        {
            look();
            patrol();
        }

        void look()
        {
            //check medium range
            colliders = Physics.OverlapSphere(npc.transform.position,
               npc.nearAlertRange, npc.myEnemyLayers);

            if (colliders.Length > 0)
            {
                visibilityCalculations(colliders[0].transform);
                if (dotProduct > 0)
                {
                    toPursueState();
                    return;
                }
                alertStateActions(colliders[0].transform);
                return;
            }

            //check max range
            colliders = Physics.OverlapSphere(npc.transform.position,
                npc.sightRange, npc.myEnemyLayers);

            foreach(Collider col in colliders)
            {
                RaycastHit hit;

                visibilityCalculations(col.transform);

                if (Physics.Linecast(npc.head.position, lookAtPoint,
                    out hit, npc.sightLayers))
                {
                    foreach(string tag in npc.myEnemyTags)
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

        void patrol()
        {
            npc.meshRendererFlag.material.color = Color.green;

            if (npc.myFollowtarget != null)
            {
                npc.currentState = npc.followState;
            }

            if (!npc.myNavMeshAgent.enabled)
            {
                return;
            }

            if (npc.waypoints.Length > 0)
            {
                moveTo(npc.waypoints[nextWaypoint].position);

                if (haveReachedDestination())
                {
                    nextWaypoint = (nextWaypoint + 1) % npc.waypoints.Length;
                }
            }

            else
            {
                if (haveReachedDestination())
                {
                    stopWalking();

                    if (randomWanderTarget(npc.transform.position, 
                        npc.sightRange, out npc.wanderLocationOfTarget))
                    {
                        moveTo(npc.wanderLocationOfTarget);
                    }
                }
            }
        }

        void alertStateActions(Transform target)
        {
            npc.locationOfInterest = target.position; //for check state
            toAlertState();
        }

        void visibilityCalculations(Transform target)
        {
            lookAtPoint = new Vector3(target.position.x, target.position.y + npc.offset, target.position.z);
            heading = lookAtPoint - npc.transform.position;
            dotProduct = Vector3.Dot(heading, npc.transform.forward);
        }

        bool randomWanderTarget(Vector3 centre, float range, out Vector3 result)
        {
            NavMeshHit navHit;

            Vector3 randomPoint = centre + Random.insideUnitSphere * npc.sightRange;
            //randomPoint.y = 0;
            if (NavMesh.SamplePosition(randomPoint, out navHit, 3f, NavMesh.AllAreas))
            {
                result = navHit.position;
                return true;
            }

            else
            {
                result = centre;
                return false;
            }   
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

        void moveTo(Vector3 targetPos)
        {
            if (Vector3.Distance(npc.transform.position, targetPos) >
                npc.myNavMeshAgent.stoppingDistance + 1)
            {
                npc.myNavMeshAgent.SetDestination(targetPos);
                keepWalking();
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
