using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.FSM {
    public class FrozenState : FSMState
    {
        protected override void init()
        {
            StateID = FSMStateID.Frozen;
        }

        public override void OnStateEnter(FSMBase fsm)
        {
            fsm.animator.SetBool("Frozen",true);
            CharacterFSM_boss boss = (CharacterFSM_boss)fsm;
            if (boss != null) {
                boss.FrozenTimer = boss.FrozenTime;
                boss.AttachedElement = Attributes.Elements.ice;
            }
        }

        public override void OnStateStay(FSMBase fsm)
        {
            CharacterFSM_boss boss = (CharacterFSM_boss)fsm;
            if (boss != null) {
                boss.FrozenTimer -= Time.deltaTime;
            }
        }

        public override void OnStateExit(FSMBase fsm)
        {
            fsm.animator.SetBool("Frozen",false);

            CharacterFSM_boss boss = (CharacterFSM_boss)fsm;
            if (boss != null) {
                boss.IsFrozen = false;
            }

        }
    }
}

