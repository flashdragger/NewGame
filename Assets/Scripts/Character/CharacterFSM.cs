using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    public class CharacterFSM : FSMBase
    {
        public float MaxPS = 100;
        public float PS = 100; //角色体力
        public int RecoverTime = 50;
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
        private Vector2 _mousePosition;
        private bool _attackState = false;
        private bool _chargeAttackState = false;
        private float _keyTime = 0f;
        private bool _addTime = false;
        public float ChargeTime = 1.0f;
        public float AttackInterval = 1.0f;
        private float _intervalTimer = 0f;

        public float IntervalTimer {
            get {return _intervalTimer;}
            set {_intervalTimer = value;}
        }

        public Vector2 MousePosition {
            get { return _mousePosition; }
            set { _mousePosition = value; }
        }

        public float KeyTime {
            get { return _keyTime; }
        }

        public bool AttackState {
            get { return _attackState; }
            set { _attackState = value; }
        }

        public bool ChargeAttackState {
            get { return _chargeAttackState; }
        }

        #endregion

        #region Damage
        [SerializeField]
        private float _damageAmount;
        private Attributes.Elements _damageType;
        public float InvincibleTime = 0.1f;
        private float _invincibleTimer; 
        private bool _isInvincible = false;
        
        public float DamageAmount {
            get { return _damageAmount; }
            set { _damageAmount = value; }
        }
        public float InvincibleTimer {
            get { return _invincibleTimer; }
            set { _invincibleTimer = value; }
        }

        public bool IsInvincible {
            get { return _isInvincible; }
            set { _isInvincible = value; }
        }

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
            idleState.AddMap(FSMTriggerID.StartAttackTrigger, FSMStateID.Attack);
            idleState.AddMap(FSMTriggerID.StartChargeAttackTrigger, FSMStateID.ChargeAttack);
            idleState.AddMap(FSMTriggerID.StartDamageTrigger, FSMStateID.Damage);
            idleState.AddMap(FSMTriggerID.DeadTrigger, FSMStateID.Dead);
            _states.Add(idleState);

            MoveState moveState = new MoveState();
            moveState.AddMap(FSMTriggerID.StartDashTrigger, FSMStateID.Dash);
            moveState.AddMap(FSMTriggerID.EndMoveTrigger, FSMStateID.Idle);
            moveState.AddMap(FSMTriggerID.StartAttackTrigger, FSMStateID.Attack);
            moveState.AddMap(FSMTriggerID.StartChargeAttackTrigger, FSMStateID.ChargeAttack);
            moveState.AddMap(FSMTriggerID.StartDamageTrigger, FSMStateID.Damage);
            moveState.AddMap(FSMTriggerID.DeadTrigger, FSMStateID.Dead);
            _states.Add(moveState);

            DashState dashState = new DashState();
            dashState.AddMap(FSMTriggerID.EndDashTrigger, FSMStateID.Idle);
            dashState.AddMap(FSMTriggerID.DeadTrigger, FSMStateID.Dead);
            _states.Add(dashState);

            AttackState attackState = new AttackState();
            attackState.AddMap(FSMTriggerID.EndAttackTrigger, FSMStateID.Idle);
            attackState.AddMap(FSMTriggerID.DeadTrigger, FSMStateID.Dead);
            _states.Add(attackState);

            DamageState damageState = new DamageState();
            damageState.AddMap(FSMTriggerID.EndDamageTrigger, FSMStateID.Idle);
            damageState.AddMap(FSMTriggerID.DeadTrigger, FSMStateID.Dead);
            _states.Add(damageState);

            ChargeAttackState chargeAttackState = new ChargeAttackState();
            chargeAttackState.AddMap(FSMTriggerID.EndChargeAttackTrigger, FSMStateID.Idle);
            chargeAttackState.AddMap(FSMTriggerID.DeadTrigger, FSMStateID.Dead);
            _states.Add(chargeAttackState);

            DeadState deadState = new DeadState();
            _states.Add(deadState);
        }

        private new void Update() {
            _currentState.Reason(this);
            _currentState.OnStateStay(this);
            KeyDetect();
            if (_intervalTimer > 0) {
                _intervalTimer -= Time.deltaTime;
            }
        }

        private void FixedUpdate() {
            if(PS != 100) {
                if(_recoverTime != RecoverTime)
                    _recoverTime++;
                else {
                    PS++;
                    _recoverTime = 0;
                }
            }
        }

        public void TakeDamage() {
            Attributes attribute = GetComponentInChildren<Attributes>();
            attribute.HP -= _damageAmount;
            StartCoroutine(CharacterFlick());
        }

        private IEnumerator CharacterFlick()
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(InvincibleTime);
            GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }

        private void KeyDetect() {
            if (Input.GetMouseButtonDown(0)) {
                _addTime = true;
            }
            if (Input.GetMouseButtonUp(0)) {
                if (_keyTime < ChargeTime) 
                    _chargeAttackState = false;
                else if (_keyTime > ChargeTime) 
                    _chargeAttackState = true;
                _attackState = true;
                _addTime = false;
                _keyTime = 0f;
                _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            if (_addTime) 
                _keyTime += Time.deltaTime;
            
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (!_isInvincible && collision.gameObject.name != "Boss")
                _damageAmount = collision.gameObject.GetComponent<AttackObject>().BaseDamage;
            else if (!_isInvincible && collision.gameObject.name == "Boss") 
                _damageAmount = 10;
        }
    }
}