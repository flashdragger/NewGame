using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.FSM
{
    public class AttackState : FSMState
    {
        public float times = 5;
        public float bulletTime = 1;
        private int s;

        float Speed = 10;
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
            times -= Time.deltaTime;
            bulletTime -= Time.deltaTime;
            s = (int)times % 60; 
            if (times <= 0)
            {
                fsm.changeActiveState(FSMStateID.Dash);
            }

            s = (int)bulletTime % 60;
            if (bulletTime <= 0)
            {
                Rigidbody2D clone;
            Vector2 direction =-new Vector2 (FPonit.position.x,FPonit.position.y) + new Vector2(player.transform.position.x, player.transform.position.y);

            float angleDir = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

             Quaternion rotation = Quaternion.AngleAxis(angleDir - 90,Vector3.forward);

                clone = (Rigidbody2D)MonoBehaviour.Instantiate(Bullet, FPonit.position, rotation);
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
