using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    public class IdleState : FSMState
    {

        protected override void init()
        {
            StateID = FSMStateID.Idle;
        }

        public override void OnStateEnter(FSMBase fsm)
        {
            base.OnStateEnter(fsm);
            CharacterFSM ch = (CharacterFSM) fsm;
            ch.Rb.velocity = Vector2.zero;
        }

        public override void OnStateExit(FSMBase fsm)
        {
            base.OnStateExit(fsm);
        }
    }
}