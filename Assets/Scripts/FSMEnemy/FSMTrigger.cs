using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.FSM
{
    public abstract class FSMTrigger
    {
        public FSMTrigger()
        {
            init();
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns>������������true, ���򷵻�false</returns>
        public abstract bool HandleTrigger(FSMBase fsm);

        /// <summary>
        /// ��������ʼ��TriggerID
        /// </summary>
        protected abstract void init();

        public FSMTriggerID TriggerID { get; set; }
    }
}
