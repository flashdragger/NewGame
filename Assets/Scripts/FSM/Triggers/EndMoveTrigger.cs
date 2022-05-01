using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    public class EndMoveTrigger : FSMTrigger
    {
       protected override void init() {
            this.TriggerID = FSMTriggerID.EndMoveTrigger;
        }

        public override bool HandleTrigger(FSMBase fsm)
        {
            CharacterFSM ch = fsm.GetComponent<CharacterFSM>();
            return ch.Rb.velocity == Vector2.zero;
        }
    }
}
