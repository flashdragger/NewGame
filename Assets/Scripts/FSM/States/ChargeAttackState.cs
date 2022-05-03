using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    public class ChargeAttackState : FSMState
    {
        protected override void init()
        {
            StateID = FSMStateID.ChargeAttack;
        }

        public override void OnStateEnter(FSMBase fsm)
        {
            CharacterFSM ch = fsm.GetComponent<CharacterFSM>();
            Vector2 direction = ch.MousePosition - new Vector2(ch.transform.position.x, ch.transform.position.y);
            float angleDir = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angleDir - 90,Vector3.forward);
            GameObject gameObject = MonoBehaviour.Instantiate(ch.GetComponentInChildren<Attributes>().ChargeAttackPrefab);
            gameObject.transform.position = ch.transform.position;
            gameObject.transform.rotation = rotation;
            gameObject.SetActive(true);

            ch.MousePosition = Vector2.zero;
            ch.AttackState = false;
        }

    }
}
