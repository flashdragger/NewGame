using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    public class StartDamageTrigger : FSMTrigger
    {
        protected override void init()
        {
            TriggerID = FSMTriggerID.StartDamageTrigger;
        }

        public override bool HandleTrigger(FSMBase fsm)
        {
            CharacterFSM ch = (CharacterFSM) fsm;
            if (ch != null && ch.DamageAmount != 0) 
                return true;            
            return false;
        }
    }

}
