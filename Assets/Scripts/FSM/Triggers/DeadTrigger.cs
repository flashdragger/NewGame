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
            CharacterManager cm = ch.gameObject.GetComponent<CharacterManager>();
            if (ch != null && ch.gameObject.GetComponentInChildren<Attributes>().HP <= 0) {
                foreach (GameObject item in cm.Characters)
                {
                    if (item.GetComponent<Attributes>().HP > 0) 
                    {
                        cm.ChangeCharacter(cm.Characters.IndexOf(item));
                        return false;
                    }
                }
                return true;
            }
            
            return false;
        }
    }

}