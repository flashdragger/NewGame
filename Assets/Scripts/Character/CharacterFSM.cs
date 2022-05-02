using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    public class CharacterFSM : FSMBase
    {
        public int PS = 100; //角色体力
        private int _recoverTime = 0;

        #region Move and Dash
        public float MoveSpeed = 5f;
        private int _dashCost;
        public float DashSpeed = 10f;
        private float _dashTimer;
        public float DashTime = 0.1f;
        private bool _isDashing;
        private Rigidbody2D _rb;

        public int DashCost {
            get { return _dashCost; }
        }

        public float DashTimer {
            get { return _dashTimer; }
            set { _dashTimer = value; }
        }

        public bool IsDashing {
            get { return _isDashing; }
            set { _isDashing = value; }
        }

        #endregion

        #region Attack

        
        
        #endregion

        public Rigidbody2D Rb {
            get { return _rb; }
        }

        protected override void init() {
            _rb = GetComponent<Rigidbody2D>();
            _dashTimer = DashTime;
            _dashCost = 10;
            _isDashing = false;
        }

        protected override void setUpFSM()
        {
            base.setUpFSM();

            IdleState idleState = new IdleState();
            idleState.AddMap(FSMTriggerID.StartMoveTrigger, FSMStateID.Move);
            _states.Add(idleState);

            MoveState moveState = new MoveState();
            moveState.AddMap(FSMTriggerID.StartDashTrigger, FSMStateID.Dash);
            moveState.AddMap(FSMTriggerID.EndMoveTrigger, FSMStateID.Idle);
            _states.Add(moveState);

            DashState dashState = new DashState();
            dashState.AddMap(FSMTriggerID.EndDashTrigger, FSMStateID.Idle);
            _states.Add(dashState);
        }

        private void FixedUpdate() {
            if(PS != 100) {
                if(_recoverTime != 50)
                    _recoverTime++;
                else {
                    PS++;
                    _recoverTime = 0;
                }
            }
        }
    }
}