using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    public class EndChargeAttackTrigger : FSMTrigger
    {
        protected override void init()
        {
            TriggerID = FSMTriggerID.EndChargeAttackTrigger;
        }

        public override bool HandleTrigger(FSMBase fsm)
        {
            CharacterFSM ch = (CharacterFSM) fsm;
            if (ch != null && ch.MousePosition == Vector2.zero) {
                return true;
            }
            return false;
        }
    }
}
