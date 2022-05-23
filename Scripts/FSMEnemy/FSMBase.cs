using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.FSM
{
    /// <summary>
    /// <para>����״̬��</para>
    /// <para>������Ҫ���ø���Start, Update���������ò���дsetUpFSM����</para>
    /// </summary>
    public class FSMBase : MonoBehaviour
    {
        /// <summary>
        /// ��ʼ��״̬����������Ҫ����base.setUpFSM()
        /// </summary>
        protected virtual void setUpFSM()
        {
            _states = new List<FSMState>();
        }
        /// <summary>
        /// ��ʼ���������
        /// </summary>
        protected virtual void init()
        {
            defaultStateID = FSMStateID.Wander;
        }
        protected void loadDefaultState()
        {
            // ����Ĭ��״̬
            _currentState = _defaultState = _states.Find(s => s.StateID == defaultStateID);
            _currentState.OnStateEnter(this);
            _currentState=(WanderState)_currentState;
            currentStateID = defaultStateID.ToString();
        }
        public void Start()
        {
            init();
            setUpFSM();
            loadDefaultState();
        }
        public void Update()
        {
            // ����״̬
            _currentState.Reason(this);
            _currentState.OnStateStay(this);
        }
        /// <summary>
        /// �л���ǰ״̬��Ŀ��״̬
        /// </summary>
        /// <param name="targetStateID">Ŀ��״̬ID</param>
        public void changeActiveState(FSMStateID targetStateID)
        {
            _currentState.OnStateExit(this);
            _currentState = targetStateID == FSMStateID.Wander ? _defaultState : _states.Find(s => s.StateID == targetStateID);
            currentStateID = _currentState.StateID.ToString();
            _currentState.OnStateEnter(this);
        }

        [Tooltip("Ĭ��״̬"), Header("״̬����")]
        public FSMStateID defaultStateID;
        protected FSMState _defaultState;
        protected List<FSMState> _states;
        protected FSMState _currentState;
        [ReadOnly]
        public string currentStateID;
        public Animator animator;
    }
}
