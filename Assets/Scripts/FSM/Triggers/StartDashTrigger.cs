using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM 
{
    public class StartDashTrigger : FSMTrigger
    {
        protected override void init()
        {
            TriggerID = FSMTriggerID.StartDashTrigger;
        }

        public override bool HandleTrigger(FSMBase fsm)
        {
            Character ch = (Character) fsm;
            return ((Input.GetKey(KeyCode.LeftShift) || Input.GetMouseButton(1)) && ch.PS >= ch.DashCost) || ch.IsDashing == true;;
        }
    }
}
