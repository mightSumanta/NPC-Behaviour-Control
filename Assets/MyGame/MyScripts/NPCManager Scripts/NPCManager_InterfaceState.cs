using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter9
{
	public interface NPCManager_InterfaceState 
	{
        void updateState();
        void toPatrolState();
        void toAlertState();
        void toPursueState();
        void toMeleeAttackState();
        void toRangeAttackState();
	}

}
