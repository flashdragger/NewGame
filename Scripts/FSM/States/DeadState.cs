using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM {
    public class DeadState : FSMState
    {
        protected override void init() 
        {
            StateID = FSMStateID.Dead;
        }

        public override void OnStateEnter(FSMBase fsm)
        {
            CharacterFSM ch = (CharacterFSM) fsm;
            // MonoBehaviour.Destroy(ch.gameObject);
            Debug.Log("Dead");
        }
    }

}
