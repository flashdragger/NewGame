using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Enemy.FSM
{
    public class DashState : FSMState
    {
        public float times = 1;
        private int s;//定义一个秒
        public GameObject player;
        private Vector2 _initVelocity;
        private Rigidbody2D rb2d;
        protected override void init()
        {
            StateID = FSMStateID.Dash;
        }
        public override void OnStateEnter(FSMBase fsm)
        {
            CharacterFSM ch = fsm.GetComponent<CharacterFSM>();
            player = GameObject.FindGameObjectWithTag("Player");
            Vector2 aim=player.transform.position-ch.FPonit.position;
            aim = aim.normalized;
            _initVelocity = ch.Rb.velocity;
            rb2d = ch.Rb;
            ch.Rb.velocity = ch.DashSpeed * aim;
            ch.IsDashing = true;
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
            rb2d.velocity = _initVelocity;
            times = 1;
        }
    }
}
