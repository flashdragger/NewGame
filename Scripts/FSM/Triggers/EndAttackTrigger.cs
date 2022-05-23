using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    public class EndAttackTrigger : FSMTrigger
    {
        protected override void init()
        {
            TriggerID = FSMTriggerID.EndAttackTrigger;
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