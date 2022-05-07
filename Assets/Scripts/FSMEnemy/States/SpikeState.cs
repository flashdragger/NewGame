using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.FSM
{
    public class SpikeState : FSMState
    {
        public float times = 3;
        private int s;//定义一个秒
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
            //计时器完成倒计时的功能
            times -= Time.deltaTime;
            s = (int)times % 60; //小数转整数 
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
