using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

namespace Enemy.FSM
{
    

    public class SpikeState : FSMState
    {
        private GameObject o1;
        private GameObject o2;
        private GameObject o3;
        private GameObject o4;
        private GameObject o11;
        private GameObject o21;
        private GameObject o31;
        private GameObject o41;
        [Range(0, 10)]
        public float AlertRadius;
        [Range(0, 360)]
        public float Alertangle;//�Ƕ�
        public float times = 3;
        private int s;//����һ����
        protected override void init()
        {
            StateID = FSMStateID.Spike;
        }
        
        public override void OnStateEnter(FSMBase fsm)
        {
            CharacterFSM_boss ch = (CharacterFSM_boss)fsm;
            /* Color color = Handles.color;
             Handles.color = Color.blue;
             Vector3 StartLine = Quaternion.Euler(0, -Alertangle, 0) * ch.transform.forward;
             Handles.DrawSolidArc(ch.transform.position, ch.transform.up, StartLine, Alertangle, AlertRadius);
             Handles.color = color;*/
           GameObject bird = GameObject.FindGameObjectWithTag("Player");
           o1 =GameObject.Instantiate(ch.prefabSpike, new Vector2(bird.transform.position.x- 1.5f, bird.transform.position.y- 1.35f), ch.transform.rotation);
           o2= GameObject.Instantiate(ch.prefabSpike, new Vector2(bird.transform.position.x + 1.5f, bird.transform.position.y - 1.35f), ch.transform.rotation);
           o3= GameObject.Instantiate(ch.prefabSpike, new Vector2(bird.transform.position.x - 1.55f, bird.transform.position.y + 1.65f), ch.transform.rotation);
           o4= GameObject.Instantiate(ch.prefabSpike, new Vector2(bird.transform.position.x+ 1.55f, bird.transform.position.y+ 1.65f), ch.transform.rotation);
            o11 = GameObject.Instantiate(ch.prefabTrap, new Vector2(bird.transform.position.x - 1.5f, bird.transform.position.y - 1.5f), ch.transform.rotation);
            o21 = GameObject.Instantiate(ch.prefabTrap, new Vector2(bird.transform.position.x + 1.45f, bird.transform.position.y - 1.5f), ch.transform.rotation);
            o31 = GameObject.Instantiate(ch.prefabTrap, new Vector2(bird.transform.position.x - 1.45f, bird.transform.position.y + 1.5f), ch.transform.rotation);
            o41 = GameObject.Instantiate(ch.prefabTrap, new Vector2(bird.transform.position.x + 1.5f, bird.transform.position.y + 1.5f), ch.transform.rotation);

        }
        public override void OnStateStay(FSMBase fsm)
        {
            fsm.animator.SetBool("Spike",true);
            Debug.Log("ccc");
            //��ʱ����ɵ���ʱ�Ĺ���
            times -= Time.deltaTime;
            s = (int)times % 60; //С��ת���� 
            if (times <= 0)
            {
                GameObject.Destroy(o1);
                GameObject.Destroy(o2);
                GameObject.Destroy(o3);
                GameObject.Destroy(o4);
                GameObject.Destroy(o11);
                GameObject.Destroy(o21);
                GameObject.Destroy(o31);
                GameObject.Destroy(o41);
                fsm.changeActiveState(FSMStateID.Wander);

            }
        }
        public override void OnStateExit(FSMBase fsm)
        {
            times = 3;
            fsm.animator.SetBool("Spike",false);

        }
    }
}
