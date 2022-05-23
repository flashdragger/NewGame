using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    public class MoveState : FSMState
    {
        protected override void init() {
            StateID = FSMStateID.Move;
        }

        public override void OnStateStay(FSMBase fsm) {
            CharacterFSM ch = (CharacterFSM) fsm;
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            ch.Rb.velocity = ch.MoveSpeed * (new Vector2(horizontal, vertical));
            if (horizontal < 0) {
                ch.gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(0, 180, 0);
            } else if (horizontal > 0) {
                ch.gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}
