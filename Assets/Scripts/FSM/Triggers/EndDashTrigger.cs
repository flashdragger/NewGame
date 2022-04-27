using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    public class EndDashTrigger : FSMTrigger
    {
        protected override void init() {
            this.TriggerID = FSMTriggerID.EndDashTrigger;
        }

        public override bool HandleTrigger(FSMBase fsm)
        {
            Character ch = fsm.GetComponent<Character>();
            if (ch != null) {
                return ch.DashTimer < 0;
            }
            throw new NotSupportedException("No Character Component");
        }
    }
}
