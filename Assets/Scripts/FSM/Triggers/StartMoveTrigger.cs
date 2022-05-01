using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM 
{
public class StartMoveTrigger : FSMTrigger
{
    protected override void init() {
        TriggerID = FSMTriggerID.StartMoveTrigger;
    }

    public override bool HandleTrigger(FSMBase fsm) {
        return Input.GetButton("Horizontal") || Input.GetButton("Vertical");
    }
}
}