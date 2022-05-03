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

            foreach (GameObject item in ch.AttackPool) {
                if (item.activeSelf == false) {
                    item.transform.position = ch.transform.position;
                    item.transform.rotation = rotation;
                    item.SetActive(true);
                    break;
                }
            }         

            ch.MousePosition = Vector2.zero;
        }
    }
}