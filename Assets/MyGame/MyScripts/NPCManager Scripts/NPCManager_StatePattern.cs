using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Chapter9
{
	public class NPCManager_StatePattern : MonoBehaviour 
	{
        //Used in decision making
        private float checkRate = 0.1f;
        private float nextCheck;
        public float sightRange = 40;
        public float nearAlertRange = 10;
        public float detectBehindRange = 5;
        public float meleeAttackRange = 3;
        public float meleeAttackDamage = 10;
        public float rangeAttackRange = 35;
        public float rangeAttackDamage = 5;
        public float rangeAttackSpread = 0.5f;
        public float attackRate = 0.4f;
        [HideInInspector]
        public float nextAttack;
        public float fleeRange = 25;
        public float offset = 0.4f;
        public int requiredDetectionCount = 15;

        public bool hasRangeAttack;
        public bool hasMeleeAttack;
        public bool isMeleeAttacking;

        public Transform myFollowtarget;
        [HideInInspector]
        public Transform pursueTarget;
        [HideInInspector]
        public Vector3 locationOfInterest;
        [HideInInspector]
        public Vector3 wanderLocationOfTarget;
        [HideInInspector]
        public Transform myAttacker;

        //For sights
        public LayerMask sightLayers;
        public LayerMask myEnemyLayers;
        public LayerMask myFriendlyLayers;
        public string[] myEnemyTags;
        public string[] myFriendlyTags;

        //References to store
        public Transform[] waypoints;
        public Transform head;
        public MeshRenderer meshRendererFlag;
        public GameObject rangeWeapon;
        public NPCManager_Master npcManagerMasterScript;
        [HideInInspector]
        public NavMeshAgent myNavMeshAgent;

        //Used for state AI
        public NPCManager_InterfaceState currentState;
        public NPCManager_InterfaceState capturedState;
        public NPCManager_PatrolState patrolState;
        public NPCManager_AlertState alertState;
        public NPCManager_PursueState pursueState;
        public NPCManager_MeleeAttackState meleeAttackState;
        public NPCManager_RangeAttackState rangeAttackState;
        public NPCManager_FleeState fleeState;
        public NPCManager_StruckState struckState;
        public NPCManager_InvestigateState investigateState;
        public NPCManager_FollowState followState;
            
            
             
        void Awake()
		{
            initiate();
            setReferences();
            npcManagerMasterScript.NPCDieEvent += disableThis;
            npcManagerMasterScript.NPCHealthLowEvent += activateFleeState;
            npcManagerMasterScript.NPCHealthRecoveredEvent += activatePatrolState;
            npcManagerMasterScript.NPCDeductHealthEvent += activateStruckState;
		}

		void OnDisable()
		{
            npcManagerMasterScript.NPCDieEvent -= disableThis;
            npcManagerMasterScript.NPCHealthLowEvent -= activateFleeState;
            npcManagerMasterScript.NPCHealthRecoveredEvent -= activatePatrolState;
            npcManagerMasterScript.NPCDeductHealthEvent -= activateStruckState;
            StopAllCoroutines();
        }
	
		void Start () 
		{
            initiate();
		}
	
		void Update () 
		{
            carryOutUpdateState();
		}

		void initiate()
		{
            myNavMeshAgent = GetComponent<NavMeshAgent>();
            activatePatrolState();
		}

        void setReferences()
        {
            patrolState = new NPCManager_PatrolState(this);
            alertState = new NPCManager_AlertState(this);
            pursueState = new NPCManager_PursueState(this);
            fleeState = new NPCManager_FleeState(this);
            followState = new NPCManager_FollowState(this);
            meleeAttackState = new NPCManager_MeleeAttackState(this);
            rangeAttackState = new NPCManager_RangeAttackState(this);
            struckState = new NPCManager_StruckState(this);
            investigateState = new NPCManager_InvestigateState(this);
        }

        void carryOutUpdateState()
        {
            if(Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;
                currentState.updateState();
            }
        }

        void activatePatrolState()
        {
            currentState = patrolState;
        }

        void activateFleeState()
        {
            if(currentState == struckState)
            {
                capturedState = fleeState;
                return;
            }
            currentState = fleeState;
        }

        void activateStruckState(int dummy)
        {
            StopAllCoroutines();

            if(currentState != struckState)
            {
                capturedState = currentState;
            }

            if(rangeWeapon != null) //required if no gun holding animation
            {
                rangeWeapon.SetActive(false);
            }

            if(myNavMeshAgent.enabled)
            {
                myNavMeshAgent.Stop();
            }

            currentState = struckState;

            npcManagerMasterScript.callNPCStruckAnimEvent();

            StartCoroutine(recoverFromStruckState());
        }

        IEnumerator recoverFromStruckState()
        {
            yield return new WaitForSeconds(1.5f);

            npcManagerMasterScript.callNPCRecoveredAnimEvent();

            if (rangeWeapon != null)
            {
                rangeWeapon.SetActive(true);
            }

            if (myNavMeshAgent != null && myNavMeshAgent.enabled) 
            {
                myNavMeshAgent.Resume();
            }

            currentState = capturedState;
        }

        public void onEnemyAttack()  //called by melee animation
        {
            if(pursueState != null)
            {
                //Debug.Log(pursueTarget.name);
                if (pursueTarget != null)
                {
                    if (Vector3.Distance(transform.position, pursueTarget.position) <= meleeAttackRange)
                    {
                        Vector3 toOther = pursueTarget.position - transform.position;
                        if (Vector3.Dot(toOther, transform.forward) > 0.5f)
                        {
                            pursueTarget.SendMessage("callDecreasePlayerHealthEvent", meleeAttackDamage, SendMessageOptions.DontRequireReceiver);
                            pursueTarget.SendMessage("damageProcess", meleeAttackDamage, SendMessageOptions.DontRequireReceiver);
                        }
                    }
                }
            }

            isMeleeAttacking = false;
        }

        public void setMyAttacker(Transform attacker)
        {
            myAttacker = attacker;
        }

        public void distract(Vector3 distractPos)
        {
            locationOfInterest = distractPos;

            if(currentState == patrolState)
            {
                currentState = alertState;
            }
        }

        void disableThis()
        {
            meshRendererFlag.enabled = false;
        }
	}

}
