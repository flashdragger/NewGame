using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    public class StartChargeAttackTrigger : FSMTrigger
    {
        protected override void init()
        {
            TriggerID = FSMTriggerID.StartChargeAttackTrigger;
        }

        public override bool HandleTrigger(FSMBase fsm)
        {
            CharacterFSM ch = (CharacterFSM) fsm;
            if (ch != null) {
                return ch.ChargeAttackState && ch.AttackState;
            }
            return false;
        }
    }
}
