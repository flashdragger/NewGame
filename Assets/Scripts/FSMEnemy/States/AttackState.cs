using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.FSM
{
    public class AttackState : FSMState
    {
        public float times = 5;
        public float bulletTime = 1;
        private int s;//����һ����

        float Speed = 10;//�ٶ�
        public Rigidbody2D Bullet;
        public Transform FPonit;
        public GameObject player;
        protected override void init()
        {
            StateID = FSMStateID.Attack;
        }
        public override void OnStateEnter(FSMBase fsm)
        {
            CharacterFSM_boss ch = fsm.GetComponent<CharacterFSM_boss>();
            Bullet = ch.Bullet;
            FPonit = ch.FPonit;
            Speed = ch.BulletSpeed;
            player = GameObject.FindGameObjectWithTag("Player");
        }
        public override void OnStateStay(FSMBase fsm)
        {
            Debug.Log("bbb");
            //��ʱ����ɵ���ʱ�Ĺ���
            times -= Time.deltaTime;
            bulletTime -= Time.deltaTime;
            s = (int)times % 60; //С��ת���� 
            if (times <= 0)
            {
                fsm.changeActiveState(FSMStateID.Dash);
            }

            //����
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
