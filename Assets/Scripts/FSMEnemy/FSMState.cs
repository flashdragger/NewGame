using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.FSM
{
    public abstract class FSMState
    {
        public FSMState()
        {
            init();
            _map = new Dictionary<FSMTriggerID, FSMStateID>();
            _triggers = new List<FSMTrigger>();
        }

        /// <summary>
        /// ��������ʼ��StateID
        /// </summary>
        protected abstract void init();

        /// <summary>
        /// Ϊ��ǰ״̬���ӳ�䣬��״̬������
        /// </summary>
        /// <param name="triggerID">����ID</param>
        /// <param name="stateID">����������ת�Ƶ���״̬ID</param>
        public void AddMap(FSMTriggerID triggerID, FSMStateID stateID)
        {
            _map.Add(triggerID, stateID);
            Type triggerType = Type.GetType("Enemy.FSM." + triggerID);
            FSMTrigger trigger = Activator.CreateInstance(triggerType) as FSMTrigger;
            _triggers.Add(trigger);
        }
        /// <summary>
        /// �ж��Ƿ������������л�״̬
        /// </summary>
        /// <param name="fsm">��Ҫ�л���״̬��</param>
        public void Reason(FSMBase fsm)
        {
            foreach (FSMTrigger i in _triggers)
            {
                if (i.HandleTrigger(fsm))
                {
                    fsm.changeActiveState(_map[i.TriggerID]);
                    return;
                }
            }
        }

        public virtual void OnStateEnter(FSMBase fsm)
        {
            // if (fsm.animator != null) {
            //     fsm.animator.SetTrigger(StateID.ToString() + "Trigger");
            // }
        }
        public virtual void OnStateStay(FSMBase fsm) { }
        public virtual void OnStateExit(FSMBase fsm)
        {
            // if (fsm.animator != null) {
            //     fsm.animator.ResetTrigger(StateID.ToString() + "Trigger");
            // }
        }

        public FSMStateID StateID { get; set; }
        private List<FSMTrigger> _triggers;
        private Dictionary<FSMTriggerID, FSMStateID> _map;
    }
}
