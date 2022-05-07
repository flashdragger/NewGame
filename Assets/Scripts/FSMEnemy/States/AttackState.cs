using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.FSM
{
    public class AttackState : FSMState
    {
        public float times = 5;
        public float bulletTime = 1;
        private int s;//定义一个秒

        float Speed = 10;//速度
        public Rigidbody2D Bullet;
        public Transform FPonit;
        public GameObject player;
        protected override void init()
        {
            StateID = FSMStateID.Attack;
        }
        public override void OnStateEnter(FSMBase fsm)
        {
            CharacterFSM ch = fsm.GetComponent<CharacterFSM>();
            Bullet = ch.Bullet;
            FPonit = ch.FPonit;
            Speed = ch.BulletSpeed;
            player = GameObject.FindGameObjectWithTag("Player");
        }
        public override void OnStateStay(FSMBase fsm)
        {
            Debug.Log("bbb");
            //计时器完成倒计时的功能
            times -= Time.deltaTime;
            bulletTime -= Time.deltaTime;
            s = (int)times % 60; //小数转整数 
            if (times <= 0)
            {
                fsm.changeActiveState(FSMStateID.Dash);
            }

            //发射
            s = (int)bulletTime % 60;
            if (bulletTime <= 0)
            {
                Rigidbody2D clone;
                clone = (Rigidbody2D)MonoBehaviour.Instantiate(Bullet, FPonit.position, FPonit.rotation);
                Vector2 aim = player.transform.position - FPonit.position;
                aim = aim.normalized;
                clone.velocity = aim*Speed;
                bulletTime = 1;
            }
        }
        public override void OnStateExit(FSMBase fsm)
        {
            times = 5;
        }
    }
}
