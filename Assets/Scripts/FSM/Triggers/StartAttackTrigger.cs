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
            if (ch != null && Input.GetMouseButtonDown(0)) {
                ch.MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                return true;
            }
            return false;
        }
    }
}