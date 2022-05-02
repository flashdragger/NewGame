using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    public class DamageState : FSMState
    {
        protected override void init()
        {
            StateID = FSMStateID.Damage;
        }

        public override void OnStateEnter(FSMBase fsm)
        {
            CharacterFSM ch = fsm.GetComponent<CharacterFSM>();
            ch.TakeDamage();
        }
    }
}