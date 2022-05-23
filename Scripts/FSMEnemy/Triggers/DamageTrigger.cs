using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.FSM
{
    public class DamageTrigger : FSMTrigger
    {
        protected override void init()
        {
            TriggerID = FSMTriggerID.DamageTrigger;
        }

        public override bool HandleTrigger(FSMBase fsm)
        {
            CharacterFSM_boss ch = (CharacterFSM_boss)fsm;
            if (ch != null && ch.DamageAmount != 0)
            {
                // ch.TakeDamage();
                return true;
            }
            return false;
        }
    }
}
