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
           o1 =GameObject.Instantiate(ch.prefab, new Vector2(ch.transform.position.x-1, ch.transform.position.y-1), ch.transform.rotation);
           o2= GameObject.Instantiate(ch.prefab, new Vector2(ch.transform.position.x + 1, ch.transform.position.y - 1), ch.transform.rotation);
           o3= GameObject.Instantiate(ch.prefab, new Vector2(ch.transform.position.x - 1, ch.transform.position.y + 1), ch.transform.rotation);
           o4= GameObject.Instantiate(ch.prefab, new Vector2(ch.transform.position.x+1, ch.transform.position.y+1), ch.transform.rotation);

        }
        public override void OnStateStay(FSMBase fsm)
        {
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
                fsm.changeActiveState(FSMStateID.Wander);

            }
        }
        public override void OnStateExit(FSMBase fsm)
        {
            times = 3;
        }
    }
}
