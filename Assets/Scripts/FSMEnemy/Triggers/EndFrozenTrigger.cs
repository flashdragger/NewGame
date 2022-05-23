using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.FSM {
    public class EndFrozenTrigger : FSMTrigger
    {
        protected override void init()
        {
            TriggerID = FSMTriggerID.EndFrozenTrigger;
        }

        public override bool HandleTrigger(FSMBase fsm)
        {
            CharacterFSM_boss ch = (CharacterFSM_boss)fsm;
            if (ch.FrozenTimer <= 0) 
                return true;
            return false;
        }
    }
}