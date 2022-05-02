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

        [Range(0,1)]
        public int AttackKind = 0;
        public List<GameObject> AttackPrefabs;
        private List<GameObject> _attackPool;
        private int _poolSize;
        private Vector2 _mousePosition;

        public Vector2 MousePosition {
            get { return _mousePosition; }
            set { _mousePosition = value; }
        }

        public List<GameObject> AttackPool {
            get { return _attackPool; }
            set { _attackPool = value; }
        }
        
        #endregion

        #region Damage
        [SerializeField]
        private float _damageAmount;
        private Attributes.Elements _damageType;

        public float DamageAmount {
            get { return _damageAmount; }
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
            _poolSize = 3;

            _attackPool = new List<GameObject>();
            if (AttackPrefabs.Count != 0) {
                for (int i = 0; i < _poolSize; i++) {
                    GameObject gameObject = Instantiate(AttackPrefabs[AttackKind]);
                    gameObject.SetActive(false);
                    _attackPool.Add(gameObject);
                }
            }
        }

        protected override void setUpFSM()
        {
            base.setUpFSM();

            IdleState idleState = new IdleState();
            idleState.AddMap(FSMTriggerID.StartMoveTrigger, FSMStateID.Move);
            idleState.AddMap(FSMTriggerID.StartAttackTrigger, FSMStateID.Attack);
            idleState.AddMap(FSMTriggerID.DamageTrigger, FSMStateID.Damage);
            _states.Add(idleState);

            MoveState moveState = new MoveState();
            moveState.AddMap(FSMTriggerID.StartDashTrigger, FSMStateID.Dash);
            moveState.AddMap(FSMTriggerID.EndMoveTrigger, FSMStateID.Idle);
            moveState.AddMap(FSMTriggerID.StartAttackTrigger, FSMStateID.Attack);
            moveState.AddMap(FSMTriggerID.DamageTrigger, FSMStateID.Damage);
            _states.Add(moveState);

            DashState dashState = new DashState();
            dashState.AddMap(FSMTriggerID.EndDashTrigger, FSMStateID.Idle);
            _states.Add(dashState);

            AttackState attackState = new AttackState();
            attackState.AddMap(FSMTriggerID.EndAttackTrigger, FSMStateID.Idle);
            _states.Add(attackState);

            DamageState damageState = new DamageState();
            
            _states.Add(damageState);
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

        public void TakeDamage() {
            Attributes attribute = GetComponentInChildren<Attributes>();
            attribute.HP -= _damageAmount;
            StartCoroutine(CharacterFlick());
        }

        private IEnumerator CharacterFlick()
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(0.1f);
            GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
    }
}