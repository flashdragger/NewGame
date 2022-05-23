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
        public GameObject prefab;

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
            set { _damageAmount = value; }
        }
        #endregion

        #region Element

        [ReadOnly]
        public Attributes.Elements AttachedElement = Attributes.Elements.NULL;
        private int _eleExistTime = 250;
        private int _eleExistTimer;
        private float damageRate;
        private float _frozenTime = 2.0f;
        private float _frozenTimer;
        [ReadOnly]
        public bool IsFrozen = false;

        public float FrozenTime {
            get {return _frozenTime;}
        }

        public float FrozenTimer {
            get {return _frozenTimer;}
            set {_frozenTimer = value;}
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
            _states.Add(wanderState);
            wanderState.AddMap(FSMTriggerID.DamageTrigger,FSMStateID.Damage);
            wanderState.AddMap(FSMTriggerID.FrozenTrigger, FSMStateID.Frozen);

            DashState dashState = new DashState();
            _states.Add(dashState);
            dashState.AddMap(FSMTriggerID.DamageTrigger, FSMStateID.Damage);
            dashState.AddMap(FSMTriggerID.FrozenTrigger, FSMStateID.Frozen);

            SpikeState spikeState = new SpikeState();
            _states.Add(spikeState);
            spikeState.AddMap(FSMTriggerID.DamageTrigger, FSMStateID.Damage);
            spikeState.AddMap(FSMTriggerID.FrozenTrigger, FSMStateID.Frozen);

            AttackState attackState=new AttackState();
            _states.Add(attackState);
            attackState.AddMap(FSMTriggerID.DamageTrigger, FSMStateID.Damage);
            attackState.AddMap(FSMTriggerID.FrozenTrigger, FSMStateID.Frozen);

            DamageState damageState = new DamageState();
            _states.Add(damageState);

            FrozenState frozenState = new FrozenState();
            _states.Add(frozenState);
            frozenState.AddMap(FSMTriggerID.EndFrozenTrigger, FSMStateID.Wander);
            frozenState.AddMap(FSMTriggerID.DamageTrigger, FSMStateID.Damage);
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
            if (AttachedElement != Attributes.Elements.NULL) {
                _eleExistTimer -= 1;
            }
            if (_eleExistTimer == 0) {
                AttachedElement = Attributes.Elements.NULL;
            }
        }

        public void TakeDamage() {
            Attributes attribute = GetComponent<Attributes>();
            attribute.HP -= _damageAmount;
            if(attribute.HP < 0)
            {
                Destroy(gameObject);
            }
            StartCoroutine(CharacterFlick());
        }

        private IEnumerator CharacterFlick()
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(0.1f);
            GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.GetComponent<AttackObject>() != null) {
                AttackObject attackObject = collision.gameObject.GetComponent<AttackObject>();
                if (AttachedElement == Attributes.Elements.NULL) {
                    _damageAmount = attackObject.BaseDamage;
                    AttachedElement = attackObject.element;
                    _eleExistTimer = _eleExistTime;
                } else {
                    EleReaction reaction = new EleReaction();
                    if (IsFrozen && attackObject.element == Attributes.Elements.fire) 
                        _frozenTimer = 0;
                    if ((AttachedElement == Attributes.Elements.ice && attackObject.element == Attributes.Elements.water) || (AttachedElement == Attributes.Elements.water && attackObject.element == Attributes.Elements.ice)) {
                        IsFrozen = true;
                        _damageAmount = attackObject.BaseDamage * reaction.Reaction(AttachedElement, attackObject.element);
                        return;
                    }
                    _damageAmount = attackObject.BaseDamage * reaction.Reaction(AttachedElement, attackObject.element);
                    AttachedElement =  AttachedElement < attackObject.element ? AttachedElement : attackObject.element;
                }
            }
        }
    }
}