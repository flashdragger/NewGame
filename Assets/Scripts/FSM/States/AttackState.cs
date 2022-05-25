using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM 
{
    public class AttackState : FSMState
    {
        protected override void init()
        {
            StateID = FSMStateID.Attack;
        }

        public override void OnStateEnter(FSMBase fsm)
        {
            CharacterFSM ch = fsm.GetComponent<CharacterFSM>();
            Vector2 direction = ch.MousePosition - new Vector2(ch.transform.position.x, ch.transform.position.y);
            float angleDir = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angleDir - 90,Vector3.forward);
            GameObject gameObject = MonoBehaviour.Instantiate(ch.GetComponentInChildren<Attributes>().AttackPrefab);
            gameObject.transform.position = ch.transform.position;
            gameObject.transform.rotation = rotation;
            gameObject.SetActive(true); 

            ch.MousePosition = Vector2.zero;
            ch.AttackState = false;
            ch.IntervalTimer = ch.AttackInterval;
        }

        public override void OnStateStay(FSMBase fsm) 
        {
            CharacterFSM ch = fsm.GetComponent<CharacterFSM>();
            if (ch.MousePosition != Vector2.zero) {
                ch.MousePosition = Vector2.zero;
            }
        }
    }
}