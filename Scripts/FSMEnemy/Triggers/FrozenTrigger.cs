using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.FSM {
    public class FrozenTrigger : FSMTrigger
    {
        protected override void init()
        {
            TriggerID = FSMTriggerID.FrozenTrigger;
        }

        public override bool HandleTrigger(FSMBase fsm)
        {
            CharacterFSM_boss ch = (CharacterFSM_boss)fsm;
            return ch.IsFrozen;
        }
    }
}