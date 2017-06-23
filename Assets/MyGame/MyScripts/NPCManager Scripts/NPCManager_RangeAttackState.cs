using Chapter6;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter9
{
	public class NPCManager_RangeAttackState : NPCManager_InterfaceState 
	{
        private readonly NPCManager_StatePattern npc;
        private RaycastHit hitTarget;

        public void toRangeAttackState() {}

        public NPCManager_RangeAttackState(NPCManager_StatePattern pattern)
        {
            npc = pattern;
        }

        public void toAlertState()
        {
            npc.currentState = npc.alertState;
        }

        public void toMeleeAttackState()
        {
            npc.currentState = npc.meleeAttackState;
        }

        public void toPatrolState()
        {
            keepWalking();
            npc.pursueTarget = null;
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

        void look()
        {
            if (npc.pursueTarget == null)
            {
                toPatrolState();
                return;
            }

            Collider[] colliders = Physics.OverlapSphere(npc.transform.position, npc.sightRange, npc.myEnemyLayers);

            if (colliders.Length == 0)
            {
                toPatrolState();
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

            toPatrolState();
        }

        void tryToAttack()
        {
            if (npc.pursueTarget != null)
            {
                npc.meshRendererFlag.material.color = Color.magenta;

                if (!isTargetInSight())
                {
                    toPursueState();
                    return;
                }

                if (Time.time >= npc.nextAttack)
                {
                    npc.nextAttack = Time.time + npc.attackRate;

                    float distanceToTarget = Vector3.Distance(npc.transform.position, npc.pursueTarget.position);

                    //Debug.Log(distanceToTarget);
                    Vector3 newPos = new Vector3(npc.pursueTarget.position.x, npc.pursueTarget.position.y, npc.pursueTarget.position.z);
                    npc.transform.LookAt(newPos);
                    if (distanceToTarget <= npc.rangeAttackRange)
                    {
                        stopWalking();
                        if (npc.rangeWeapon.GetComponent<GunManager_Master>() != null)
                        {
                            npc.rangeWeapon.GetComponent<GunManager_Master>().callNPCInputEvent(npc.rangeAttackSpread);
                            return;
                        }
                    }

                    if (distanceToTarget <= npc.meleeAttackRange && npc.hasMeleeAttack)
                    {
                        toMeleeAttackState();
                    }
                }
            }
            else
            {
                toPatrolState();
            }
        }

        void keepWalking()
        {
            if (npc.myNavMeshAgent.enabled)
            {
                npc.myNavMeshAgent.Resume();
                npc.npcManagerMasterScript.callNPCWalkAnimEvent();
            }
        }

        void stopWalking()
        {
            if (npc.myNavMeshAgent.enabled)
            {
                npc.myNavMeshAgent.Stop();
                npc.npcManagerMasterScript.callNPCIdleAnimEvent();
            }
        }

        bool isTargetInSight()
        {
            RaycastHit hit;

            Vector3 weaponLookAtVector = new Vector3(npc.pursueTarget.position.x,
                npc.pursueTarget.position.y + npc.offset, npc.pursueTarget.position.z);
            npc.rangeWeapon.transform.LookAt(weaponLookAtVector);

            if (Physics.Raycast(npc.rangeWeapon.transform.position, npc.rangeWeapon.transform.forward, out hit))
            {
                foreach (string tag in npc.myEnemyTags)
                {
                    //Debug.Log(hit.transform.root);
                    if (hit.transform.CompareTag(tag))
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }

    }

}
