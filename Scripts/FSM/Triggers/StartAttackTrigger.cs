using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    public class StartAttackTrigger : FSMTrigger
    {
        protected override void init()
        {
            TriggerID = FSMTriggerID.StartAttackTrigger;
        }

        public override bool HandleTrigger(FSMBase fsm)
        {
            CharacterFSM ch = (CharacterFSM) fsm;
            if (ch != null) {
                return !ch.ChargeAttackState && ch.AttackState && ch.IntervalTimer <= 0;
            }
            return false;
        }
    }
}