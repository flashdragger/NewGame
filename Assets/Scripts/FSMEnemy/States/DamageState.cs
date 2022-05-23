using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.FSM
{
    public class DamageState : FSMState
    {
        protected override void init()
        {
            StateID = FSMStateID.Damage;
        }

        public override void OnStateEnter(FSMBase fsm)
        {
            CharacterFSM_boss ch = fsm.GetComponent<CharacterFSM_boss>();
            ch.TakeDamage();
            ch.DamageAmount = 0;
            
            fsm.changeActiveState(FSMStateID.Wander);
        }
    }
}
