using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    public class DamageState : FSMState
    {
        protected override void init()
        {
            StateID = FSMStateID.Damage;
        }

        public override void OnStateEnter(FSMBase fsm)
        {
            CharacterFSM ch = fsm.GetComponent<CharacterFSM>();
            ch.TakeDamage();
            ch.DamageAmount = 0;
            ch.IsInvincible = true;
            ch.InvincibleTimer = ch.InvincibleTime;
        }

        public override void OnStateStay(FSMBase fsm)
        {
           CharacterFSM ch = fsm.GetComponent<CharacterFSM>();
           ch.InvincibleTimer -= Time.deltaTime;
        }

        public override void OnStateExit(FSMBase fsm)
        {
            CharacterFSM ch = fsm.GetComponent<CharacterFSM>();
            ch.IsInvincible = false;
        }
    }
}