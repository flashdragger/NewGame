using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM 
{
    public class EndDamageTrigger : FSMTrigger
    {
        protected override void init()
        {
            TriggerID = FSMTriggerID.EndDamageTrigger; 
        }

        public override bool HandleTrigger(FSMBase fsm)
        {
            CharacterFSM ch = (CharacterFSM) fsm;
            if (ch != null && ch.InvincibleTimer <= 0) {
                ch.TakeDamage();
                return true;
            }               
            return false;
        }
    }
}
