using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.FSM
{
    public class CharacterFSM_boss : FSMBase
    {
        public int PS = 100; //boss体力
        private int _recoverTime = 0;

        #region Move and Dash
        public float MoveSpeed = 5f;
        private int _dashCost;
        public float DashSpeed = 10f;
        private float _dashTimer;
        public float DashTime = 0.1f;
        private bool _isDashing;
        private Rigidbody2D _rb;
        private GameObject _gameObject;
        public GameObject prefabSpike;
        public GameObject prefabTrap;

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
        public Rigidbody2D bullet;
        private Transform fponit;
        public float BulletSpeed = 10f;

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

        public GameObject Go
        {
            get { return _gameObject; }
        }
        public Rigidbody2D Bullet
        {
            get { return bullet; }
        }
        public Transform FPonit
        {
            get { return fponit; }
        }


        protected override void init() {
            _rb = GetComponent<Rigidbody2D>();
            _dashTimer = DashTime;
            _dashCost = 10;
            _isDashing = false;
            _gameObject = gameObject;
            fponit = gameObject.transform;
        }

        protected override void setUpFSM()
        {
            base.setUpFSM();
            WanderState wanderState=new WanderState();
            wanderState.AddMap(FSMTriggerID.DamageTrigger,FSMStateID.Damage);
            _states.Add(wanderState);

            DashState dashState = new DashState();
            _states.Add(dashState);
            dashState.AddMap(FSMTriggerID.DamageTrigger, FSMStateID.Damage);

            SpikeState spikeState = new SpikeState();
            _states.Add(spikeState);
            spikeState.AddMap(FSMTriggerID.DamageTrigger, FSMStateID.Damage);

            AttackState attackState=new AttackState();
            _states.Add(attackState);
            attackState.AddMap(FSMTriggerID.DamageTrigger, FSMStateID.Damage);
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
            if(attribute.HP < 0)
            {
                Destroy(transform);
            }
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