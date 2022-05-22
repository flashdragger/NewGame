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
        private Rigidbody2D clone1;
        private Rigidbody2D clone2;
        private Rigidbody2D clone3;
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
            fsm.animator.SetBool("Fire",true);
            times -= Time.deltaTime;
            bulletTime -= Time.deltaTime;
            s = (int)times % 60; 
            if (times <= 0)
            {
                fsm.changeActiveState(FSMStateID.Dash);
            }

            //����
            s = (int)bulletTime % 60;
            if (bulletTime <= 0)
            {
 
                Vector2 direction =-new Vector2 (FPonit.position.x,FPonit.position.y) + new Vector2(player.transform.position.x, player.transform.position.y);

                float angleDir = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                Quaternion rotation = Quaternion.AngleAxis(angleDir - 90,Vector3.forward);

                clone1 = (Rigidbody2D)MonoBehaviour.Instantiate(Bullet, FPonit.position-new Vector3(-1,1,0), rotation);
                clone2 = (Rigidbody2D)MonoBehaviour.Instantiate(Bullet, FPonit.position-new Vector3(-1,1,0), rotation);
                clone3 = (Rigidbody2D)MonoBehaviour.Instantiate(Bullet, FPonit.position-new Vector3(-1,1,0), rotation);
                Vector2 aim1 = player.transform.position - FPonit.position;
                Vector2 aim2 = player.transform.position - FPonit.position;
                aim2.y+=1;
                Vector2 aim3 = player.transform.position - FPonit.position;
                aim3.y-=1;
                aim1 = aim1.normalized;
                aim2 = aim2.normalized;
                aim3 = aim3.normalized;
                clone1.velocity = aim1*Speed;
                clone2.velocity = aim2*Speed;
                clone3.velocity = aim3*Speed;
                bulletTime = 1;
            }
        }
        public override void OnStateExit(FSMBase fsm)
        {
            times = 5;
            fsm.animator.SetBool("Fire",false);

        }
    }
}
