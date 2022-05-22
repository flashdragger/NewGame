using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.FSM
{
    public class WanderState : FSMState
    {
        // Animator animator;

        public float times = 3;
        private int s;//����һ����
        GameObject go;
        protected override void init()
        {
            StateID = FSMStateID.Wander;
        }
        public override void OnStateEnter(FSMBase fsm)
        {
            fsm.animator.SetBool("Walk",true);
            CharacterFSM_boss ch = fsm.GetComponent<CharacterFSM_boss>();
            go = fsm.gameObject;
            go.AddComponent<Wander>();
        }
        public override void OnStateStay(FSMBase fsm)
        {
            times -= Time.deltaTime;
            s = (int)times % 60; //С��ת���� 
            if (times <= 0)
            {
                int r=Random.Range(0, 2);
                if(r== 0)
                    fsm.changeActiveState(FSMStateID.Attack);
                else
                    fsm.changeActiveState(FSMStateID.Spike);
            }
        }
        public override void OnStateExit(FSMBase fsm)
        {
            MonoBehaviour.Destroy(go.GetComponent<Wander>());
            times = 3;
            fsm.animator.SetBool("Walk",false);
    }
        
    }
}
