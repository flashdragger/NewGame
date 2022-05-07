using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.FSM
{
    public class SpikeState : FSMState
    {
        public float times = 3;
        private int s;//����һ����
        protected override void init()
        {
            StateID = FSMStateID.Spike;
        }
        public override void OnStateEnter(FSMBase fsm)
        {

        }
        public override void OnStateStay(FSMBase fsm)
        {
            Debug.Log("ccc");
            //��ʱ����ɵ���ʱ�Ĺ���
            times -= Time.deltaTime;
            s = (int)times % 60; //С��ת���� 
            if (times <= 0)
            {
                fsm.changeActiveState(FSMStateID.Wander);
            }
        }
        public override void OnStateExit(FSMBase fsm)
        {
            times = 3;
        }
    }
}
