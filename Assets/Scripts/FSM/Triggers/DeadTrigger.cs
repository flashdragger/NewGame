using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM {
    public class DeadTrigger : FSMTrigger
    {
        protected override void init()
        {
            TriggerID = FSMTriggerID.DeadTrigger;
        }

        public override bool HandleTrigger(FSMBase fsm)
        {
            CharacterFSM ch = (CharacterFSM) fsm;
            if (ch != null && ch.gameObject.GetComponentInChildren<Attributes>().HP <= 0)
                return true;
            return false;
        }
    }

}