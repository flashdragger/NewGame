using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    public class DashState : FSMState
    {
        private Vector2 _initVelocity;
        protected override void init() 
        {
            StateID = FSMStateID.Dash;
        }

        public override void OnStateEnter(FSMBase fsm) 
        {
        CharacterFSM ch = fsm.GetComponent<CharacterFSM>();
            if (ch != null) {
                ch.PS -= ch.DashCost;
                ch.DashTimer = ch.DashTime;
                _initVelocity = ch.Rb.velocity;
                ch.Rb.velocity = ch.DashSpeed * ch.Rb.velocity.normalized;
                ch.IsDashing = true;
            }
        }

        public override void OnStateStay(FSMBase fsm)
        {
            CharacterFSM ch = fsm.GetComponent<CharacterFSM>();
            if (ch != null) {
                ch.DashTimer -= Time.deltaTime;
            }
        }

        public override void OnStateExit(FSMBase fsm)
        {
            CharacterFSM ch = fsm.GetComponent<CharacterFSM>();
            if (ch != null) {
                ch.DashTimer = ch.DashTime;
                ch.IsDashing = false;
                ch.Rb.velocity = _initVelocity;
            }
        }

    }


}
