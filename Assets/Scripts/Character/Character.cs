using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    public class Character : FSMBase
    {
        [HideInInspector]
        public enum Elements {
            water,
            fire,
            ice
        }
        public Elements EleAttribute;
        public int HP;
        public int PS; //角色体力
        public float MoveSpeed;
        private int _dashCost;
        public float DashSpeed;
        private float _dashTimer;
        public float DashTime;
        private bool _isDashing;
        private Rigidbody2D _rb;

        public int DashCost {
            get { return _dashCost; }
        }

        public float DashTimer {
            get { return _dashTimer; }
            set { _dashTimer = value; }
        }

        public Rigidbody2D Rb {
            get { return _rb; }
        }

        public bool IsDashing {
            get { return _isDashing; }
            set { _isDashing = value; }
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
            idleState.AddMap(FSMTriggerID.MoveTrigger, FSMStateID.Move);
            _states.Add(idleState);

            MoveState moveState = new MoveState();
            moveState.AddMap(FSMTriggerID.StartDashTrigger, FSMStateID.Dash);
            _states.Add(moveState);

            DashState dashState = new DashState();
            dashState.AddMap(FSMTriggerID.EndDashTrigger, FSMStateID.Idle);
            _states.Add(dashState);
        }
    }
}